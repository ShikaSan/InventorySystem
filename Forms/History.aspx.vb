Public Class History
    Inherits System.Web.UI.Page
    Private Shared strUserName As String = Nothing
    Private Shared strFullName As String = Nothing
    Private Shared switch As Integer = 0

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
                    getEquipHistory(txtSearch.Text)
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


    Protected Sub gvHistory_SelectedIndexChanged(sender As Object, e As EventArgs)
        If switch = 0 Then
            Session("id") = gvHistory.SelectedRow.Cells(0).Text
            Session("Username") = strUserName
            Session("FullName") = strFullName
            strUserName = ""
            strFullName = ""
            Response.Redirect("~/Forms/ViewEquipment.aspx")
        ElseIf switch = 1 Then

        End If
    End Sub

    Protected Sub gvHistory_RowDataBound(sender As Object, e As GridViewRowEventArgs)
        If (e.Row.RowType = DataControlRowType.DataRow) Then
            e.Row.Cells(0).Visible = False
        End If

        If (e.Row.RowType = DataControlRowType.Header) Then
            e.Row.Cells(0).Visible = False
        End If

        If switch = 0 Then
            If e.Row.RowType = DataControlRowType.DataRow Then
                e.Row.Attributes("onclick") = Page.ClientScript.GetPostBackClientHyperlink(gvHistory, "Select$" & e.Row.RowIndex)
                e.Row.ToolTip = "Click to select this row."
            End If
        ElseIf switch = 1 Then

        End If
    End Sub

    Protected Sub btnSearch_Click(sender As Object, e As EventArgs)
        If switch = 0 Then
            getEquipHistory(txtSearch.Text)
        ElseIf switch = 1 Then
            getUserHistory(txtSearch.Text)
        End If
    End Sub

    Private Sub getEquipHistory(param_search As String)
        gvHistory.DataSource = Nothing
        Dim getData As New Inventory
        Dim ds As New DataSet

        ds = getData.getHistory(param_search, "")
        gvHistory.DataSource = ds
        gvHistory.DataBind()
    End Sub

    Protected Sub gvHistory_PageIndexChanging(sender As Object, e As GridViewPageEventArgs)
        gvHistory.PageIndex = e.NewPageIndex
        If switch = 0 Then
            getEquipHistory(txtSearch.Text)
        ElseIf switch = 1 Then
            getUserHistory(txtSearch.Text)
        End If
    End Sub

    Protected Sub btnEquipHistory_Click(sender As Object, e As EventArgs)
        switch = 0
        getEquipHistory(txtSearch.Text)
    End Sub

    Protected Sub btnUserHistory_Click(sender As Object, e As EventArgs)
        switch = 1
        getUserHistory(txtSearch.Text)
    End Sub

    Private Sub getUserHistory(param_search As String)
        gvHistory.DataSource = Nothing
        Dim getData As New Inventory
        Dim ds As New DataSet

        ds = getData.getUserHistory(param_search)
        gvHistory.DataSource = ds
        gvHistory.DataBind()
    End Sub
End Class