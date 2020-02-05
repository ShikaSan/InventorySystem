Imports System.DirectoryServices

Public Class Authentication
    Public Shared Function AuthenticateDomain(ByVal UserName As String, ByVal Password As String) As Boolean
        Dim boolResultValue = False
        Dim strDomainName = "wordtext.ph"
        Dim dirEntry = New DirectoryEntry()
        Dim dirSearcher = New DirectorySearcher()

        Try
            dirEntry = New DirectoryEntry("LDAP://" & strDomainName, UserName.ToLower(), Password)
            dirSearcher = New DirectorySearcher(dirEntry)
            dirSearcher.Filter = "(samAccountName=" & UserName.ToLower() & ")"
            Dim sr = dirSearcher.FindOne()
            If sr.Equals(Nothing) Then
                boolResultValue = False
                Return boolResultValue
            Else
                boolResultValue = True
            End If
        Catch
            strDomainName = "itsdi.com.ph"
            dirEntry = New DirectoryEntry("LDAP://" & strDomainName, UserName.ToLower(), Password)
            dirSearcher = New DirectorySearcher(dirEntry)
            dirSearcher.Filter = "(samAccountName=" & UserName.ToLower() & ")"
            Dim SR = dirSearcher.FindOne()
            If SR.Equals(Nothing) Then
                boolResultValue = False
                Return boolResultValue
            Else
                boolResultValue = True
            End If
        End Try

        Return boolResultValue
    End Function

    Public Shared Function RetrieveData(ByVal UserName As String) As DirectoryEntry
        Dim ResultValue = New DirectoryEntry()
        Dim strDomainName = "wordtext.ph"
        Dim dirEntry = New DirectoryEntry()
        Dim dirSearcher = New DirectorySearcher()

        Try
            dirEntry = New DirectoryEntry("LDAP://" & strDomainName)
            dirSearcher = New DirectorySearcher(dirEntry)
            dirSearcher.Filter = "(samAccountName=" & UserName.ToLower() & ")"
            dirSearcher.PropertiesToLoad.Add("GivenName")
            dirSearcher.PropertiesToLoad.Add("Mail")
            dirSearcher.PropertiesToLoad.Add("sn")
            Dim sr = dirSearcher.FindOne()
            If sr.Equals(Nothing) Then
                ResultValue = New DirectoryEntry()
                Return ResultValue
            Else
                Dim de = sr.GetDirectoryEntry()
                'If de.Properties("Mail").Value IsNot Nothing Then
                '    Session.Add("Email", de.Properties("Mail").Value.ToString())
                'End If

                'If de.Properties("GivenName").Value IsNot Nothing Then
                '    Session.Add("GivenName", de.Properties("GivenName").Value.ToString())
                'End If

                'If de.Properties("sn").Value IsNot Nothing Then
                '    Session.Add("sn", de.Properties("sn").Value.ToString())
                'End If

                ResultValue = de
            End If
        Catch
            Try
                strDomainName = "itsdi.com.ph"
                dirEntry = New DirectoryEntry("LDAP://" & strDomainName)
                dirSearcher = New DirectorySearcher(dirEntry)
                dirSearcher.Filter = "(samAccountName=" & UserName.ToLower() & ")"
                dirSearcher.PropertiesToLoad.Add("GivenName")
                dirSearcher.PropertiesToLoad.Add("Mail")
                dirSearcher.PropertiesToLoad.Add("sn")
                dirSearcher.PropertiesToLoad.Add("title")
                dirSearcher.PropertiesToLoad.Add("department")
                Dim SR = dirSearcher.FindOne()
                If SR.Equals(Nothing) Then
                    ResultValue = New DirectoryEntry()
                    Return ResultValue
                Else
                    Dim de = SR.GetDirectoryEntry()
                    ResultValue = de
                End If
            Catch ex As Exception
                ResultValue = New DirectoryEntry()
            End Try
        End Try

        Return ResultValue
    End Function
End Class
