Public Class pageMaster
    Inherits System.Web.UI.MasterPage
    Private Shared strUserName As String = Nothing
    Private Shared strFullName As String = Nothing
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            'Dim i As Integer = 0
            'While i < Session.Contents.Count
            '    If Session.Keys(i).ToString() = "Username" Then
            '        strUserName = Session("Username").ToString()
            '    ElseIf Session.Keys(i).ToString() = "FullName" Then
            '        strFullName = Session("FullName").ToString()
            '    End If
            '    i += 1
            'End While

            Dim cls As New User_Validation
            If cls.userLevel(strUserName) = "RO" Then
                btnHistory.Visible = False
                btnMaintenance.Visible = False
                'btnReports.Visible = False
            End If
        End If
    End Sub

    Protected Sub btnHome_Click(sender As Object, e As EventArgs)
        'Session("Username") = strUserName
        'Session("FullName") = strFullName
        'strUserName = ""
        'strFullName = ""
        Response.Redirect("~/Forms/Main.aspx")
    End Sub

    Protected Sub btnAssignedEq_Click(sender As Object, e As EventArgs)
        'Session("Username") = strUserName
        'Session("FullName") = strFullName
        'strUserName = ""
        'strFullName = ""
        Response.Redirect("~/Forms/UserAccountability.aspx")
    End Sub
    Protected Sub btnHistory_Click(sender As Object, e As EventArgs)
        'Session("Username") = strUserName
        'Session("FullName") = strFullName
        'strUserName = ""
        'strFullName = ""
        Response.Redirect("~/Forms/History.aspx")
    End Sub

    'Protected Sub btnReports_Click(sender As Object, e As EventArgs)
    '    'Session("Username") = strUserName
    '    'Session("FullName") = strFullName
    '    'strUserName = ""
    '    'strFullName = ""
    '    Response.Redirect("~/Forms/Reports.aspx")
    'End Sub

    Protected Sub btnLogout_Click(sender As Object, e As EventArgs)
        Session.Clear()
        Session.RemoveAll()
        strUserName = ""
        strFullName = ""
        Response.Redirect("~/Forms/Login.aspx")
    End Sub

    Public Sub getUserName(param_name As String, param_username As String)
        strUserName = param_username
        strFullName = param_name
        lblName.Text = param_name
    End Sub

    Protected Sub btnMaintenance_Click(sender As Object, e As ImageClickEventArgs)
        Response.Redirect("~/Forms/Maintenance.aspx")
    End Sub

    Protected Sub btnRepairPage_Click(sender As Object, e As EventArgs) Handles btnRepairPage.Click
        Response.Redirect("~/Forms/ForRepair.aspx")
    End Sub
End Class