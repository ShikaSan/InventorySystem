Imports System.IO
Imports InventorySystem.Maintenance_Model

Public Class Maintenance
    Inherits System.Web.UI.Page
    Private Shared DepID As String = Nothing
    Private Shared EqID As String = Nothing
    Private Shared ULId As String = Nothing
    Private Shared strUserName As String = Nothing
    Private Shared strFullName As String = Nothing

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
                    'name = Session("name").ToString()
                    'txtName.Text = name
                    'getData(name)
                    getData("")
                    Me.Master.getUserName(strFullName, strUserName)

                    Session.Remove("name")
                    'Session.Remove("Username")
                    'Session.Remove("FullName")

                    'Dim cls As New clsUserValidation
                    'If cls.userLevel(strUserName) = "RO" Then
                    '    'btnEdit.Visible = False
                    '    'gvView.Enabled = False
                    '    'btnReclaim.Visible = False
                    'End If
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

    Private Sub getData(param_mode As String)
        Dim ds As New DataSet
        Dim cls As New clsMaintenance

        If param_mode = "0" Then
            ds = cls.getData("0")
            gvDept.DataSource = ds
            gvDept.DataBind()
        ElseIf param_mode = "1" Then
            ds = cls.getData("1")
            gvTypes.DataSource = ds
            gvTypes.DataBind()
        ElseIf param_mode = "2" Then
            ds = cls.getData("2")
            gvUserAccess.DataSource = ds
            gvUserAccess.DataBind()
        ElseIf param_mode = "" Then
            ds = cls.getData("0")
            gvDept.DataSource = ds
            gvDept.DataBind()
            ds.Clear()
            ds = cls.getData("1")
            gvTypes.DataSource = ds
            gvTypes.DataBind()
            ds.Clear()
            ds = cls.getData("2")
            gvUserAccess.DataSource = ds
            gvUserAccess.DataBind()
            ds.Clear()
        End If

    End Sub

    Protected Sub gvUserAccess_RowDataBound(sender As Object, e As GridViewRowEventArgs)
        If (e.Row.RowType = DataControlRowType.DataRow) Then
            e.Row.Cells(0).Visible = False
        End If

        If (e.Row.RowType = DataControlRowType.Header) Then
            e.Row.Cells(0).Visible = False
        End If

        If e.Row.RowType = DataControlRowType.DataRow Then
            e.Row.Attributes("onclick") = Page.ClientScript.GetPostBackClientHyperlink(gvUserAccess, "Select$" & e.Row.RowIndex)
            e.Row.ToolTip = "Click to select this row."
        End If
    End Sub

    Protected Sub gvUserAccess_SelectedIndexChanged(sender As Object, e As EventArgs)
        ULId = gvUserAccess.SelectedRow.Cells(0).Text
        txtName.Text = gvUserAccess.SelectedRow.Cells(1).Text
        txtDomain.Text = gvUserAccess.SelectedRow.Cells(2).Text
        ddlUserLvl.SelectedValue = gvUserAccess.SelectedRow.Cells(3).Text
        btnULSave.Text = "Update"
    End Sub

    Protected Sub gvDept_RowDataBound(sender As Object, e As GridViewRowEventArgs)
        If (e.Row.RowType = DataControlRowType.DataRow) Then
            e.Row.Cells(0).Visible = False
        End If

        If (e.Row.RowType = DataControlRowType.Header) Then
            e.Row.Cells(0).Visible = False
        End If

        If e.Row.RowType = DataControlRowType.DataRow Then
            e.Row.Attributes("onclick") = Page.ClientScript.GetPostBackClientHyperlink(gvDept, "Select$" & e.Row.RowIndex)
            e.Row.ToolTip = "Click to select this row."
        End If
    End Sub

    Protected Sub gvDept_SelectedIndexChanged(sender As Object, e As EventArgs)
        DepID = gvDept.SelectedRow.Cells(0).Text
        txtDeptName.Text = gvDept.SelectedRow.Cells(1).Text
        btnDSave.Text = "Update"
    End Sub

    Protected Sub gvTypes_RowDataBound(sender As Object, e As GridViewRowEventArgs)
        If (e.Row.RowType = DataControlRowType.DataRow) Then
            e.Row.Cells(0).Visible = False
        End If

        If (e.Row.RowType = DataControlRowType.Header) Then
            e.Row.Cells(0).Visible = False
        End If

        If e.Row.RowType = DataControlRowType.DataRow Then
            e.Row.Attributes("onclick") = Page.ClientScript.GetPostBackClientHyperlink(gvTypes, "Select$" & e.Row.RowIndex)
            e.Row.ToolTip = "Click to select this row."
        End If
    End Sub

    Protected Sub gvTypes_SelectedIndexChanged(sender As Object, e As EventArgs)
        EqID = gvTypes.SelectedRow.Cells(0).Text
        txtEqType.Text = gvTypes.SelectedRow.Cells(1).Text
        btnEqSave.Text = "Update"
    End Sub

    Protected Sub gvUserAccess_PageIndexChanging(sender As Object, e As GridViewPageEventArgs)
        gvUserAccess.PageIndex = e.NewPageIndex
        getData("2")
    End Sub

    Protected Sub gvTypes_PageIndexChanging(sender As Object, e As GridViewPageEventArgs)
        gvTypes.PageIndex = e.NewPageIndex
        getData("1")
    End Sub

    Protected Sub gvDept_PageIndexChanging(sender As Object, e As GridViewPageEventArgs)
        gvDept.PageIndex = e.NewPageIndex
        getData("0")
    End Sub

    Protected Sub btnDCancel_Click(sender As Object, e As EventArgs)
        txtDeptName.Text = ""
        btnDSave.Text = "Save"
    End Sub

    Protected Sub btnEqCancel_Click(sender As Object, e As EventArgs)
        txtEqType.Text = ""
        btnEqSave.Text = "Save"
    End Sub

    Protected Sub btnULCancel_Click(sender As Object, e As EventArgs)
        txtName.Text = ""
        txtDomain.Text = ""
        ddlUserLvl.SelectedValue = 0
        btnULSave.Text = "Save"
    End Sub

    Protected Sub btnDSave_Click(sender As Object, e As EventArgs)
        Dim cls As New clsMaintenance
        Dim obj As New Maintenance_Model
        obj.val1 = txtDeptName.Text
        obj.id = DepID
        cls.saveData(btnDSave.Text, "Dept", obj)
        getData(0)
        txtDeptName.Text = ""
        btnDSave.Text = "Save"
    End Sub

    Protected Sub btnEqSave_Click(sender As Object, e As EventArgs)
        Dim cls As New clsMaintenance
        Dim obj As New Maintenance_Model
        obj.val1 = txtEqType.Text
        obj.id = EqID
        cls.saveData(btnEqSave.Text, "Type", obj)
        getData(1)
        txtEqType.Text = ""
        btnEqSave.Text = "Save"
    End Sub

    Protected Sub btnULSave_Click(sender As Object, e As EventArgs)
        Dim cls As New clsMaintenance
        Dim obj As New Maintenance_Model
        obj.val1 = txtName.Text.Trim
        obj.val2 = txtDomain.Text.Trim
        obj.val3 = ddlUserLvl.SelectedValue.ToString()
        obj.id = ULId
        cls.saveData(btnULSave.Text, "UserAccess", obj)
        getData(2)
        txtName.Text = ""
        txtDomain.Text = ""
        ddlUserLvl.SelectedValue = 0
        btnULSave.Text = "Save"
    End Sub

    'Protected Sub btnUpload_Click(sender As Object, e As EventArgs)
    '    If FileUpload1.HasFile = True Then
    '        Dim SR As StreamReader = New StreamReader(FileUpload1.PostedFile.InputStream)
    '        Dim line As String = SR.ReadLine()
    '        Dim strArray As String() = line.Split(","c)
    '        Dim dtExcelFile As DataTable = New DataTable()
    '        Dim row As DataRow
    '        Dim line2 As String = Nothing

    '        dtExcelFile.Columns.Add(New DataColumn())
    '        dtExcelFile.Columns.Add(New DataColumn())
    '        dtExcelFile.Columns.Add(New DataColumn())
    '        dtExcelFile.Columns.Add(New DataColumn())
    '        dtExcelFile.Columns.Add(New DataColumn())
    '        dtExcelFile.Columns.Add(New DataColumn())
    '        dtExcelFile.Columns.Add(New DataColumn())
    '        dtExcelFile.Columns.Add(New DataColumn())
    '        dtExcelFile.Columns.Add(New DataColumn())
    '        dtExcelFile.Columns.Add(New DataColumn())
    '        dtExcelFile.Columns.Add(New DataColumn())
    '        dtExcelFile.Columns.Add(New DataColumn())
    '        dtExcelFile.Columns.Add(New DataColumn())
    '        dtExcelFile.Columns.Add(New DataColumn())

    '        Dim count As Integer = 0
    '        Try
    '            Do
    '                line = SR.ReadLine.Trim
    '                line2 = line
    '                line2 = line2.ToString.Replace(",", "_")
    '                If Not line2 = "______________" Then
    '                    row = dtExcelFile.NewRow()
    '                    'line = line.ToString.Replace(",", "_")
    '                    line = line.ToString.Replace(ControlChars.Quote, "")
    '                    line = line.ToString.Replace("'", "")
    '                    line = line.Trim
    '                    row.ItemArray = line.Split(","c)
    '                    dtExcelFile.Rows.Add(row)
    '                    count = count + 1
    '                End If
    '            Loop
    '        Catch ex As Exception

    '        End Try
    '        'sampleonly.DataSource = dt
    '        'sampleonly.DataBind()


    '        'Dim obj As New clsProducts()
    '        'Dim oProds As New clsProducts()
    '        Dim dtEqps As DataTable = New DataTable()
    '        Dim prevName As String = Nothing
    '        Dim curName As String = Nothing
    '        Dim rowAcc As DataRow
    '        Dim save As New Assign_Equip
    '        Dim pcnum As Integer = 0

    '        dtEqps.Columns.Add(New DataColumn())
    '        dtEqps.Columns.Add(New DataColumn())
    '        dtEqps.Columns.Add(New DataColumn())
    '        dtEqps.Columns.Add(New DataColumn())
    '        dtEqps.Columns.Add(New DataColumn())
    '        dtEqps.Columns.Add(New DataColumn())
    '        dtEqps.Columns.Add(New DataColumn())
    '        dtEqps.Columns.Add(New DataColumn())
    '        dtEqps.Columns.Add(New DataColumn())
    '        dtEqps.Columns.Add(New DataColumn())
    '        dtEqps.Columns.Add(New DataColumn())
    '        dtEqps.Columns.Add(New DataColumn())
    '        dtEqps.Columns.Add(New DataColumn())
    '        dtEqps.Columns.Add(New DataColumn())

    '        For x = 0 To dtExcelFile.Rows.Count - 1
    '            Dim cls As New clsMaintenance
    '            Dim cls2 As New Inventory
    '            If cls2.databaseValidation("tbl_Equipments", "Equipment_PPE_Number", dtExcelFile.Rows(x)(4).ToString().Trim) = 0 Then
    '                If cls2.databaseValidation("tbl_Equipments", "Equipment_Serial", dtExcelFile.Rows(x)(6).ToString().Trim) = 0 Then
    '                    If cls2.databaseValidation("tbl_Equipments", "Equipment_Asset_Number", dtExcelFile.Rows(x)(5).ToString().Trim) = 0 Then
    '                        If dtExcelFile.Rows(x)(0).ToString().Trim <> "" Then
    '                            cls.saveUpload(dtExcelFile.Rows(x)(2).ToString().Trim, dtExcelFile.Rows(x)(3).ToString().Trim, dtExcelFile.Rows(x)(4).ToString().Trim,
    '                                       dtExcelFile.Rows(x)(5).ToString().Trim, dtExcelFile.Rows(x)(6).ToString().Trim, dtExcelFile.Rows(x)(11).ToString().Trim,
    '                                       dtExcelFile.Rows(x)(12).ToString().Trim)
    '                            If dtExcelFile.Rows(x)(9).ToString().Trim <> "" Then
    '                                cls.saveHistory(dtExcelFile.Rows(x)(2).ToString().Trim, dtExcelFile.Rows(x)(3).ToString().Trim, dtExcelFile.Rows(x)(4).ToString().Trim,
    '                                            dtExcelFile.Rows(x)(5).ToString().Trim, dtExcelFile.Rows(x)(6).ToString().Trim, dtExcelFile.Rows(x)(9).ToString().Trim)
    '                            End If

    '                            If x <> 0 Then
    '                                prevName = dtExcelFile.Rows(x - 1)(1).ToString()
    '                            Else
    '                                prevName = dtExcelFile.Rows(x)(1).ToString()
    '                            End If
    '                            curName = dtExcelFile.Rows(x)(1).ToString()

    '                            If prevName = curName Then
    '                                rowAcc = dtEqps.NewRow()
    '                                rowAcc.Item(0) = dtExcelFile.Rows(x)(0).ToString().Trim 'Dept
    '                                rowAcc.Item(1) = dtExcelFile.Rows(x)(1).ToString().Trim 'Name
    '                                rowAcc.Item(2) = dtExcelFile.Rows(x)(2).ToString().Trim 'Type
    '                                rowAcc.Item(3) = dtExcelFile.Rows(x)(3).ToString().Trim 'Desc
    '                                rowAcc.Item(4) = dtExcelFile.Rows(x)(4).ToString().Trim 'PPE
    '                                rowAcc.Item(5) = dtExcelFile.Rows(x)(5).ToString().Trim 'asset
    '                                rowAcc.Item(6) = dtExcelFile.Rows(x)(6).ToString().Trim 'serial
    '                                rowAcc.Item(7) = dtExcelFile.Rows(x)(7).ToString().Trim 'rftnum
    '                                rowAcc.Item(8) = dtExcelFile.Rows(x)(8).ToString().Trim 'rftdate
    '                                rowAcc.Item(9) = dtExcelFile.Rows(x)(9).ToString().Trim 'history
    '                                rowAcc.Item(10) = dtExcelFile.Rows(x)(10).ToString().Trim 'OS
    '                                rowAcc.Item(11) = dtExcelFile.Rows(x)(11).ToString().Trim 'dateacq
    '                                rowAcc.Item(12) = dtExcelFile.Rows(x)(12).ToString().Trim 'remarks
    '                                rowAcc.Item(13) = dtExcelFile.Rows(x)(13).ToString().Trim 'hostname
    '                            Else
    '                                Dim loc As String = ""
    '                                Dim type As String = ""

    '                                loc = dtEqps.Rows(0)(13).ToString()
    '                                loc = loc.Substring(0, 6)

    '                                If loc.ToUpper = "WSIMKT" Then
    '                                    loc = "2"
    '                                ElseIf loc.ToUpper = "WSICBU" Then
    '                                    loc = "3"
    '                                ElseIf loc.ToUpper = "WSIDVO" Then
    '                                    loc = "4"
    '                                End If

    '                                type = dtEqps.Rows(0)(13).ToString()
    '                                type = type.Substring(7, 2)
    '                                If type.ToLower = "lt" Then
    '                                    type = "Laptop"
    '                                ElseIf type.ToLower = "dt" Then
    '                                    type = "Desktop"
    '                                End If

    '                                If save.validatePCnumber(dtEqps.Rows(0)(13).ToString(), type) > 0 Then
    '                                    pcnum = 0
    '                                Else
    '                                    pcnum = save.getPCnumber() + 1
    '                                End If

    '                                For y = 0 To dtEqps.Rows.Count - 1
    '                                    save.saveAssign(dtEqps.Rows(y)(2).ToString(), dtEqps.Rows(y)(4).ToString(), dtEqps.Rows(y)(5).ToString(),
    '                                                dtEqps.Rows(y)(6).ToString(), dtEqps.Rows(y)(1).ToString(), "", type, loc, dtEqps.Rows(y)(13).ToString(), pcnum,
    '                                                dtEqps.Rows(y)(7).ToString(), dtEqps.Rows(y)(8).ToString())
    '                                Next

    '                                Dim depts As New Inventory
    '                                Dim dsDept As New DataSet
    '                                Dim dept As String = ""

    '                                dsDept = depts.getDept()

    '                                For j = 0 To dsDept.Tables(0).Rows.Count - 1
    '                                    If dsDept.Tables(0).Rows(j)(1).ToString = dtEqps.Rows(0)(0).ToString() Then
    '                                        dept = dsDept.Tables(0).Rows(j)(0).ToString
    '                                    End If
    '                                Next

    '                                Dim eqType As String = ""
    '                                Dim desc As String = ""
    '                                Dim ppe As String = ""
    '                                Dim asset As String = ""
    '                                Dim serial As String = ""

    '                                For h = 0 To dtEqps.Rows.Count - 1
    '                                    If dtEqps.Rows(h)(2).ToString() = "Casing" Or dtEqps.Rows(h)(2).ToString() = "Laptop" Then
    '                                        eqType = dtEqps.Rows(h)(2).ToString()
    '                                        desc = dtEqps.Rows(h)(3).ToString()
    '                                        ppe = dtEqps.Rows(h)(4).ToString()
    '                                        asset = dtEqps.Rows(h)(5).ToString()
    '                                        serial = dtEqps.Rows(h)(6).ToString()
    '                                    End If
    '                                Next

    '                                save.saveInfo(dtEqps.Rows(0)(13).ToString(), dtEqps.Rows(0)(1).ToString(), "", dept, dtEqps.Rows(0)(10).ToString(), "position", type,
    '                                          eqType, desc, ppe, asset, serial, dtEqps.Rows(0)(7).ToString(), dtEqps.Rows(0)(8).ToString(), "Save", "")
    '                                dtEqps.Clear()

    '                                rowAcc = dtEqps.NewRow()
    '                                rowAcc.Item(0) = dtExcelFile.Rows(x)(0).ToString().Trim 'Dept
    '                                rowAcc.Item(1) = dtExcelFile.Rows(x)(1).ToString().Trim 'Name
    '                                rowAcc.Item(2) = dtExcelFile.Rows(x)(2).ToString().Trim 'Type
    '                                rowAcc.Item(3) = dtExcelFile.Rows(x)(3).ToString().Trim 'Desc
    '                                rowAcc.Item(4) = dtExcelFile.Rows(x)(4).ToString().Trim 'PPE
    '                                rowAcc.Item(5) = dtExcelFile.Rows(x)(5).ToString().Trim 'asset
    '                                rowAcc.Item(6) = dtExcelFile.Rows(x)(6).ToString().Trim 'serial
    '                                rowAcc.Item(7) = dtExcelFile.Rows(x)(7).ToString().Trim 'rftnum
    '                                rowAcc.Item(8) = dtExcelFile.Rows(x)(8).ToString().Trim 'rftdate
    '                                rowAcc.Item(9) = dtExcelFile.Rows(x)(9).ToString().Trim 'history
    '                                rowAcc.Item(10) = dtExcelFile.Rows(x)(10).ToString().Trim 'OS
    '                                rowAcc.Item(11) = dtExcelFile.Rows(x)(11).ToString().Trim 'dateacq
    '                                rowAcc.Item(12) = dtExcelFile.Rows(x)(12).ToString().Trim 'remarks
    '                                rowAcc.Item(13) = dtExcelFile.Rows(x)(13).ToString().Trim 'hostname

    '                            End If
    '                            dtEqps.Rows.Add(rowAcc)
    '                        End If
    '                    End If
    '                End If
    '            End If
    '        Next

    '        System.Web.HttpContext.Current.Response.Write("<script>alert(" & ControlChars.Quote & "File Upload Complete." & ControlChars.Quote & ");</script>")
    '    Else
    '        System.Web.HttpContext.Current.Response.Write("<script>alert(" & ControlChars.Quote & "No File Selected. Please Select A File To Upload First." & ControlChars.Quote & ");</script>")
    '    End If
    'End Sub

    Protected Sub btnUploadEqOnly_Click1(sender As Object, e As EventArgs)
        If FileUpload2.HasFile = True Then
            Dim SR As StreamReader = New StreamReader(FileUpload2.PostedFile.InputStream)
            Dim line As String = SR.ReadLine()
            Dim strArray As String() = line.Split(","c)
            Dim dtExcelFile As DataTable = New DataTable()
            Dim row As DataRow


            dtExcelFile.Columns.Add(New DataColumn())
            dtExcelFile.Columns.Add(New DataColumn())
            dtExcelFile.Columns.Add(New DataColumn())
            dtExcelFile.Columns.Add(New DataColumn())
            dtExcelFile.Columns.Add(New DataColumn())
            dtExcelFile.Columns.Add(New DataColumn())
            dtExcelFile.Columns.Add(New DataColumn())
            dtExcelFile.Columns.Add(New DataColumn())

            Dim count As Integer = 0
            Try
                Do
                    line = SR.ReadLine.Trim
                    row = dtExcelFile.NewRow()
                    line = line.Trim
                    row.ItemArray = line.Split(","c)
                    dtExcelFile.Rows.Add(row)
                    count = count + 1
                Loop
            Catch ex As Exception

            End Try

            Dim save As New Assign_Equip

            For x = 0 To dtExcelFile.Rows.Count - 1
                Dim cls As New clsMaintenance
                Dim cls2 As New Inventory
                If cls2.databaseValidation("tbl_Equipments", "Equipment_PPE_Number", dtExcelFile.Rows(x)(2).ToString().Trim) = 0 Then
                    If cls2.databaseValidation("tbl_Equipments", "Equipment_Asset_Number", dtExcelFile.Rows(x)(3).ToString().Trim) = 0 Then
                        If cls2.databaseValidation("tbl_Equipments", "Equipment_Serial", dtExcelFile.Rows(x)(4).ToString().Trim) = 0 Then
                            If dtExcelFile.Rows(x)(0).ToString().Trim <> "" Then
                                cls.saveUpload(dtExcelFile.Rows(x)(0).ToString().Trim,
                                                   dtExcelFile.Rows(x)(1).ToString().Trim,
                                                   dtExcelFile.Rows(x)(2).ToString().Trim,
                                                   dtExcelFile.Rows(x)(3).ToString().Trim,
                                                   dtExcelFile.Rows(x)(4).ToString().Trim,
                                                   dtExcelFile.Rows(x)(6).ToString().Trim,
                                                   dtExcelFile.Rows(x)(7).ToString().Trim)
                                If dtExcelFile.Rows(x)(5).ToString().Trim <> "" Then
                                    cls.saveHistory(dtExcelFile.Rows(x)(0).ToString().Trim,
                                                        dtExcelFile.Rows(x)(1).ToString().Trim,
                                                        dtExcelFile.Rows(x)(2).ToString().Trim,
                                                        dtExcelFile.Rows(x)(3).ToString().Trim,
                                                        dtExcelFile.Rows(x)(4).ToString().Trim,
                                                        dtExcelFile.Rows(x)(5).ToString().Trim)
                                End If
                            End If
                        End If
                    End If
                End If
            Next

            System.Web.HttpContext.Current.Response.Write("<script>alert(" & ControlChars.Quote & "File Upload Complete." & ControlChars.Quote & ");</script>")

        Else
            System.Web.HttpContext.Current.Response.Write("<script>alert(" & ControlChars.Quote & "No File Selected. Please Select A File To Upload First." & ControlChars.Quote & ");</script>")
        End If
    End Sub
End Class