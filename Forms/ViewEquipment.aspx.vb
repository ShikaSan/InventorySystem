Imports System.Data
Imports System.Configuration
Imports System.Data.SqlClient
Imports System.IO

Public Class ViewEqiupment
    Inherits System.Web.UI.Page
    Private Shared prevPage As String = String.Empty
    Private Shared strUserName As String = Nothing
    Private Shared strFullName As String = Nothing
    Dim ext As String
    Private Shared equipID As Integer = Nothing

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        HttpContext.Current.Response.Cache.SetCacheability(HttpCacheability.NoCache)
        HttpContext.Current.Response.Cache.SetNoServerCaching()
        HttpContext.Current.Response.Cache.SetNoStore()

        If Not IsPostBack Then
            Try
                Dim i As Integer = 0
                While i < Session.Contents.Count
                    If Session.Keys(i).ToString() = "Username" Then
                        strUserName = Session("Username").ToString()
                    ElseIf Session.Keys(i).ToString() = "FullName" Then
                        strFullName = Session("FullName").ToString()
                    End If
                    i += 1
                End While

                If Session("FullName").ToString() <> String.Empty Then
                    lblID.Text = Session("id").ToString()
                    setData(lblID.Text)
                    getHistory(lblID.Text)
                    enabledDisabled(False)
                    Me.Master.getUserName(strFullName, strUserName)
                    Session.Remove("id")
                    'Session.Remove("Username")
                    'Session.Remove("FullName")

                    Dim cls As New User_Validation
                    If cls.userLevel(strUserName) = "RO" Then
                        enabledDisabled(False)
                        btnEdit.Visible = False
                    End If
                Else
                    Session.RemoveAll()
                    Response.Redirect("~/Forms/Login.aspx")
                End If
            Catch ex As Exception
                Session.RemoveAll()
                Response.Redirect("~/Forms/Login.aspx")
            End Try
        End If

        'ddlReplacement.Visible = False
        'lblReplacement.Visible = False



        'Dim ddlStatus_Val As String

        'ddlStatus_Val = ddlStatus.SelectedItem.Text

        'If ddlStatus_Val = "For Repair" Or ddlStatus_Val = "Defective" Then
        '    ddlReplacement.Visible = True
        '    lblReplacement.Visible = True
        '    fill_ReplacementDDL()
        'Else
        '    ddlReplacement.Visible = False
        '    lblReplacement.Visible = False
        'End If

    End Sub

    Private Sub getTypes()
        Dim getData As New Inventory
        Dim ds As New DataSet

        ds = getData.getTypes()
        ddlType.DataSource = ds
        ddlType.DataTextField = "EQType"
        ddlType.DataBind()
        ddlType.Items.Insert(0, "")
        ddlType.Items(0).Value = 0
        ddlType.Items.Insert(ddlType.Items.Count, "Others")
        ddlType.Items(ddlType.Items.Count - 1).Value = ddlType.Items.Count
    End Sub

    Private Function SetBatteryAge(dateAcquired As String) As String
        Dim zeroTime As DateTime = New DateTime(1, 1, 1)
        Dim dateFromDB As String = dateAcquired
        Dim olddate As DateTime = DateTime.Parse(dateFromDB)
        Dim curdate As DateTime = DateTime.Now.ToLocalTime()

        Dim span As TimeSpan = curdate - olddate
        Dim years As Integer = (zeroTime + span).Year - 1
        Dim months As Integer = (zeroTime + span).Month - 1
        Dim days As Integer = (zeroTime + span).Day

        Dim parsedDate As String = years.ToString + " Year(s) " + months.ToString + " Month(s) " + days.ToString + " Day(s) "

        Return parsedDate
    End Function

    Private Sub setData(param_search As String)
        Dim cls As New Inventory
        Dim ds As New DataSet
        Dim equipType As String
        Dim Equipment_BatteryAge As String

        ds = cls.getDataEquipment(param_search)

        ddlType.Items.Insert(0, ds.Tables(0).Rows(0)(0).ToString())
        ddlType.Items(0).Value = 0
        ddlType.SelectedValue = 0
        txtItemDesc.Text = ds.Tables(0).Rows(0)(1).ToString()
        txtItemPPE.Text = ds.Tables(0).Rows(0)(2).ToString()
        txtItemSerial.Text = ds.Tables(0).Rows(0)(3).ToString()
        txtItemAsset.Text = ds.Tables(0).Rows(0)(4).ToString()
        txtDateAcquired.Text = ds.Tables(0).Rows(0)(5).ToString()

        txtBatteryAge.ReadOnly = True
        txtBatteryAge.Visible = False
        lblBatteryAge.Visible = False

        equipType = ds.Tables(0).Rows(0)(0).ToString()

        If equipType = "Laptop" OrElse equipType = "UPS" Then
            txtBatteryAge.Visible = True
            lblBatteryAge.Visible = True
            Equipment_BatteryAge = SetBatteryAge(txtDateAcquired.Text)
            txtBatteryAge.Text = Equipment_BatteryAge
        End If



        txtRFTnum.Text = ds.Tables(0).Rows(0)(6).ToString()
        txtRFTdate.Text = ds.Tables(0).Rows(0)(7).ToString()
        txtRemarks.Text = ds.Tables(0).Rows(0)(8).ToString()
        ddlStatus.Text = ds.Tables(0).Rows(0)(9).ToString()
        txtAssignedTo.Text = ds.Tables(0).Rows(0)(10).ToString()
        ddlLocation.SelectedIndex = ds.Tables(0).Rows(0)(11).ToString()
        txtOS.Text = ds.Tables(0).Rows(0)(12).ToString()
        txtHostName.Text = ds.Tables(0).Rows(0)(13).ToString()

        Try
            Dim bytes As Byte() = cls.getEqPic(param_search)
            'Dim base64String As String = Convert.ToBase64String(bytes, 0, bytes.Length)
            'imgEqp.ImageUrl = Convert.ToString("data:image/;base64,") & base64String
            imgEqp.ImageUrl = "data:image/png;base64," & Convert.ToBase64String(bytes)
            dvHasPic.Visible = True
            dvNoPic.Visible = False
        Catch ex As Exception
            dvNoPic.Visible = True
            dvHasPic.Visible = False
        End Try
        cls.saveUserHistory("viewed", strFullName, ddlType.Text + " " + txtItemDesc.Text)
    End Sub

    Protected Sub btnBack_Click(sender As Object, e As EventArgs)
        Session("Username") = strUserName
        Session("FullName") = strFullName
        strUserName = ""
        strFullName = ""
        Response.Redirect("~/Forms/Main.aspx")
    End Sub

    Private Function FileUploadByte() As Byte()
        Dim bData As Byte() = Nothing
        If fUpload.HasFile Then
            Dim iUploadedCnt As Integer = 0
            Dim iFailedCnt As Integer = 0
            Dim hfc As HttpFileCollection = Request.Files
            Dim TotalFileSize As Integer = 0

            If hfc.Count <= 1 Then
                For i As Integer = 0 To hfc.Count - 1
                    Dim hpf As HttpPostedFile = hfc(i)
                    Dim filename As String = Path.GetFileName(hpf.FileName)
                    Dim fileInfo As New FileInfo(filename)
                    ext = Path.GetExtension(filename)
                    If CheckFileEnd() = True Then
                        If hpf.ContentLength > 0 And hpf.ContentLength <= 3000000 Then
                            Dim fs As IO.Stream = hpf.InputStream
                            Dim br As New BinaryReader(fs)
                            Dim bytes As Byte() = br.ReadBytes(fs.Length)
                            bData = bytes
                        End If
                    End If
                Next i
            Else
            End If
        Else
            bData = Nothing
        End If
        Return bData
    End Function

    Private Function CheckFileEnd() As Boolean
        CheckFileEnd = False

        Select Case ext
            Case ".jpg"
                ContentType = "jpg"
                CheckFileEnd = True
                Exit Select
            Case ".png"
                ContentType = "png"
                CheckFileEnd = True
                Exit Select
            Case ".jpeg"
                ContentType = "jpg"
                CheckFileEnd = True
                Exit Select
            Case ".JPG"
                ContentType = "jpg"
                CheckFileEnd = True
                Exit Select
            Case ".JPEG"
                ContentType = "jpg"
                CheckFileEnd = True
                Exit Select
            Case ".PNG"
                ContentType = "png"
                CheckFileEnd = True
                Exit Select
        End Select
    End Function

    Protected Sub btnEdit_Click(sender As Object, e As EventArgs)
        enabledDisabled(True)
        btnEdit.Visible = False
        btnSave.Visible = True
        btnCancel.Visible = True
        btnBack.Visible = False
        Dim x As String
        x = ddlType.SelectedItem.ToString()
        getTypes()
        ddlType.Text = x
        Dim getid As New Inventory
        equipID = getid.getID(txtItemPPE.Text, txtItemAsset.Text, txtItemSerial.Text)
        dvNoPic.Visible = True
        dvHasPic.Visible = False
    End Sub

    Protected Sub btnSave_Click(sender As Object, e As EventArgs)
        Dim cls As New Inventory
        Dim obj As New Inventory_Model
        Dim equipLocation As String

        If txtItemPPE.Text <> "" And txtItemSerial.Text <> "" And txtItemAsset.Text <> "" Then
            obj.ID = equipID
            obj.Type = ddlType.Text
            obj.Desc = txtItemDesc.Text
            obj.PPE = txtItemPPE.Text
            obj.Asset = txtItemAsset.Text
            obj.Serial = txtItemSerial.Text
            obj.AssignedTo = txtAssignedTo.Text
            obj.Remarks = txtRemarks.Text
            obj.DateAcquired = txtDateAcquired.Text
            obj.Location = ddlLocation.SelectedIndex
            obj.RFTnum = txtRFTnum.Text
            obj.RFTDate = txtRFTdate.Text
            obj.OS = txtOS.Text
            obj.HostName = txtHostName.Text


            If dvNoPic.Visible = True Then
                Dim data As Byte() = FileUploadByte()
                Try
                    If data.Length > 0 Then
                        obj.Data = data
                    Else
                        obj.Data = Nothing
                    End If
                Catch
                    obj.Data = Nothing
                End Try
            End If

            'If ddlStatus.Text <> "For Disposal" Or ddlStatus.Text <> "Defective" Then
            '    If ddlStatus.Text = "On Stock" And txtAssignedTo.Text <> "" Then
            '        obj.Status = "Assigned"
            '    ElseIf ddlStatus.Text = "Assigned" And txtAssignedTo.Text = "ITSG" Then
            '        obj.Status = "On Stock"
            '    ElseIf ddlStatus.Text = "Assigned" And txtAssignedTo.Text <> "" Then
            '        obj.Status = "Assigned"
            '    Else
            '        obj.Status = "On Stock"
            '    End If
            'Else
            obj.Status = ddlStatus.Text
            'End If

            cls.updateEquip(obj)
            cls.saveUserHistory("updated", strFullName, ddlType.Text + " " + txtItemDesc.Text)
            cls.AddEquipmentHistory(equipID, txtAssignedTo.Text, equipLocation, ddlStatus.Text)
            Session("Username") = strUserName
            Session("FullName") = strFullName
            strUserName = ""
            strFullName = ""
            Response.Redirect("~/Forms/Main.aspx")
        Else
            System.Web.HttpContext.Current.Response.Write("<script>alert(" & ControlChars.Quote & "Please Complete All Fields!" & ControlChars.Quote & ");</script>")
        End If

    End Sub

    Protected Sub btnCancel_Click(sender As Object, e As EventArgs)
        enabledDisabled(False)
        btnEdit.Visible = True
        btnSave.Visible = False
        btnCancel.Visible = False
        btnBack.Visible = True
        setData(lblID.Text)
        equipID = Nothing
    End Sub

    Private Sub enabledDisabled(x As Boolean)
        ddlType.Enabled = x
        txtItemDesc.Enabled = x
        txtItemPPE.Enabled = x
        txtItemSerial.Enabled = x
        txtItemAsset.Enabled = x
        txtDateAcquired.Enabled = x
        txtRFTnum.Enabled = x
        txtRFTdate.Enabled = x
        txtRemarks.Enabled = x
        ddlStatus.Enabled = x
        txtAssignedTo.Enabled = False
        ddlLocation.Enabled = x
        fUpload.Enabled = x

    End Sub

    Private Sub getHistory(param_search As String)
        Dim getData As New Inventory
        Dim ds As New DataSet

        ds = getData.getHistory("", param_search)
        gvHistory.DataSource = ds
        gvHistory.DataBind()
    End Sub

    Protected Sub gvHistory_RowDataBound(sender As Object, e As GridViewRowEventArgs)
        If (e.Row.RowType = DataControlRowType.DataRow) Then
            e.Row.Cells(0).Visible = False
            e.Row.Cells(1).Visible = False
            e.Row.Cells(2).Visible = False
            e.Row.Cells(3).Visible = False
            e.Row.Cells(4).Visible = False
            e.Row.Cells(5).Visible = False
        End If

        If (e.Row.RowType = DataControlRowType.Header) Then
            e.Row.Cells(0).Visible = False
            e.Row.Cells(1).Visible = False
            e.Row.Cells(2).Visible = False
            e.Row.Cells(3).Visible = False
            e.Row.Cells(4).Visible = False
            e.Row.Cells(5).Visible = False
        End If
    End Sub

    'Protected Sub fill_ReplacementDDL()
    '    Dim replacementType As String
    '    Dim ddl_DataSet As New DataSet
    '    Dim ddl_DataTable As New DataTable

    '    replacementType = ddlType.Text.ToString

    '    Dim Str As String
    '    Str = "Password=ch33s3c@k3;User ID=sa;Initial Catalog=Inventory_ITSG;Data Source=wsimkt-dt656; Persist Security Info=true; Connect Timeout=1000"

    '    Using con As New SqlConnection(Str)
    '        Dim com As String
    '        com = "SELECT Equipment_ID, Equipment_Description
    '                     FROM tbl_Equipments WHERE Equipment_Status = 'On Stock' AND Equipment_Type = '" & replacementType.Trim & "' 
    '                     "
    '        Dim adpt = New SqlDataAdapter(com, con)
    '        adpt.Fill(ddl_DataSet)

    '        ddlReplacement.DataSource = ddl_DataSet
    '        ddlReplacement.DataBind()
    '        ddlReplacement.DataTextField = "Equipment_Description"
    '        ddlReplacement.DataValueField = "Equipment_ID"
    '        ddlReplacement.DataBind()

    '        con.Close()
    '    End Using

    'End Sub

End Class