Imports System.Data.SqlClient
Public Class dbhelper

    Dim oConnection As SqlConnection
    Dim oCommand As SqlCommand

    Dim oEricConnection As SqlConnection
    Dim oEricCommand As SqlCommand

    Public strConStr2 = ConfigurationManager.ConnectionStrings.Item("ITSGinventory").ToString()

    Public Sub OpenNewConnection()
        oConnection.Open()
    End Sub

    Public Function CallConnection() As SqlConnection
        Dim newConnection As New SqlConnection(strConStr2)
        Return newConnection
    End Function

    Public Function CallCommand() As SqlCommand
        Dim newCommand As New SqlCommand()

        Return newCommand
    End Function

    Public Sub CloseNewConnection()
        oConnection.Close()
        SqlConnection.ClearAllPools()
    End Sub

    Public Function execReader(sQuery As String) As SqlDataReader

        Dim oReader As SqlDataReader = Nothing
        oConnection = New SqlConnection(strConStr2)

        oCommand = New SqlCommand(sQuery, oConnection)

        Try
            OpenNewConnection()
            oReader = oCommand.ExecuteReader()
        Catch ex As Exception
            Console.WriteLine(ex.Message)
        End Try
        Return oReader

        SqlConnection.ClearAllPools()
    End Function

    Public Function GetServerDate() As String
        Dim sQuery As New StringBuilder

        sQuery.Append("SELECT GETDATE() AS ServerDate")

        Dim resultValue As String = ""

        Try
            Dim oreader = execReader(sQuery.ToString())

            While oreader.Read()
                resultValue = oreader("ServerDate").ToString()
            End While

        Catch ex As Exception
            System.Web.HttpContext.Current.Response.Write("<script>alert(" & ControlChars.Quote & ex.Message & " -GetServerDate" & ControlChars.Quote & ");</script>")
        Finally
            CloseNewConnection()
        End Try

        Return resultValue
        SqlConnection.ClearAllPools()
    End Function

    Public Function Connect(ByRef vErr As String) As Boolean
        Connect = False
        Try
            oConnection = New SqlConnection(strConStr2)
            If oConnection.State = ConnectionState.Closed Then oConnection.Open()
            Connect = True
        Catch ex As Exception
            vErr = ex.Message
        End Try
    End Function

    Public Function GetDataSet(ByVal vQuery As String, ByRef vErr As String, Optional ByRef vErrNo As Long = 0) As DataSet
        GetDataSet = Nothing
        If Connect(vErr) Then
            Try
                Using da As New SqlDataAdapter(vQuery, oConnection)
                    Using ds As New DataSet()
                        da.Fill(ds)
                        GetDataSet = ds
                    End Using
                End Using
            Catch ex As Exception
                vErr = ex.Message
                vErrNo = Err.Number
            End Try
        End If
        Call DisConnect()
        SqlConnection.ClearAllPools()
    End Function

    Public Function DisConnect() As Boolean
        DisConnect = False
        Try
            If oConnection.State = ConnectionState.Open Then oConnection.Close()
            DisConnect = True
        Catch ex As Exception
            SqlConnection.ClearAllPools()
        End Try
        SqlConnection.ClearAllPools()
    End Function
End Class
