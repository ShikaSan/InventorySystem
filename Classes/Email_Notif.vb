Imports System.Net
Imports Microsoft.Exchange.WebServices.Data
Public Class Email_Notif : Inherits dbhelper

    Public Sub SendAssignNotification(strType As String, strDesc As String, strPPE As String, strAsset As String,
                                      strSerial As String, strDate As String, strRemarks As String)
        Dim service As New ExchangeService(ExchangeVersion.Exchange2007_SP1)
        service.Credentials = New NetworkCredential("SOMSExchSVC", "Imper$0n@t", "wordtext.ph")
        service.AutodiscoverUrl("SOMSExchangeService@wsiphil.com.ph", AddressOf adAutoDiscoCallBack)

        service.ImpersonatedUserId = New ImpersonatedUserId(ConnectingIdType.SmtpAddress, "JohnlordJoseph.Villas@wsiphil.com.ph")

        service.TraceEnabled = True
        service.TraceFlags = TraceFlags.All

        Dim email As New EmailMessage(service)

        email.Importance = Importance.High
        email.Subject = "ITSG Inventory"

        email.ToRecipients.Add("JohnlordJoseph.Villas@wsiphil.com.ph")
        email.ToRecipients.Add("boy.palencia@wsiphil.com.ph")
        email.ToRecipients.Add("Matthew.Lizares@wsiphil.com.ph")
        'email.CcRecipients.Add(param_CCTo)

        'email.BccRecipients.Add("JohnlordJoseph.Villas@wsiphil.com.ph")

        Dim body As String = String.Empty

        body = My.Resources.NewEquipment

        body = body.Replace("strType", strType)
        body = body.Replace("strDesc", strDesc)
        body = body.Replace("strPPE", strPPE)
        body = body.Replace("strAsset", strAsset)
        body = body.Replace("strSerial", strSerial)
        body = body.Replace("strDate", strDate)
        body = body.Replace("strRemarks", strRemarks)

        email.Body = New MessageBody(BodyType.HTML, body)
        email.SendAndSaveCopy()

    End Sub

    Public Sub SendMultipleRows(strName As String, strDept As String)
        Dim service As New ExchangeService(ExchangeVersion.Exchange2007_SP1)
        service.Credentials = New NetworkCredential("SOMSExchSVC", "Imper$0n@t", "wordtext.ph")
        service.AutodiscoverUrl("SOMSExchangeService@wsiphil.com.ph", AddressOf adAutoDiscoCallBack)

        service.ImpersonatedUserId = New ImpersonatedUserId(ConnectingIdType.SmtpAddress, "JohnlordJoseph.Villas@wsiphil.com.ph")

        service.TraceEnabled = True
        service.TraceFlags = TraceFlags.All

        Dim email As New EmailMessage(service)

        email.Importance = Importance.High
        email.Subject = "ITSG Inventory"

        email.ToRecipients.Add("JohnlordJoseph.Villas@wsiphil.com.ph")
        'email.ToRecipients.Add("boy.palencia@wsiphil.com.ph")
        'email.ToRecipients.Add("Zaramil.Leyte@wsiphil.com.ph")
        email.ToRecipients.Add("Matthew.Lizares@wsiphil.com.ph")
        'email.CcRecipients.Add(param_CCTo)

        'email.BccRecipients.Add("JohnlordJoseph.Villas@wsiphil.com.ph")

        Dim body As String = String.Empty
        body = body + vbNewLine + "<div style=" & ControlChars.Quote & "border-bottom: 3px solid #1e8cdb;width:100%" & ControlChars.Quote & ">" +
        "<H2 style=" & ControlChars.Quote & "font-family:'Segoe UI';" &
            ControlChars.Quote & ">ITSG Inventory - Assigned Equipment</H2></div>" +
            vbNewLine + vbNewLine + vbNewLine + "<p style=" & ControlChars.Quote & "font-family:'Segoe UI';font-size:15px" &
            ControlChars.Quote & "> An equipment has been assigned to <b>" & strName & "</b> of <b>" & getDeptName(strDept) & " Department</b>.</p>"

        Dim ds As New DataSet
        Dim getData As New View_Accountability

        Dim builder As New StringBuilder
        builder.Append("<br>")
        builder.Append("<center>")
        builder.Append("<table cellpadding = " & ControlChars.Quote & "0" & ControlChars.Quote & " cellspacing = " & ControlChars.Quote & "0" & ControlChars.Quote & " style=" & ControlChars.Quote & "width:100%; font-family:'Segoe UI';padding:4px 4px 4px 4px;margin:0px 0px 0px 0px;" & ControlChars.Quote & ">")
        builder.Append("<tr>")
        builder.Append("<td>")
        builder.Append("Type")
        builder.Append("</td>")
        builder.Append("<td>")
        builder.Append("Description")
        builder.Append("</td>")
        builder.Append("<td>")
        builder.Append("PPE #")
        builder.Append("</td>")
        builder.Append("<td>")
        builder.Append("Asset #")
        builder.Append("</td>")
        builder.Append("<td>")
        builder.Append("Serial #")
        builder.Append("</td>")
        builder.Append("<td>")
        builder.Append("Remarks")
        builder.Append("</td>")
        builder.Append("<td>")
        builder.Append("Date Acquired")
        builder.Append("</td>")
        builder.Append("</tr>")

        'System.IO.File.WriteAllText("File.html", builder.ToString())



        ds = getData.getAllItems(strName, "")

        For x = 0 To ds.Tables(0).Rows.Count - 1
            'builder.Append("<tr>")
            'builder.Append("<td>")
            'builder.Append(getDeptName(strDept))
            'builder.Append("</td>")
            'builder.Append("<td>")
            'builder.Append(strName)
            'builder.Append("</td>")
            For y = 1 To ds.Tables(0).Columns.Count - 1
                builder.Append("<td>")
                builder.Append(ds.Tables(0).Rows(x)(y).ToString())
                builder.Append("</td>")
            Next
            builder.Append("</tr>")
        Next

        builder.Append("</table>")
        builder.Append("</center>")
        body = body + builder.ToString()

        email.Body = New MessageBody(BodyType.HTML, body)
        email.SendAndSaveCopy()

    End Sub


    Friend Shared Function adAutoDiscoCallBack(redirectionUrl As String) As Boolean
        ' The default for the validation callback is to reject the URL.
        Dim result As Boolean = False

        Dim redirectionUri As New Uri(redirectionUrl)

        ' Validate the contents of the redirection URL. In this simple validation
        ' callback, the redirection URL is considered valid if it is using HTTPS
        ' to encrypt the authentication credentials. 
        If redirectionUri.Scheme = "https" Then
            result = True
        End If

        Return result
    End Function

    Private Function getDeptName(param_search As String) As String
        Dim sQuery As New StringBuilder

        sQuery.Append("SELECT DeptName FROM tbl_Departments WHERE idDept = '" & param_search & "'")

        Dim resultValue As String = ""

        Try
            Dim oreader = execReader(sQuery.ToString())

            While oreader.Read()
                resultValue = oreader("DeptName").ToString()
            End While

        Catch ex As Exception
            System.Web.HttpContext.Current.Response.Write("<script>alert(" & ControlChars.Quote & ex.Message & " -getDeptName" & ControlChars.Quote & ");</script>")
        Finally
            CloseNewConnection()
        End Try

        Return resultValue

    End Function
End Class
