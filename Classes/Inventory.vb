Imports InventorySystem.Inventory_Model

Public Class Inventory : Inherits dbhelper
    Public Function getDataView(param_search As String, param_type As String, param_status As String) As DataSet
        Dim result = New DataSet
        Dim strQuery As New StringBuilder
        Dim ds As DataSet

        strQuery.Append("SELECT Equipment_ID as ID, Equipment_Type As Type, Equipment_Description as Description, Equipment_PPE_Number as 'PPE #',Equipment_Serial AS 'Serial #',Equipment_Asset_Number As 'Asset #', Equipment_Status as Status FROM tbl_Equipments WHERE Equipment_ID <> '0' ")
        If param_type <> "0" And param_type <> "" Then
            strQuery.Append("AND Equipment_Type = '" & param_type.Trim & "'")
        End If
        If param_status <> "0" And param_status <> "" Then
            strQuery.Append("AND Equipment_Status = '" & param_status.Trim & "'")
        End If
        If param_search <> "" Then
            strQuery.Append("AND Equipment_Description LIKE '%" & param_search.Trim & "%' 
                        OR Equipment_PPE_Number LIKE '%" & param_search.Trim & "%' 
                        OR Equipment_Asset_Number LIKE '%" & param_search.Trim & "%' 
                        OR Equipment_Serial LIKE '%" & param_search.Trim & "%' 
                        OR Equipment_ID LIKE '%" & param_search.Trim & "%' ")
        End If

        ds = GetDataSet(strQuery.ToString, "")

        If Not ds Is Nothing Then
            result = ds
        Else
            result = Nothing
        End If
        Return result
    End Function

    Public Function getUserAccountability(param_id As String) As DataSet
        Dim result = New DataSet()
        Dim strQuery As New StringBuilder
        Dim ds As DataSet

        'strQuery.Append("SELECT c.Host_Name as 'Host Name', a.Assigned_To as 'Assigned To', c.PC_Type as 'Type',
        '                a.Assigned_Date AS 'Assigned Date', b.DeptName, a.Department, a.OS, a.Position, c.RFTnum, c.RFTdate
        '                FROM tbl_Assigned_To as a
        '                INNER JOIN tbl_Departments as b
        '                ON a.Department = b.idDept
        '                INNER JOIN tbl_Host_Names as c
        '                ON c.PC_num = a.PC_num
        '                WHERE a.active = 'Yes' AND a.id LIKE '%" & param_id.Trim & "%' ")

        strQuery.Append("SELECT c.Host_Name as 'Host Name', a.Assigned_To as 'Assigned To',
                        a.Assigned_Date AS 'Assigned Date', a.Department, a.OS, a.Position, c.RFTnum, c.RFTdate
                        FROM tbl_Assigned_To as a
                        INNER JOIN tbl_Host_Names as c
                        ON c.PC_num = a.PC_num
                        WHERE a.active = 'Yes' AND a.id LIKE '%" & param_id.Trim & "%' ")

        ds = GetDataSet(strQuery.ToString, "")

        If Not ds Is Nothing Then
            result = ds
        Else
            result = Nothing
        End If
        Return result
    End Function

    Public Function getUAlist(param_name As String, param_dept As String, param_pos As String) As DataSet
        Dim result = New DataSet
        Dim strQuery As New StringBuilder
        Dim ds As DataSet

        'DO NOT UNCOMMENT
        'strQuery.Append("SELECT a.Assigned_To as 'Name', c.PC_Type as 'Type',
        '                a.Assigned_Date AS 'Assigned Date', b.DeptName, a.Position
        '                FROM tbl_Assigned_To as a
        '                INNER JOIN tbl_Departments as b
        '                ON a.Department = b.idDept
        '                INNER JOIN tbl_Host_Names as c
        '                ON c.PC_num = a.PC_num
        '                WHERE a.active = 'Yes' ")

        'strQuery.Append("DECLARE @T1 TABLE (
        '                id int,
        '                Assigned_To nvarchar(max), 
        '                PC_Type nvarchar(max),
        '                Assigned_Date nvarchar(max),
        '                DeptName nvarchar(max),
        '                Position nvarchar(max),
        '                Model nvarchar(max),
        '                Status nvarchar(50)) 

        '                INSERT INTO @T1(id, Assigned_To,PC_Type,Assigned_Date,DeptName,Position, Status) 

        '                SELECT a.id, a.Assigned_To, c.PC_Type, a.Assigned_Date, b.DeptName, a.Position, a.Status
        '                FROM tbl_Assigned_To as a
        '                INNER JOIN tbl_Departments as b
        '                ON a.Department = b.idDept
        '                INNER JOIN tbl_Host_Names as c
        '                ON c.PC_num = a.PC_num
        '                WHERE a.active = 'Yes' ")
        'DO NOT UNCOMMENT

        strQuery.Append("SELECT a.id, a.Assigned_To AS 'Name', 
                         CASE WHEN c.Equipment_Type = 'Casing'
                         THEN 'Desktop' 
                         ELSE 'Laptop'
                         END AS 'Type', 
                         a.Assigned_Date AS 'Assigned Date', a.Department AS 'Department', a.Position AS 'Position', c.Equipment_Description AS 'Model'
                         FROM tbl_Assigned_To a INNER JOIN tbl_Accountability b on b.PC_num = a.PC_num
                         INNER JOIN tbl_Equipments c on b.Equipment_ID = c.Equipment_ID WHERE (a.active = 'Yes')")

        'If param_dept <> "0" And param_dept <> "" Then
        '    strQuery.Append("AND b.idDept = '" & param_dept.Trim & "' ")
        'End If

        If param_dept <> "" Then
            strQuery.Append("AND a.Department LIKE '%" & param_dept.Trim & "%' ") '" & param_dept.Trim & "' ")
        End If

        '  strQuery.Append("DECLARE @i as int = 1 
        '                  DECLARE @model AS nvarchar(max)
        '                  DECLARE @assignTo as nvarchar(max)
        '                  DECLARE @pctype as NVARCHAR(MAX)
        'DECLARE @type as NVARCHAR(MAX)

        '                  WHILE(@i) <= (SELECT COUNT(*) FROM @T1) 
        '                  BEGIN 
        '                  SELECT @assignTo = Assigned_To FROM @T1 WHERE id = @i
        '                  SELECT @pctype = PC_Type FROM @t1 WHERE id = @i

        'IF @pctype = 'Laptop'							
        '	SET @type = 'Laptop'							
        'ELSE IF @pctype = 'Desktop'						
        '	SET @type = 'Casing' 

        '                  SELECT @model = a.Equipment_Description 
        '                  FROM tbl_Equipments a 
        '                  INNER JOIN tbl_Accountability b 
        '                  ON a.Equipment_ID = b.Equipment_ID 
        '                  INNER JOIN tbl_Assigned_To c 
        '                  ON b.PC_num = c.PC_num 
        '                  WHERE c.Assigned_To = @assignTo AND a.Equipment_Type = @type
        '                  UPDATE @T1 SET Model = @model WHERE id = @i 
        '                  SET @i = @i + 1 
        '                  END 

        '                  SELECT id, Assigned_To as 'Name', PC_Type as 'PC Type',Assigned_Date as 'Assigned Date',DeptName as 'Department Name', Position, Model, Status FROM @T1 WHERE id <> 0 ")

        If param_name <> "" And param_name <> "0" Then
            strQuery.Append("AND a.Assigned_To LIKE LOWER('%" & param_name.Trim & "%') ")
        End If
        If param_pos <> "" And param_pos <> "0" Then
            strQuery.Append("AND a.Position LIKE LOWER('%" & param_pos.Trim & "%') ")
        End If

        strQuery.Append("AND c.Equipment_Type = 'Laptop' OR c.Equipment_Type = 'Casing' ")

        ds = GetDataSet(strQuery.ToString, "")

        If Not ds Is Nothing Then
            result = ds
        Else
            result = Nothing
        End If
        Return result
    End Function

    Public Function getDataEquipment(param_search As String) As DataSet
        Dim result = New DataSet
        Dim strQuery As New StringBuilder
        Dim ds As DataSet

        strQuery.Append("SELECT a.Equipment_Type, a.Equipment_Description, a.Equipment_PPE_Number, a.Equipment_Serial, 
                         a.Equipment_Asset_Number, a.Equipment_Date_Acquired, a.Equipment_RFT_num, a.Equipment_RFT_Date, 
                         a.Equipment_Remarks, a.Equipment_Status, c.Assigned_To, a.Equipment_Location,c.OS,d.Host_Name
                         FROM tbl_Equipments as a 
						 LEFT JOIN tbl_Accountability as b 
						 ON b.Equipment_ID = a.Equipment_ID
                         LEFT JOIN tbl_Assigned_To as c 
                         ON b.PC_num = c.PC_num
						 LEFT JOIN tbl_Host_Names as d
						 ON c.PC_num = d.PC_num
                         WHERE a.Equipment_ID = '" & param_search.Trim & "'")

        ds = GetDataSet(strQuery.ToString, "")

        If Not ds Is Nothing Then
            result = ds
        Else
            result = Nothing
        End If
        Return result
    End Function

    Public Function getStatus() As DataSet
        Dim result = New DataSet
        Dim strQuery As New StringBuilder
        Dim ds As DataSet

        strQuery.Append("SELECT DISTINCT Equipment_Status FROM tbl_Equipments")

        ds = GetDataSet(strQuery.ToString, "")

        If Not ds Is Nothing Then
            result = ds
        Else
            result = Nothing
        End If
        Return result
    End Function

    Public Function getDept() As DataSet
        Dim result = New DataSet
        Dim strQuery As New StringBuilder
        Dim ds As DataSet

        strQuery.Append("SELECT idDept ,DeptName FROM tbl_Departments ORDER BY DeptName ASC ")

        ds = GetDataSet(strQuery.ToString, "")

        If Not ds Is Nothing Then
            result = ds
        Else
            result = Nothing
        End If
        Return result
    End Function

    Public Function getPos() As DataSet
        Dim result = New DataSet
        Dim strQuery As New StringBuilder
        Dim ds As DataSet

        strQuery.Append("SELECT DISTINCT Position FROM tbl_Assigned_To")

        ds = GetDataSet(strQuery.ToString, "")

        If Not ds Is Nothing Then
            result = ds
        Else
            result = Nothing
        End If
        Return result
    End Function

    Public Function getEquipments(param_search As String) As DataSet
        Dim result = New DataSet
        Dim strQuery As New StringBuilder
        Dim ds As DataSet

        strQuery.Append("SELECT Equipment_PPE_Number ,Equipment_Serial ,Equipment_Asset_Number ,Equipment_OS FROM tbl_Equipments")
        If param_search <> "" Then
            strQuery.Append("WHERE Equipment_Description LIKE '%" & param_search.Trim & "%'")
        End If

        ds = GetDataSet(strQuery.ToString, "")

        If Not ds Is Nothing Then
            result = ds
        Else
            result = Nothing
        End If
        Return result
    End Function

    Public Function getTypes() As DataSet
        Dim result = New DataSet
        Dim strQuery As New StringBuilder
        Dim ds As DataSet

        strQuery.Append("SELECT EQType FROM tbl_Type ORDER BY EQType ASC")

        ds = GetDataSet(strQuery.ToString, "")

        If Not ds Is Nothing Then
            result = ds
        Else
            result = Nothing
        End If
        Return result
    End Function

    Public Function getDescription(param_search As String) As DataSet
        Dim result = New DataSet
        Dim strQuery As New StringBuilder
        Dim ds As DataSet

        strQuery.Append("SELECT DISTINCT Equipment_Description FROM tbl_Equipments ")

        If param_search <> "" Then
            strQuery.Append("WHERE Equipment_Type = '" & param_search.Trim & "'")
        End If
        strQuery.Append("ORDER BY Equipment_Description ASC")

        ds = GetDataSet(strQuery.ToString, "")

        If Not ds Is Nothing Then
            result = ds
        Else
            result = Nothing
        End If
        Return result
    End Function

    Public Function getItemDetails(param_desc As String, param_search As String) As DataSet
        Dim result = New DataSet
        Dim strQuery As New StringBuilder
        Dim ds As DataSet

        strQuery.Append("SELECT Equipment_ID, Equipment_PPE_Number ,Equipment_Serial ,Equipment_Asset_Number 
                         FROM tbl_Equipments
                         WHERE Equipment_Description = '" & param_desc.Trim & "'                         
                         ")
        If param_search <> "" Then
            strQuery.Append("AND Equipment_Description LIKE '%" & param_search.Trim & "%' 
                        OR Equipment_PPE_Number LIKE '%" & param_search.Trim & "%' 
                        OR Equipment_Asset_Number LIKE '%" & param_search.Trim & "%' 
                        OR Equipment_Serial LIKE '%" & param_search.Trim & "%' 
                        OR Equipment_ID LIKE '%" & param_search.Trim & "%' ")
        End If
        ds = GetDataSet(strQuery.ToString, "")

        If Not ds Is Nothing Then
            result = ds
        Else
            result = Nothing
        End If
        Return result
    End Function

    Public Function getItemDetailsOnAdd(param_desc As String, param_search As String) As DataSet
        Dim result = New DataSet
        Dim strQuery As New StringBuilder
        Dim ds As DataSet

        strQuery.Append("SELECT Equipment_ID, Equipment_PPE_Number ,Equipment_Serial ,Equipment_Asset_Number 
                         FROM tbl_Equipments
                         WHERE Equipment_Status = 'On Stock' AND Equipment Equipment_Description = '" & param_desc.Trim & "'                         
                         ")
        If param_search <> "" Then
            strQuery.Append("AND Equipment_Description LIKE '%" & param_search.Trim & "%' 
                        OR Equipment_PPE_Number LIKE '%" & param_search.Trim & "%' 
                        OR Equipment_Asset_Number LIKE '%" & param_search.Trim & "%' 
                        OR Equipment_Serial LIKE '%" & param_search.Trim & "%' 
                        OR Equipment_ID LIKE '%" & param_search.Trim & "%' ")
        End If
        ds = GetDataSet(strQuery.ToString, "")

        If Not ds Is Nothing Then
            result = ds
        Else
            result = Nothing
        End If
        Return result
    End Function

    Public Function getHistory(param_search As String, param_id As String) As DataSet
        Dim result = New DataSet
        Dim strQuery As New StringBuilder
        Dim ds As DataSet

        strQuery.Append("SELECT b.Equipment_ID AS ID, b.Equipment_Type as Type, b.Equipment_Description as Description, b.Equipment_PPE_Number AS 'PPE #',
                         b.Equipment_Serial as 'Serial #', b.Equipment_Asset_Number as 'Asset #', a.History_Assigned_To as 'Assigned To', a.History_Location as 'Location', a.History_Status as 'Status', a.History_Assigned_Date as 'Assigned Date'
                         FROM dbo.tbl_History AS a INNER JOIN
                         dbo.tbl_Equipments AS b ON a.History_Equipment_ID = b.Equipment_ID
                         WHERE History_ID <> 0
                        ")

        If param_search <> "" Then
            strQuery.Append(" AND a.History_Assigned_To LIKE '%" & param_search.Trim & "%'")
        End If
        If param_id <> "" Then
            strQuery.Append(" AND b.Equipment_ID = '" & param_id.Trim & "'")
        End If
        strQuery.Append(" ORDER BY a.History_Assigned_Date ASC")
        ds = GetDataSet(strQuery.ToString, "")

        If Not ds Is Nothing Then
            result = ds
        Else
            result = Nothing
        End If
        Return result
    End Function

    Public Function getUserHistory(param_search As String) As DataSet
        Dim result = New DataSet
        Dim strQuery As New StringBuilder
        Dim ds As DataSet

        strQuery.Append("SELECT id
                        ,name as Name
                        ,message as 'Action' 
                        ,timestamp as 'Date Time'
                        FROM tbl_User_History
                        ")

        If param_search <> "" Then
            strQuery.Append("
                            WHERE name LIKE '%" & param_search.Trim & "%' 
                            ")
        End If

        strQuery.Append("ORDER BY timestamp DESC")

        ds = GetDataSet(strQuery.ToString, "")

        If Not ds Is Nothing Then
            result = ds
        Else
            result = Nothing
        End If
        Return result
    End Function

    Public Function saveNewEquip(obj As Inventory_Model) As Boolean
        Dim send As New Email_Notif
        Dim resultValue As Boolean = False

        Dim oConnection = CallConnection()
        Try
            oConnection.Open()
            Dim oCommand = CallCommand()
            oCommand.Connection = oConnection
            oCommand.CommandText = "spSave"
            oCommand.CommandType = CommandType.StoredProcedure
#Region "insert"
            oCommand.Parameters.AddWithValue("Type", obj.Type.Trim)
            oCommand.Parameters.AddWithValue("Desc", obj.Desc.Trim)
            oCommand.Parameters.AddWithValue("PPE", obj.PPE.Trim)
            oCommand.Parameters.AddWithValue("Asset", obj.Asset.Trim)
            oCommand.Parameters.AddWithValue("Serial", obj.Serial.Trim)
            oCommand.Parameters.AddWithValue("DateAcquired", obj.DateAcquired.Trim)
            'oCommand.Parameters.AddWithValue("RFTnum", obj.RFTnum.Trim)
            'oCommand.Parameters.AddWithValue("RFTDate", obj.RFTDate.Trim)
            oCommand.Parameters.AddWithValue("Remarks", obj.Remarks.Trim)
            oCommand.Parameters.AddWithValue("bData", obj.Data)
#End Region
            oCommand.ExecuteNonQuery()
            resultValue = True

            'send.SendAssignNotification(obj.Type.Trim, obj.Desc.Trim, obj.PPE.Trim, obj.Asset.Trim, obj.Serial.Trim, obj.DateAcquired.Trim, obj.Remarks.Trim)

        Catch ex As Exception
            System.Web.HttpContext.Current.Response.Write("<script>alert(" & ControlChars.Quote & ex.Message & " -SaveNewEquip" & ControlChars.Quote & ");</script>")
        Finally
            oConnection.Close()
        End Try

        Return resultValue
    End Function

    Public Function updateEquip(obj As Inventory_Model) As Boolean
        Dim resultValue As Boolean = False
        Dim send As New Email_Notif

        Dim oConnection = CallConnection()
        Try
            oConnection.Open()
            Dim oCommand = CallCommand()
            oCommand.Connection = oConnection
            oCommand.CommandText = "spUpdate"
            oCommand.CommandType = CommandType.StoredProcedure
#Region "Update"
            oCommand.Parameters.AddWithValue("ID", obj.ID)
            oCommand.Parameters.AddWithValue("Type", obj.Type.Trim)
            oCommand.Parameters.AddWithValue("Desc", obj.Desc.Trim)
            oCommand.Parameters.AddWithValue("PPE", obj.PPE.Trim)
            oCommand.Parameters.AddWithValue("Asset", obj.Asset.Trim)
            oCommand.Parameters.AddWithValue("Serial", obj.Serial.Trim)
            oCommand.Parameters.AddWithValue("Location", obj.Location)
            'oCommand.Parameters.AddWithValue("RFTnum", obj.RFTnum.Trim)
            'oCommand.Parameters.AddWithValue("RFTDate", obj.RFTDate.Trim)
            oCommand.Parameters.AddWithValue("Remarks", obj.Remarks.Trim)
            oCommand.Parameters.AddWithValue("AssignedTo", obj.AssignedTo.Trim)
            oCommand.Parameters.AddWithValue("Status", obj.Status.Trim)
            oCommand.Parameters.AddWithValue("bdata", obj.Data)
            oCommand.Parameters.AddWithValue("OS", obj.OS)
            oCommand.Parameters.AddWithValue("HostName", obj.HostName)
#End Region
            oCommand.ExecuteNonQuery()
            resultValue = True

            'send.SendAssignNotification(obj.Type.Trim, obj.Desc.Trim, obj.PPE.Trim, obj.Asset.Trim, obj.Serial.Trim, obj.DateAcquired.Trim, obj.Remarks.Trim)

        Catch ex As Exception
            System.Web.HttpContext.Current.Response.Write("<script>alert(" & ControlChars.Quote & ex.Message & " -UpdateEquip" & ControlChars.Quote & ");</script>")
        Finally
            oConnection.Close()
        End Try

        Return resultValue
    End Function

    Public Sub AddEquipmentHistory(historyEquip_ID As Integer, history_AssignedTo As String, history_Location As String, history_Status As String)
        Dim strQuery As New StringBuilder
        Dim nDate As DateTime = DateTime.Now
        Dim sDate As String = String.Format("{0:MM/dd/yyyy hh:mm:ss tt}", nDate)
        'Dim mes As String = param_name + " " + param_what + " " + param_id
        strQuery.Append("
                        INSERT INTO tbl_History (History_Equipment_ID, History_Assigned_To, History_Location, History_Status, History_Assigned_Date) VALUES(
                        '" & historyEquip_ID & "',
                        '" & history_AssignedTo & "',
                        '" & history_Location & "',
                        '" & history_Status & "',
                        '" & sDate & "'
                        )
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
            System.Web.HttpContext.Current.Response.Write("<script>alert(" & ControlChars.Quote & ex.Message & " Data Successfully Saved." & ControlChars.Quote & ");</script>")
        Finally
            oConnection.Close()
        End Try
    End Sub

    Public Function getID(param_ppe As String, param_asset As String, param_serial As String) As Integer
        Dim result As Integer
        Dim sQuery As New StringBuilder
        getID = Nothing

        sQuery.Append("SELECT Equipment_ID
                       FROM tbl_Equipments
                       WHERE Equipment_PPE_Number = '" & param_ppe & "'
                       AND Equipment_Serial = '" & param_serial & "'
                       AND Equipment_Asset_Number = '" & param_asset & "'
                      ")

        Try
            Dim oreader = execReader(sQuery.ToString())

            While oreader.Read()
                result = oreader("Equipment_ID")
            End While

        Catch ex As Exception
            result = Nothing
            System.Web.HttpContext.Current.Response.Write("<script>alert(" & ControlChars.Quote & ex.Message & " -geteqid" & ControlChars.Quote & ");</script>")
        Finally
            CloseNewConnection()
        End Try

        Return result
    End Function

    Public Function databaseValidation(strTable As String, strCol As String, strValue As String) As Integer
        Dim sQuery As New StringBuilder
        Dim count As Integer = 0

        If strValue <> "N/A" And strValue.ToLower.Trim <> "none" And strValue.Trim <> "" And strValue.ToLower.Trim <> "na" Then
            sQuery.Append("SELECT * FROM " & strTable & " ")
            sQuery.Append("WHERE " & strCol & " = '" & strValue & "' ")


            Try
                Dim oreader = execReader(sQuery.ToString())

                While oreader.Read()
                    count = count + 1
                End While

            Catch ex As Exception
                System.Web.HttpContext.Current.Response.Write("<script>alert(" & ControlChars.Quote & ex.Message & " -dbv" & ControlChars.Quote & ");</script>")
            Finally
                CloseNewConnection()
            End Try
        End If
        Return count
    End Function

    Public Sub saveUserHistory(param_what As String, param_name As String, param_id As String)
        Dim strQuery As New StringBuilder
        Dim nDate As DateTime = DateTime.Now
        Dim sDate As String = String.Format("{0:MM/dd/yyyy hh:mm:ss tt}", nDate)
        Dim mes As String = param_name + " " + param_what + " " + param_id
        strQuery.Append("
                        INSERT INTO tbl_User_History VALUES(
                        '" & param_name.Trim & "',
                        '" & mes.Trim & "',
                        '" & sDate & "'
                        )
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

    Public Function getEqPic(param_id As String) As Byte()
        Dim strQuery As New StringBuilder
        Dim pic As Byte() = Nothing
        Dim convert As String = ""
        strQuery.Append("SELECT Equip_Pic_Data FROM tbl_Equipments WHERE Equipment_ID = '" & param_id & "'")

        Try
            Dim oreader = execReader(strQuery.ToString())

            While oreader.Read()
                pic = DirectCast(oreader("Equip_Pic_Data"), Byte())
            End While

        Catch
            pic = Nothing
        Finally
            CloseNewConnection()
        End Try

        Return pic
    End Function
End Class
