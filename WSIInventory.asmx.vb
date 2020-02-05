Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports System.ComponentModel
Imports System.Data.SqlClient

' To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line.
' <System.Web.Script.Services.ScriptService()> _
<System.Web.Services.WebService(Namespace:="http://tempuri.org/")> _
<System.Web.Services.WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)> _
<ToolboxItem(False)> _
Public Class WSIInventory
    Inherits System.Web.Services.WebService


    'Public Function HelloWorld() As String
    '    Return "Hello World"
    'End Function

#Region "dbHelper"
    Dim oConnection As SqlConnection
    Dim oCommand As SqlCommand
    ''' <summary>
    ''' Specify the connection String
    ''' </summary>
    Private ConnString = ConfigurationManager.ConnectionStrings.Item("ITSGinventory").ToString()

    Private Sub OpenNewConnection()
        oConnection.Open()
    End Sub

    Public Function CallConnection() As SqlConnection
        Dim newConnection As New SqlConnection(ConnString)
        Return newConnection
    End Function

    Public Function CallCommand() As SqlCommand
        Dim newCommand As New SqlCommand()

        Return newCommand
    End Function

    Private Function execReader(sQuery As String) As SqlDataReader
        Dim oReader As SqlDataReader = Nothing
        oConnection = New SqlConnection(ConnString)

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

    Private Function Connect(ByRef vErr As String) As Boolean
        Connect = False
        Try
            oConnection = New SqlConnection(ConnString)
            If oConnection.State = ConnectionState.Closed Then oConnection.Open()
            Connect = True
        Catch ex As Exception
            vErr = ex.Message
        End Try
    End Function

    Private Function GetDataSet(ByVal vQuery As String, ByRef vErr As String, Optional ByRef vErrNo As Long = 0) As DataSet
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

    Private Function DisConnect() As Boolean
        DisConnect = False
        Try
            If oConnection.State = ConnectionState.Open Then oConnection.Close()
            DisConnect = True
        Catch ex As Exception
            SqlConnection.ClearAllPools()
        End Try
        SqlConnection.ClearAllPools()
    End Function
#End Region

    ''' <summary>
    ''' Retrieves the initial list of names. Can be searched by Accountability Number.
    ''' </summary>
    ''' <param name="param_Acode">Accountability Number</param>
    ''' <returns>Dataset of all the list of names with other details.</returns>
    <WebMethod()>
    Public Function getGridviewList(param_Acode As String) As DataSet
        Dim strQuery As New StringBuilder
        Dim ds As DataSet

        strQuery.Append("DECLARE @T1 TABLE (
                        id int,
                        Assigned_To nvarchar(max), 
                        PC_Type nvarchar(max),
                        Assigned_Date nvarchar(max),
                        DeptName nvarchar(max),
                        Position nvarchar(max),
                        Model nvarchar(max),
                        Status nvarchar(50),
                        aCode nvarchar(50)) 
                        
                        INSERT INTO @T1(id, Assigned_To,PC_Type,Assigned_Date,DeptName,Position, Status, aCode) 

                        SELECT a.id, a.Assigned_To, c.PC_Type, a.Assigned_Date, b.DeptName, a.Position, a.Status, a.aCode
                        FROM tbl_Assigned_To as a
                        INNER JOIN tbl_Departments as b
                        ON a.Department = b.idDept
                        INNER JOIN tbl_Host_Names as c
                        ON c.PC_num = a.PC_num
                        WHERE a.active = 'Yes'
                       
                        DECLARE @i as int = 1 
                        DECLARE @model AS nvarchar(max)
                        DECLARE @assignTo as nvarchar(max)
                        DECLARE @pctype as NVARCHAR(MAX)
						DECLARE @type as NVARCHAR(MAX)

                        WHILE(@i) <= (SELECT COUNT(*) FROM @T1) 
                        BEGIN 
                        SELECT @assignTo = Assigned_To FROM @T1 WHERE id = @i
                        SELECT @pctype = PC_Type FROM @t1 WHERE id = @i
												
						IF @pctype = 'Laptop'							
							SET @type = 'Laptop'							
						ELSE IF @pctype = 'Desktop'						
							SET @type = 'Casing' 

                        SELECT @model = a.Equipment_Description 
                        FROM tbl_Equipments a 
                        INNER JOIN tbl_Accountability b 
                        ON a.Equipment_ID = b.Equipment_ID 
                        INNER JOIN tbl_Assigned_To c 
                        ON b.PC_num = c.PC_num 
                        WHERE c.Assigned_To = @assignTo AND a.Equipment_Type = @type
                        UPDATE @T1 SET Model = @model WHERE id = @i 
                        SET @i = @i + 1 
                        END 

                        SELECT id, Assigned_To as 'Name', PC_Type as 'PC Type',Assigned_Date as 'Assigned Date',DeptName as 'Department Name', Position, Model, Status, aCode as 'Accountability Code' 
                        FROM @T1
                        WHERE id <> 0 
                        ")

        If param_Acode <> "" And param_Acode <> "0" Then
            strQuery.Append("AND aCode = '" & param_Acode & "'")
        End If

        ds = GetDataSet(strQuery.ToString, "")

        If Not ds Is Nothing Then
            getGridviewList = ds
        Else
            getGridviewList = Nothing
        End If
    End Function

    ''' <summary>
    ''' Retrieves the list of equipment assigned to the employee. Pass the Accountability ID from the gridview.
    ''' </summary>
    ''' <param name="param_Acode">ID of the person</param>
    ''' <returns>Dataset containing the equipments assigned to the employee</returns>
    <WebMethod()>
    Public Function getSelectedPersonAccountability(param_Acode As String) As DataSet
        Dim strQuery As New StringBuilder
        Dim ds As DataSet

        strQuery.Append("
                         SELECT a.Equipment_ID, 
                         a.Equipment_Description as Description, 
                         a.Equipment_PPE_Number as 'PPE #', 
                         a.Equipment_Asset_Number As 'Asset #', 
                         a.Equipment_Serial AS 'Serial #'
                         FROM tbl_Equipments as a INNER JOIN 
                         tbl_Accountability as b ON a.Equipment_ID = b.Equipment_ID
                         INNER JOIN tbl_Assigned_To as c on c.PC_num = b.PC_num
                         WHERE c.aCode = '" & param_Acode.Trim & "'
                         ")

        ds = GetDataSet(strQuery.ToString, "")

        If Not ds Is Nothing Then
            getSelectedPersonAccountability = ds
        Else
            getSelectedPersonAccountability = Nothing
        End If
    End Function

    <WebMethod()>
    Public Sub updateRFTnumber(param_RFTnum As String, param_rftDate As String, param_Acode As String)

        Dim strQuery As New StringBuilder
        Dim ds As DataSet

        strQuery.Append("
                         SELECT a.Equipment_ID, 
                         a.Equipment_Type As Type, 
                         a.Equipment_Description as Description, 
                         a.Equipment_PPE_Number as 'PPE #', 
                         a.Equipment_Asset_Number As 'Asset #', 
                         a.Equipment_Serial AS 'Serial #'
                         FROM tbl_Equipments as a INNER JOIN 
                         tbl_Accountability as b ON a.Equipment_ID = b.Equipment_ID
                         INNER JOIN tbl_Assigned_To as d on b.PC_num = d.PC_num
                         WHERE d.aCode = '" & param_Acode & "'
                         ")

        ds = GetDataSet(strQuery.ToString, "")
        strQuery.Clear()

        For x = 0 To ds.Tables(0).Rows.Count
            strQuery.Append("
                          UPDATE tbl_Equipments SET 
                          Equipment_RFT_num = '" & param_RFTnum & "', 
                          Equipment_RFT_Date = '" & param_rftDate & "'
                          WHERE Equipment_ID = '" & ds.Tables(0).Rows(x)(0).ToString() & "'
                          ")
        Next

        strQuery.Append("
                          Declare @pcnum int = null

                          SELECT @pcnum = PC_num FROM tbl_Assigned_To WHERE aCode = '" & param_Acode & "'

                          UPDATE tbl_Host_Names SET RFTnum = '" & param_RFTnum & "', RFTdate = '" & param_rftDate & "'
                          WHERE PC_num = @pcnum                          

                          ")

        Dim oConnection = CallConnection()
        Try
            oConnection.Open()
            Dim oCommand = CallCommand()
            oCommand.Connection = oConnection
            oCommand.CommandText = strQuery.ToString()
            oCommand.CommandType = CommandType.Text
            oCommand.ExecuteNonQuery()
        Catch ex As Exception
            System.Web.HttpContext.Current.Response.Write("<script>alert(" & ControlChars.Quote & ex.Message & " -saveAssign" & ControlChars.Quote & ");</script>")
        Finally
            oConnection.Close()
        End Try
    End Sub

    <WebMethod()>
    Public Function getAccountabilityEquipments(param_Acode As String) As DataSet
        Dim strQuery As New StringBuilder
        Dim ds As DataSet

        strQuery.Append("
                         SELECT 
                         a.Equipment_PPE_Number as 'PPE #', 
                         a.Equipment_Description as Description,   
                         a.Equipment_Serial AS 'Serial #'
                         FROM tbl_Equipments as a INNER JOIN 
                         tbl_Accountability as b ON a.Equipment_ID = b.Equipment_ID    
						 INNER JOIN tbl_Assigned_To as c on b.PC_num=c.PC_num               
                         WHERE c.aCode = '" & param_Acode.Trim & "'
                         ")

        ds = GetDataSet(strQuery.ToString, "")

        If Not ds Is Nothing Then
            getAccountabilityEquipments = ds
        Else
            getAccountabilityEquipments = Nothing
        End If
    End Function

    <WebMethod()>
    Public Sub updateStatusRemarks(param_status As String, param_remarks As String, param_Acode As String)
        Dim strQuery As New StringBuilder
        strQuery.Append("
                          UPDATE tbl_Assigned_To SET Status = '" & param_status & "', Remarks = '" & param_remarks & "'
                          WHERE aCode = '" & param_Acode & "'                                                  
                          ")

        Dim oConnection = CallConnection()
        Try
            oConnection.Open()
            Dim oCommand = CallCommand()
            oCommand.Connection = oConnection
            oCommand.CommandText = strQuery.ToString()
            oCommand.CommandType = CommandType.Text
            oCommand.ExecuteNonQuery()
        Catch ex As Exception
            System.Web.HttpContext.Current.Response.Write("<script>alert(" & ControlChars.Quote & ex.Message & " -saveAssign" & ControlChars.Quote & ");</script>")
        Finally
            oConnection.Close()
        End Try
    End Sub
End Class