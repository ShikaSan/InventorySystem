Imports System.IO

Public Class UserAccountability
    Inherits System.Web.UI.Page
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
                    getData("", "", "")
                    'setDept()
                    setPos()
                    Me.Master.getUserName(strFullName, strUserName)
                    'Session.Remove("Username")
                    'Session.Remove("FullName")
                    Dim cls As New User_Validation
                    If cls.userLevel(strUserName) = "RO" Then
                        btnAddAcc.Visible = False
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
    End Sub

    Protected Sub btnAddAcc_Click(sender As Object, e As EventArgs)
        Session("Username") = strUserName
        Session("FullName") = strFullName
        strUserName = ""
        strFullName = ""
        Response.Redirect("~/Forms/AddAccountability.aspx")
    End Sub

    Protected Sub gvGetAccList_SelectedIndexChanged(sender As Object, e As EventArgs)
        Session("id") = gvGetAccList.SelectedRow.Cells(0).Text
        Session("name") = gvGetAccList.SelectedRow.Cells(1).Text
        Session("Username") = strUserName
        Session("FullName") = strFullName
        strUserName = ""
        strFullName = ""
        Response.Redirect("~/Forms/ViewAccountability.aspx")
    End Sub

    Protected Sub gvGetAccList_RowDataBound(sender As Object, e As GridViewRowEventArgs)
        If (e.Row.RowType = DataControlRowType.DataRow) Then
            e.Row.Cells(0).Visible = False
            ' e.Row.Cells(5).Visible = False
        End If

        If (e.Row.RowType = DataControlRowType.Header) Then
            e.Row.Cells(0).Visible = False
            'e.Row.Cells(5).Visible = False
        End If

        If e.Row.RowType = DataControlRowType.DataRow Then
            e.Row.Attributes("onclick") = Page.ClientScript.GetPostBackClientHyperlink(gvGetAccList, "Select$" & e.Row.RowIndex)
            e.Row.ToolTip = "Click to select this row."
        End If
    End Sub

    Private Sub getData(param_name As String, param_dept As String, param_pos As String)
        Dim getData As New Inventory
        Dim ds As New DataSet

        ds = getData.getUAlist(param_name, param_dept, param_pos)
        gvGetAccList.DataSource = ds
        gvGetAccList.DataBind()
    End Sub

    Protected Sub txtSearch_TextChanged(sender As Object, e As EventArgs)
        getData(txtSearch.Text, txtDepartment.Text, ddlPosition.SelectedValue)
    End Sub

    Protected Sub ddlDepartment_SelectedIndexChanged(sender As Object, e As EventArgs)
        getData(txtSearch.Text, txtDepartment.Text, ddlPosition.SelectedValue)
    End Sub

    'Private Sub setDept()
    '    Dim getData As New Inventory
    '    Dim ds As New DataSet

    '    ds = getData.getDept()
    '    ddlDepartment.DataSource = ds
    '    ddlDepartment.DataTextField = "DeptName"
    '    ddlDepartment.DataValueField = "idDept"
    '    ddlDepartment.DataBind()
    '    ddlDepartment.Items.Insert(0, "")
    '    ddlDepartment.Items(0).Value = 0
    'End Sub

    Private Sub setPos()
        Dim getData As New Inventory
        Dim ds As New DataSet

        ds = getData.getPos()
        ddlPosition.DataSource = ds
        ddlPosition.DataTextField = "Position"
        ddlPosition.DataBind()
        ddlPosition.Items.Insert(0, "")
        ddlPosition.Items(0).Value = 0
    End Sub

    Protected Sub ddlPosition_SelectedIndexChanged(sender As Object, e As EventArgs)
        getData(txtSearch.Text, txtDepartment.Text, ddlPosition.SelectedValue)
    End Sub

    Protected Sub btnPrintAcc_Click(sender As Object, e As EventArgs)
        getList()
        'updPnl1.Update()
        dvPrintAcc.Visible = True
    End Sub

    Protected Sub btnPrintAll_Click(sender As Object, e As EventArgs)
        exportPrinting("A")
    End Sub

    Protected Sub btnPrintSelected_Click(sender As Object, e As EventArgs)
        exportPrinting("S")
    End Sub

    Protected Sub btnCancel_Click(sender As Object, e As EventArgs)
        dvPrintAcc.Visible = False
    End Sub

    Protected Sub gvNameList_SelectedIndexChanged(sender As Object, e As EventArgs)
        Dim dt As New DataTable
        dt.Columns.Add("ID")
        dt.Columns.Add("Name")
        Dim x() As String
        If gvSelectedList.Rows.Count <> 0 Then
            For Each row As GridViewRow In gvSelectedList.Rows
                Dim dr As DataRow = dt.NewRow()
                For j = 0 To row.Cells.Count - 1
                    dr(j) = row.Cells(j).Text
                Next
                dt.Rows.Add(dr)
            Next
        End If
        x = {gvNameList.SelectedRow.Cells(0).Text, gvNameList.SelectedRow.Cells(1).Text}
        dt.Rows.Add(x)
        gvSelectedList.DataSource = dt
        gvSelectedList.DataBind()
        'updPnl1.Update()
    End Sub

    Protected Sub gvNameList_RowDataBound(sender As Object, e As GridViewRowEventArgs)
        'If (e.Row.RowType = DataControlRowType.DataRow) Then
        '    e.Row.Cells(0).Visible = False
        'End If

        If e.Row.RowType = DataControlRowType.DataRow Then
            e.Row.Attributes("onclick") = Page.ClientScript.GetPostBackClientHyperlink(gvNameList, "Select$" & e.Row.RowIndex)
            e.Row.ToolTip = "Click to select this row."
        End If
    End Sub

    Protected Sub gvNameList_PageIndexChanging(sender As Object, e As GridViewPageEventArgs)
        gvNameList.PageIndex = e.NewPageIndex
        getList()
        'updPnl1.Update()
    End Sub

    Protected Sub gvSelectedList_SelectedIndexChanged(sender As Object, e As EventArgs)
        Dim dt As New DataTable
        dt.Columns.Add("ID")
        dt.Columns.Add("Name")
        If gvSelectedList.Rows.Count <> 0 Then
            For Each row As GridViewRow In gvSelectedList.Rows
                Dim dr As DataRow = dt.NewRow()
                For j = 0 To row.Cells.Count - 1
                    dr(j) = row.Cells(j).Text
                Next
                dt.Rows.Add(dr)
            Next
        End If
        dt.Rows.RemoveAt(gvSelectedList.SelectedRow.RowIndex)
        gvSelectedList.DataSource = dt
        gvSelectedList.DataBind()
        'updPnl1.Update()
    End Sub

    Protected Sub gvSelectedList_RowDataBound(sender As Object, e As GridViewRowEventArgs)
        'If (e.Row.RowType = DataControlRowType.DataRow) Then
        '    e.Row.Cells(0).Visible = False
        'End If

        If e.Row.RowType = DataControlRowType.DataRow Then
            e.Row.Attributes("onclick") = Page.ClientScript.GetPostBackClientHyperlink(gvSelectedList, "Select$" & e.Row.RowIndex)
            e.Row.ToolTip = "Click to select this row."
        End If
    End Sub

    Private Sub getList()
        Dim list As New PrintHelper
        gvNameList.DataSource = list.getNamesAcc()
        gvNameList.DataBind()
    End Sub

    Private Function funcHeader(strID As String) As DataSet
        Dim details As New PrintHelper
        funcHeader = details.getPrintHeader(strID)
    End Function

    Private Function funcEqList(strID As String) As DataSet
        Dim details As New PrintHelper
        funcEqList = details.getEqAcc(strID)
    End Function


    Private Sub exportPrinting(param_mode As String)
        Dim builder As New StringBuilder
        Dim dtNames As New DataTable
        dtNames.Columns.Add("id")
        dtNames.Columns.Add("Name")

        If param_mode = "A" Then
            If gvNameList.Rows.Count <> 0 Then
                For Each row As GridViewRow In gvNameList.Rows
                    Dim dr As DataRow = dtNames.NewRow()
                    For j = 0 To row.Cells.Count - 1
                        dr(j) = row.Cells(j).Text
                    Next
                    dtNames.Rows.Add(dr)
                Next
            End If
        ElseIf param_mode = "S" Then
            If gvSelectedList.Rows.Count <> 0 Then
                For Each row As GridViewRow In gvSelectedList.Rows
                    Dim dr As DataRow = dtNames.NewRow()
                    For j = 0 To row.Cells.Count - 1
                        dr(j) = row.Cells(j).Text
                    Next
                    dtNames.Rows.Add(dr)
                Next
            End If
        End If

        For i = 0 To dtNames.Rows.Count - 1
            Dim dsNames As New DataSet

            dsNames = funcHeader(dtNames.Rows(i)(0).ToString())
            builder.Append("<html><body>")
            builder.Append("PC # : <b>" + dsNames.Tables(0).Rows(0)(0).ToString() + "</b>")
            builder.Append("<b>   |   </b>")
            builder.Append("User : <b>" + dsNames.Tables(0).Rows(0)(1).ToString() + "</b>")
            builder.Append("<b>   |   </b>")
            builder.Append("Host Name : <b>" + dsNames.Tables(0).Rows(0)(2).ToString() + "</b>")

            builder.Append("<br>")
            builder.Append("<table cellpadding = " & ControlChars.Quote & "1" & ControlChars.Quote &
                           " cellspacing = " & ControlChars.Quote & "1" & ControlChars.Quote &
                           " border = " & ControlChars.Quote & "0" & ControlChars.Quote &
                           " style=" & ControlChars.Quote & "width:100%; 
                           font-family:'Segoe UI';
                           padding:4px 4px 4px 4px;
                           margin:0px 0px 0px 0px;
                           border:solid 2px black; 
                           border-spacing: 0;
                           width: 100%;
                           display: table;
                           text-align: left;
                           vertical-align: top;
                           padding-left: 16px" & ControlChars.Quote & ">")
            builder.Append("    <tr>")
            builder.Append("        <td>")
            builder.Append("            <b>Type</b>")
            builder.Append("        </td>")
            builder.Append("        <td>")
            builder.Append("             <b>Description</b>")
            builder.Append("        </td>")
            builder.Append("        <td>")
            builder.Append("            <b>PPE #</b>")
            builder.Append("        </td>")
            builder.Append("        <td>")
            builder.Append("            <b>Asset #</b>")
            builder.Append("        </td>")
            builder.Append("        <td>")
            builder.Append("            <b>Serial #</b>")
            builder.Append("        </td>")
            builder.Append("        <td>")
            builder.Append("            <b>Remarks</b>")
            builder.Append("        </td>")
            builder.Append("        <td>")
            builder.Append("            <b>Date Acquired</b>")
            builder.Append("        </td>")
            builder.Append("    </tr>")

            Dim dsEqLst As New DataSet

            dsEqLst = funcEqList(dsNames.Tables(0).Rows(0)(0).ToString())

            For x = 0 To dsEqLst.Tables(0).Rows.Count - 1
                builder.Append("<tr>")
                For y = 0 To dsEqLst.Tables(0).Columns.Count - 1
                    builder.Append("<td>")
                    builder.Append(dsEqLst.Tables(0).Rows(x)(y).ToString())
                    builder.Append("</td>")
                Next
                builder.Append("</tr>")
            Next
            builder.Append("</table>")
            builder.Append("<br><hr>")
            builder.Append("<br>")
            builder.Append("</body></html>")
        Next
        Dim strDate As String = DateTime.Now.ToString("_ddMMMMyyy_HHmm")
        Dim path As String = "\\wsimkt-dt656\Users\Public\PrintAccountability" & strDate & ".html"
        'Dim path As String = "C:\Users\Public\" & strDate & ".html"
        'File.WriteAllText(path, builder.ToString())
        'System.Web.HttpContext.Current.Response.Write("<script>alert(" & ControlChars.Quote & "File sucessfully exported! Copy this link in your web browser :                      \\\\wsimkt-dt656\\Users\\Public\\PrintAccountability" & strDate & ".html" & ControlChars.Quote & ");</script>")
        dvPrintAcc.Visible = False
        'lnkPath.HRef = path
        'lnkbtn.PostBackUrl = path
        'test.Text = lnkPath.HRef.ToString()
        'dvNotif.Visible = True

        Dim output = builder.ToString()
        Response.Clear()
        Response.ClearHeaders()
        Response.AddHeader("Content-Length", builder.Length.ToString)
        Response.ContentType = "text/plain"
        Response.AppendHeader("content-disposition", "attachment;filename=Accountability" & strDate & ".html")
        Response.Write(builder)
        Response.End()
    End Sub

    Protected Sub btnClose_Click(sender As Object, e As EventArgs)
        dvNotif.Visible = False
    End Sub

    Protected Sub txtDepartment_TextChanged(sender As Object, e As EventArgs)
        getData(txtSearch.Text, txtDepartment.Text, ddlPosition.SelectedValue)
    End Sub

    Protected Sub gvGetAccList_PageIndexChanging(sender As Object, e As GridViewPageEventArgs)
        gvGetAccList.PageIndex = e.NewPageIndex
        getData(txtSearch.Text, txtDepartment.Text, ddlPosition.SelectedValue)
    End Sub
End Class