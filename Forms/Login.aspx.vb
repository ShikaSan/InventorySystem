Imports System.DirectoryServices
Imports InventorySystem.Authentication

Public Class Login
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Session.RemoveAll()
    End Sub

    Protected Sub btnSignIn_Click(sender As Object, e As EventArgs) Handles btnLogin.Click

        If AuthenticateDomain(txtUserName.Text.ToLower(), txtPassword.Text) Then
            Dim cls As New Inventory
            Dim oUserValidate As New User_Validation
            If oUserValidate.domainValidation(txtUserName.Text) >= 1 Then
                Dim data = RetrieveData(txtUserName.Text.ToLower())
                If data.Properties("Mail").Value IsNot Nothing Then
                    Session.Add("Email", data.Properties("Mail").Value.ToString())
                End If

                If data.Properties("GivenName").Value IsNot Nothing Then
                    Session.Add("GivenName", data.Properties("GivenName").Value.ToString())
                End If

                If data.Properties("sn").Value IsNot Nothing Then
                    Session.Add("sn", data.Properties("sn").Value.ToString())
                End If

                Session.Add("Username", txtUserName.Text)
                Session.Add("FullName", Session("GivenName").ToString() & " " & Session("sn").ToString())
                cls.saveUserHistory("logged in", Session("GivenName").ToString() & " " & Session("sn").ToString(), "")
                Session.Remove("GivenName")
                Session.Remove("sn")
                Session.Remove("Email")
                Response.Redirect("~/Forms/Main.aspx")
            Else
                lblWarningMessage.Visible = True
                lblWarningMessage.Text = "Access Denied!"
            End If
        Else
            pnlWarningMessage.Visible = True
            'lblWarningMessage.Visible = True
            lblWarningMessage.Text = "user/password is incorrect!"
            Response.Redirect("~/Forms/Login.aspx")
        End If

    End Sub

End Class