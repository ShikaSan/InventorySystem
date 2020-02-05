
Partial Class Print
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'Dim btn2 As Button = New Button With {
        '    .ID = "btnEdit",
        '    .Text = "Edit Member"
        '}
        'AddHandler btn2.Click, AddressOf Me.btnEdit_Click
        'form1.Controls.Add(btn2)

        Dim ctrl As Control = CType(Session("ctrl"), Control)
        PrintHelper.PrintWebControl(ctrl)

    End Sub

    Protected Sub btnEdit_Click(sender As Object, e As EventArgs)
        Response.Redirect("~/Forms/UserAccountability.aspx")
    End Sub
End Class
