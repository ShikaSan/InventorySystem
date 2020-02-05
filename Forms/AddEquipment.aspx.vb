Imports System.Data.SqlClient
Imports System.IO

Public Class AddEquipment
    Inherits System.Web.UI.Page
    Private Shared prevPage As String = String.Empty
    Private Shared strUserName As String = Nothing
    Private Shared strFullName As String = Nothing
    Dim ext As String

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
                    getTypes()
                    prevPage = Request.UrlReferrer.ToString()
                    Me.Master.getUserName(strFullName, strUserName)
                    'Session.Remove("Username")
                    'Session.Remove("FullName")
                Else
                    Session.RemoveAll()
                    Response.Redirect("~/Forms/Login.aspx")
                End If
            Catch ex As Exception
                Session.RemoveAll()
                Response.Redirect("~/Forms/Login.aspx")
            End Try
        End If
    End Sub

    Protected Sub btnSave_Click(sender As Object, e As EventArgs)
        Dim cls As New Inventory
        Dim obj As New Inventory_Model

        If cls.databaseValidation("tbl_Equipments", "Equipment_PPE_Number", txtItemPPE.Text.Trim) = 0 Then
            If cls.databaseValidation("tbl_Equipments", "Equipment_Serial", txtItemSerial.Text.Trim) = 0 Then
                If cls.databaseValidation("tbl_Equipments", "Equipment_Asset_Number", txtItemAsset.Text.Trim) = 0 Then
                    If txtItemPPE.Text <> "" And txtItemSerial.Text <> "" And txtItemAsset.Text <> "" Then
                        obj.Type = ddlType.Text
                        obj.Desc = txtItemDesc.Text
                        obj.PPE = txtItemPPE.Text
                        obj.Serial = txtItemSerial.Text
                        obj.Asset = txtItemAsset.Text
                        obj.DateAcquired = txtDateAcquired.Text
                        'obj.RFTnum = txtRFTnum.Text
                        'obj.RFTDate = txtRFTdate.Text
                        obj.Remarks = txtRemarks.Text
                        obj.Data = FileUploadByte()
                        cls.saveNewEquip(obj)
                        cls.saveUserHistory("added new equipment", strFullName, ddlType.Text + " " + txtItemDesc.Text)
                        Session("Username") = strUserName
                        Session("FullName") = strFullName
                        strUserName = ""
                        strFullName = ""
                        Response.Redirect("~/Forms/Main.aspx")
                    Else
                        System.Web.HttpContext.Current.Response.Write("<script>alert(" & ControlChars.Quote & "Please fill in the required fields. Put N/A or None if the field is not available/applicable." & ControlChars.Quote & ");</script>")
                    End If
                Else
                    System.Web.HttpContext.Current.Response.Write("<script>alert(" & ControlChars.Quote & "Equipment already exist" & ControlChars.Quote & ");</script>")
                End If
            Else
                System.Web.HttpContext.Current.Response.Write("<script>alert(" & ControlChars.Quote & "Equipment already exist" & ControlChars.Quote & ");</script>")
            End If
        Else
            System.Web.HttpContext.Current.Response.Write("<script>alert(" & ControlChars.Quote & "Equipment already exist" & ControlChars.Quote & ");</script>")
        End If
    End Sub

    Private Sub getTypes()
        Dim getData As New Inventory
        Dim ds As New DataSet

        ds = getData.getTypes()
        ddlType.DataSource = ds
        ddlType.DataTextField = "EQType"
        ddlType.DataBind()
        ddlType.Items.Insert(0, "")
        ddlType.Items(0).Value = ddlType.Items.Count
        ddlType.Items.Insert(ddlType.Items.Count, "Others")
        ddlType.Items(ddlType.Items.Count - 1).Value = ddlType.Items.Count
    End Sub

    Protected Sub btnCancel_Click(sender As Object, e As EventArgs)
        Session("Username") = strUserName
        Session("FullName") = strFullName
        strUserName = ""
        strFullName = ""
        Response.Redirect(prevPage)
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
End Class
