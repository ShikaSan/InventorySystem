Public Class Main
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
                    getData()
                    getTypes()
                    getStatus()
                    Me.Master.getUserName(strFullName, strUserName)
                    'Session.Remove("Username")
                    'Session.Remove("FullName")
                    Dim cls As New User_Validation
                    If cls.userLevel(strUserName) = "RO" Then
                        btnAdd.Visible = False
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

    Private Sub getData()
        Dim getData As New Inventory
        Dim ds As New DataSet

        ds = getData.getDataView(txtSearch.Text, ddlType.Text, ddlStatus.Text)
        Dim dv As DataView = ds.Tables(0).DefaultView
        dv.Sort = SortExpression + " " + If(IsAscendingSort, "ASC", "DESC")
        gvView.DataSource = dv
        gvView.DataBind()
    End Sub

    Private Sub getStatus()
        Dim getData As New Inventory
        Dim ds As New DataSet

        ds = getData.getStatus()
        ddlStatus.DataSource = ds
        ddlStatus.DataTextField = "Equipment_Status"
        ddlStatus.DataBind()
        ddlStatus.Items.Insert(0, "")
        ddlStatus.Items(0).Value = 0
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

    Protected Sub btnSearch_Click(sender As Object, e As EventArgs)
        getData()
    End Sub

    Protected Sub gvView_SelectedIndexChanged(sender As Object, e As EventArgs)
        Session("id") = gvView.SelectedRow.Cells(0).Text
        'Session("Username") = strUserName
        'Session("FullName") = strFullName
        strUserName = ""
        strFullName = ""
        Response.Redirect("~/Forms/ViewEquipment.aspx")
    End Sub

    Protected Sub gvView_RowDataBound(sender As Object, e As GridViewRowEventArgs)
        'If (e.Row.RowType = DataControlRowType.DataRow) Then
        '    e.Row.Cells(0).Visible = False
        'End If

        'If (e.Row.RowType = DataControlRowType.Header) Then
        '    e.Row.Cells(0).Visible = False
        'End If

        If e.Row.RowType = DataControlRowType.DataRow Then
            e.Row.Attributes("onclick") = Page.ClientScript.GetPostBackClientHyperlink(gvView, "Select$" & e.Row.RowIndex)
            e.Row.ToolTip = "Click to select this row."
        End If
    End Sub

    Protected Sub ddlStatus_SelectedIndexChanged(sender As Object, e As EventArgs)
        getData()
    End Sub

    Protected Sub ddlType_SelectedIndexChanged(sender As Object, e As EventArgs)
        getData()
    End Sub

    Protected Sub btnAdd_Click(sender As Object, e As EventArgs)
        'Session("Username") = strUserName
        'Session("FullName") = strFullName
        strUserName = ""
        strFullName = ""
        Response.Redirect("~/Forms/AddEquipment.aspx")
    End Sub

    Protected Sub gvView_PageIndexChanging(sender As Object, e As GridViewPageEventArgs)
        gvView.PageIndex = e.NewPageIndex
        getData()
    End Sub

    Protected Sub gvView_Sorting(sender As Object, e As GridViewSortEventArgs)
        If e.SortExpression = SortExpression Then
            IsAscendingSort = Not IsAscendingSort
        Else
            SortExpression = e.SortExpression
        End If
        getData()
    End Sub

    Protected Property SortExpression As String
        Get
            Dim value As Object = ViewState("SortExpression")
            Return If(Not IsNothing(value), CStr(value), "id")
        End Get
        Set(value As String)
            ViewState("SortExpression") = value
        End Set
    End Property

    Protected Property IsAscendingSort As Boolean
        Get
            Dim value As Object = ViewState("IsAscendingSort")
            Return If(Not IsNothing(value), CBool(value), False)
        End Get
        Set(value As Boolean)
            ViewState("IsAscendingSort") = value
        End Set
    End Property
End Class