Public Class User_Validation : Inherits dbhelper
    Public Function domainValidation(strValue As String) As Integer
        Dim sQuery As New StringBuilder
        Dim count As Integer = 0
        sQuery.Append("SELECT * ")
        sQuery.Append("FROM tbl_User_Access ")
        sQuery.Append("WHERE domain = '" & strValue & "' ")

        Try
            Dim oreader = execReader(sQuery.ToString())

            While oreader.Read()
                count = count + 1
            End While

        Catch ex As Exception
            System.Web.HttpContext.Current.Response.Write("<script>alert(" & ControlChars.Quote & ex.Message & " -domainv" & ControlChars.Quote & ");</script>")
        Finally
            CloseNewConnection()
        End Try

        Return count
    End Function

    Public Function userLevel(strValue As String) As String
        Dim sQuery As New StringBuilder
        Dim ul As String = ""
        sQuery.Append("SELECT user_level ")
        sQuery.Append("FROM tbl_User_Access ")
        sQuery.Append("WHERE domain = '" & strValue & "' ")

        Try
            Dim oreader = execReader(sQuery.ToString())

            While oreader.Read()
                ul = oreader("user_level").ToString()
            End While

        Catch ex As Exception
            System.Web.HttpContext.Current.Response.Write("<script>alert(" & ControlChars.Quote & ex.Message & " -ulv" & ControlChars.Quote & ");</script>")
        Finally
            CloseNewConnection()
        End Try

        Return ul
    End Function
End Class
