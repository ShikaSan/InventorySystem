Imports System.Data.SqlClient

Public Class Assign_Equip : Inherits dbhelper

    Public Function getDescription(param_type As String, param_ctrl As String) As DataSet
        Dim strQuery As New StringBuilder
        Dim ds As DataSet

        strQuery.Append("SELECT DISTINCT Equipment_Description FROM tbl_Equipments WHERE Equipment_Type = '" & param_type & "' ")
        If param_ctrl = 0 Then
            strQuery.Append("AND Equipment_Status = 'On Stock'")
        End If
        strQuery.Append("ORDER BY Equipment_Description ASC")

        ds = GetDataSet(strQuery.ToString, "")

        If Not ds Is Nothing Then
            getDescription = ds
        Else
            getDescription = Nothing
        End If
    End Function

    Public Function getDesc_OnAdd(param_type As String, param_ctrl As String) As DataSet
        Dim strQuery As New StringBuilder
        Dim ds As DataSet

        strQuery.Append("SELECT DISTINCT Equipment_Description FROM tbl_Equipments WHERE Equipment_Type = '" & param_type & "' ")
        ' If param_ctrl = 0 Then
        strQuery.Append("AND Equipment_Status != 'Assigned'")
        'End If
        strQuery.Append("ORDER BY Equipment_Description ASC")

        ds = GetDataSet(strQuery.ToString, "")

        If Not ds Is Nothing Then
            getDesc_OnAdd = ds
        Else
            getDesc_OnAdd = Nothing
        End If
    End Function

    Public Function getPPEList(param_type As String, param_desc As String, param_ctrl As String) As DataSet
        Dim strQuery As New StringBuilder
        Dim ds As DataSet

        strQuery.Append("SELECT DISTINCT Equipment_PPE_Number 
                        FROM tbl_Equipments 
                        WHERE Equipment_Type = '" & param_type.Trim & "' 
                        AND Equipment_Description = '" & param_desc.Trim & "' 
                        ")

        If param_ctrl = 0 Then
            strQuery.Append("AND Equipment_Status = 'On Stock'")
            'strQuery.Append("AND Equipment_Status = 'On Stock' AND ctrlIsBundled = 'Yes' OR ctrlIsBundled = 'No' ")
            'Else
            '    strQuery.Append("AND ctrlIsBundled = 1")
        End If

        strQuery.Append("ORDER BY Equipment_PPE_Number ASC")

        ds = GetDataSet(strQuery.ToString, "")

        If Not ds Is Nothing Then
            getPPEList = ds
        Else
            getPPEList = Nothing
        End If
    End Function

    Public Function getAssetList(param_type As String, param_desc As String, param_ctrl As String) As DataSet
        Dim strQuery As New StringBuilder
        Dim ds As DataSet

        strQuery.Append("SELECT DISTINCT Equipment_Asset_Number 
                        FROM tbl_Equipments 
                        WHERE Equipment_Type = '" & param_type.Trim & "' 
                        AND Equipment_Description = '" & param_desc.Trim & "' 
                        ")

        If param_ctrl = 0 Then
            strQuery.Append("AND Equipment_Status = 'On Stock' AND ctrlIsBundled = 'No' ")
            'Else
            '    strQuery.Append("AND ctrlIsBundled = 1")
        End If

        strQuery.Append("ORDER BY Equipment_Asset_Number ASC")

        ds = GetDataSet(strQuery.ToString, "")

        If Not ds Is Nothing Then
            getAssetList = ds
        Else
            getAssetList = Nothing
        End If
    End Function

    Public Function getSerialList(param_type As String, param_desc As String, param_ctrl As String) As DataSet
        Dim strQuery As New StringBuilder
        Dim ds As DataSet

        strQuery.Append("SELECT DISTINCT Equipment_Serial 
                        FROM tbl_Equipments 
                        WHERE Equipment_Type = '" & param_type.Trim & "' 
                        AND Equipment_Description = '" & param_desc.Trim & "' 
                        ")

        If param_ctrl = 0 Then
            strQuery.Append("AND Equipment_Status = 'On Stock' AND ctrlIsBundled = 'No' ")
            'Else
            '    strQuery.Append("AND ctrlIsBundled = 1")
        End If

        strQuery.Append("ORDER BY Equipment_Serial ASC")

        ds = GetDataSet(strQuery.ToString, "")

        If Not ds Is Nothing Then
            getSerialList = ds
        Else
            getSerialList = Nothing
        End If
    End Function

    Public Function getOtherDetails(param_type As String, param_1 As String, param_2 As String, param_mode As String) As DataSet
        Dim strQuery As New StringBuilder
        Dim ds As DataSet

        If param_type = "PPE" Then
            strQuery.Append("SELECT Equipment_Serial, Equipment_Asset_Number FROM tbl_Equipments
                            WHERE Equipment_PPE_Number = '" & param_1.Trim & "' AND Equipment_Description = '" & param_2.Trim & "' ")
        ElseIf param_type = "Asset" Then
            strQuery.Append("SELECT Equipment_Serial, Equipment_PPE_Number FROM tbl_Equipments
                            WHERE Equipment_Asset_Number = '" & param_1.Trim & "' AND Equipment_Description = '" & param_2.Trim & "' ")
        ElseIf param_type = "Serial" Then
            strQuery.Append("SELECT Equipment_PPE_Number, Equipment_Asset_Number FROM tbl_Equipments 
                            WHERE Equipment_Serial = '" & param_1.Trim & "' AND Equipment_Description = '" & param_2.Trim & "' ")
        End If
        If param_mode = "1" Then
            strQuery.Append(" AND Equipment_Status = 'On Stock' ")
        End If
        ds = GetDataSet(strQuery.ToString, "")

        If Not ds Is Nothing Then
            getOtherDetails = ds
        Else
            getOtherDetails = Nothing
        End If

    End Function

    Public Function getPCnumber() As Integer
        Dim sQuery As New StringBuilder
        Dim count As Integer = 0
        sQuery.Append("SELECT COUNT(DISTINCT PC_num) as num ")
        sQuery.Append("FROM tbl_Accountability ")

        Try
            Dim oreader = execReader(sQuery.ToString())

            While oreader.Read()
                count = oreader("num")
            End While

        Catch ex As Exception
            System.Web.HttpContext.Current.Response.Write("<script>alert(" & ControlChars.Quote & ex.Message & " -getPCnum" & ControlChars.Quote & ");</script>")
        Finally
            CloseNewConnection()
        End Try

        Return count
    End Function

    Public Function validatePCnumber(param_host As String, param_Type As String) As Integer
        Dim sQuery As New StringBuilder
        Dim count As Integer = 0
        sQuery.Append("                       
                       SELECT PC_num 
                       FROM tbl_Host_Names 
                       WHERE Host_Name = '" & param_host.Trim & "'
                       AND PC_Type = '" & param_Type.Trim & "'
                     ")

        Try
            Dim oreader = execReader(sQuery.ToString())

            While oreader.Read()
                count = oreader("PC_num")
            End While

        Catch ex As Exception
            System.Web.HttpContext.Current.Response.Write("<script>alert(" & ControlChars.Quote & ex.Message & " -ValidatePcNum" & ControlChars.Quote & ");</script>")
        Finally
            CloseNewConnection()
        End Try

        Return count
    End Function

    Public Function getID(param_host As String) As String
        Dim sQuery As New StringBuilder
        Dim id As String = ""
        sQuery.Append("
                       SELECT a.id as ID 
                       FROM tbl_Assigned_To as a 
                       INNER JOIN tbl_Host_Names as b 
                       ON a.PC_num = b.PC_num 
                       WHERE b.Host_Name = '" & param_host & "'
                       ")

        Try
            Dim oreader = execReader(sQuery.ToString())

            While oreader.Read()
                id = oreader("id")
            End While

        Catch ex As Exception
            System.Web.HttpContext.Current.Response.Write("<script>alert(" & ControlChars.Quote & ex.Message & " -getName" & ControlChars.Quote & ");</script>")
        Finally
            CloseNewConnection()
        End Try

        Return id
    End Function

    Public Function saveAssign(param_type As String, param_ppe As String, param_asset As String,
                          param_serial As String, param_assignedTo As String, param_assignedDate As String, param_Type2 As String,
                          param_location As String, param_HostName As String, param_pcnum As String, param_rftnum As String, param_rftdate As String) As Boolean

        Dim result As Boolean = False
        Dim strQuery As New StringBuilder

        strQuery.Append("DECLARE @id int = 0
                        
                        SELECT @id = Equipment_ID 
                        FROM tbl_Equipments 
                        WHERE Equipment_Type =  @param_type
                        AND Equipment_PPE_Number =  @param_ppe
                        AND Equipment_Serial =  @param_serial
                        AND Equipment_Asset_Number =  @param_asset
                        ")

        If param_pcnum <> 0 Then
            strQuery.Append("INSERT INTO tbl_Accountability VALUES
                            (@param_pcnum ,@id)
                            ")
        End If
        Dim pDate As String = param_assignedDate.Trim
        strQuery.Append("UPDATE tbl_Equipments 
                        SET Equipment_Status = 'Assigned', 
                        Equipment_Location = @param_location, 
                        Equipment_RFT_num = @param_rftnum, 
                        Equipment_RFT_Date = @param_rftdate ,
                        ctrlIsBundled = 'Yes'
                        WHERE Equipment_ID = @id

                        INSERT INTO tbl_History(History_Equipment_ID, History_Assigned_To, History_Assigned_Date) VALUES(
                         @id,
                        @param_assignedTo,
                        '" & String.Format("{0:MM/dd/yyyy}", pDate) & "'
                        ) ")

        '        Declare @host int = null

        'SELECT @host = COUNT([Host_Name]) FROM [tbl_Accountability]

        'IF @host = 0
        '    BEGIN
        '        INSERT INTO tbl_Accountability VALUES(
        '        '" & param_HostName.Trim & "',
        '        @id
        '        )
        '    END

        Dim oConnection = CallConnection()
        Try
            oConnection.Open()

            Using cmd As SqlCommand = New SqlCommand()
                cmd.Connection = oConnection
                cmd.CommandText = strQuery.ToString()
                cmd.CommandType = CommandType.Text

                Dim parm2 As SqlParameter = New SqlParameter With {
                    .ParameterName = "@param_type",
                    .SqlDbType = SqlDbType.NVarChar,
                    .Value = param_type
                }
                cmd.Parameters.Add(parm2)

                'Dim parm3 As SqlParameter = New SqlParameter With {
                '    .ParameterName = "@param_desc",
                '    .SqlDbType = SqlDbType.NVarChar,
                '    .Value = param_desc
                '}
                'cmd.Parameters.Add(parm3)

                Dim p4 As SqlParameter = New SqlParameter With {
                    .ParameterName = "@param_ppe",
                    .SqlDbType = SqlDbType.NVarChar,
                    .Value = param_ppe
                }
                cmd.Parameters.Add(p4)

                Dim p5 As SqlParameter = New SqlParameter With {
                    .ParameterName = "@param_asset",
                    .SqlDbType = SqlDbType.NVarChar,
                    .Value = param_asset
                }
                cmd.Parameters.Add(p5)

                Dim p6 As SqlParameter = New SqlParameter With {
                    .ParameterName = "@param_serial",
                    .SqlDbType = SqlDbType.NVarChar,
                    .Value = param_serial
                }
                cmd.Parameters.Add(p6)

                Dim p7 As SqlParameter = New SqlParameter With {
                    .ParameterName = "@param_assignedTo",
                    .SqlDbType = SqlDbType.NVarChar,
                    .Value = param_assignedTo
                }
                cmd.Parameters.Add(p7)

                Dim p8 As SqlParameter = New SqlParameter With {
                    .ParameterName = "@param_assignedDate",
                    .SqlDbType = SqlDbType.NVarChar,
                    .Value = param_assignedDate
                }
                cmd.Parameters.Add(p8)

                Dim p9 As SqlParameter = New SqlParameter With {
                    .ParameterName = "@param_Type2",
                    .SqlDbType = SqlDbType.NVarChar,
                    .Value = param_Type2
                }
                cmd.Parameters.Add(p9)

                Dim p10 As SqlParameter = New SqlParameter With {
                    .ParameterName = "@param_location",
                    .SqlDbType = SqlDbType.NVarChar,
                    .Value = param_location
                }
                cmd.Parameters.Add(p10)

                Dim p11 As SqlParameter = New SqlParameter With {
                    .ParameterName = "@param_HostName",
                    .SqlDbType = SqlDbType.NVarChar,
                    .Value = param_HostName
                }
                cmd.Parameters.Add(p11)

                Dim p12 As SqlParameter = New SqlParameter With {
                    .ParameterName = "@param_pcnum",
                    .SqlDbType = SqlDbType.Int,
                    .Value = Convert.ToInt32(param_pcnum)
                }
                cmd.Parameters.Add(p12)

                Dim p13 As SqlParameter = New SqlParameter With {
                    .ParameterName = "@param_rftnum",
                    .SqlDbType = SqlDbType.NVarChar,
                    .Value = param_rftnum
                }
                cmd.Parameters.Add(p13)

                Dim p14 As SqlParameter = New SqlParameter With {
                    .ParameterName = "@param_rftdate",
                    .SqlDbType = SqlDbType.NVarChar,
                    .Value = param_rftdate
                }
                cmd.Parameters.Add(p14)

                If cmd.ExecuteNonQuery() >= 1 Then
                    result = True
                End If
                cmd.Dispose()
                cmd.Parameters.Clear()
            End Using

        Catch ex As Exception
            System.Web.HttpContext.Current.Response.Write("<script>alert(" & ControlChars.Quote & ex.Message & " -saveAssign" & ControlChars.Quote & ");</script>")
        Finally
            oConnection.Close()
        End Try

        Return result
    End Function

    Public Sub saveInfo(param_host As String, param_assignedTo As String, param_assignedDate As String, param_department As String,
                        param_OS As String, param_position As String, param_type2 As String, param_type As String, param_desc As String,
                        param_ppe As String, param_asset As String, param_serial As String, param_rftnum As String, param_rftdate As String, param_mode As String, param_acode As String)
        Dim strQuery As New StringBuilder

        strQuery.Append("DECLARE @id int

                        SELECT @id = a.PC_num 
                        FROM [tbl_Accountability] a
                        INNER JOIN tbl_Equipments as b
                        ON a.[Equipment_ID] = b.[Equipment_ID]
                        WHERE b.Equipment_Type = '" & param_type.Trim & "'
                        AND b.Equipment_Description = '" & param_desc.Trim & "'
                        AND b.Equipment_PPE_Number = '" & param_ppe.Trim & "'
                        AND b.Equipment_Serial = '" & param_serial.Trim & "' 
                        AND b.Equipment_Asset_Number = '" & param_asset.Trim & "'                        
                         ")
        If param_mode = "Save" Then
            strQuery.Append("
                        INSERT INTO tbl_Assigned_To VALUES(
                        '" & param_assignedTo.Trim & "',
                        '" & param_assignedDate.Trim & "',
                        '" & param_department.Trim & "',
                        '" & param_OS.Trim & "',
                        '" & param_position.Trim & "',
                        @id,
                        '',
                        '',
                        '" & param_acode & "', 'Yes')

                       INSERT INTO tbl_Host_Names VALUES(
                       @id,
                       '" & param_host.Trim & "',
                       '" & param_type2.Trim & "',
                       '" & param_rftnum & "',
                       '" & param_rftdate & "', 
                       'Yes')                         
                        ")
        ElseIf param_mode = "Update" Then
            strQuery.Append("
                        UPDATE tbl_Assigned_To SET 
                        Assigned_To = '" & param_assignedTo.Trim & "',
                        Assigned_Date = '" & param_assignedDate.Trim & "',
                        Department = '" & param_department.Trim & "',
                        OS = '" & param_OS.Trim & "',
                        Position = '" & param_position.Trim & "',
                        active = 'Yes'
                        WHERE PC_num = @id

                        UPDATE tbl_Host_Names 
                        SET RFTnum = '" & param_rftnum & "',
                        RFTdate = '" & param_rftdate & "' ,
                        active = 'Yes'
                        WHERE PC_num = @id                       
                        ")
        End If

        'INSERT INTO tbl_Assigned_To VALUES(
        '                '" & param_host.Trim & "',
        '                '" & param_assignedTo.Trim & "',
        '                '" & param_assignedDate.Trim & "',
        '                '" & param_department.Trim & "',
        '                '" & param_OS.Trim & "',
        '                '" & param_position.Trim & "',
        '                @id,
        '                '" & param_type2.Trim & "')

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

    Public Sub deleteExistingAssign(param_id As String, param_reclaim As Integer)
        Dim strQuery As New StringBuilder
        Dim ds As New DataSet

        strQuery.Append("SELECT a.Equipment_ID 
                        FROM tbl_Equipments as a 
                        INNER JOIN tbl_Accountability as b 
                        ON a.Equipment_ID = b.Equipment_ID 
                        INNER JOIN tbl_Assigned_To as c 
                        ON b.PC_num = c.PC_num
                        ")
        strQuery.Append("WHERE c.id = '" & param_id & "'")

        ds = GetDataSet(strQuery.ToString, "")

        For x = 0 To ds.Tables(0).Rows.Count - 1
            strQuery.Clear()
            strQuery.Append("UPDATE tbl_Equipments SET Equipment_Status = 'On Stock', Equipment_Location = '1' 
                        WHERE Equipment_ID = '" & ds.Tables(0).Rows(x)(0).ToString() & "'")
            Dim oCon = CallConnection()
            Try
                oCon.Open()
                Dim oCommand = CallCommand()
                oCommand.Connection = oCon
                oCommand.CommandText = strQuery.ToString()
                oCommand.CommandType = CommandType.Text
                oCommand.ExecuteNonQuery()
            Catch ex As Exception
                System.Web.HttpContext.Current.Response.Write("<script>alert(" & ControlChars.Quote & ex.Message & " -UpdateEqp" & ControlChars.Quote & ");</script>")
            Finally
                oCon.Close()
            End Try
        Next
        strQuery.Clear()
        strQuery.Append("
                         DECLARE @pcnum int
                         SELECT @pcnum = PC_num FROM tbl_Assigned_To WHERE id = '" & param_id & "'
                         ")

        If param_reclaim = 0 Then
            strQuery.Append("                             
                            DELETE FROM tbl_Accountability WHERE PC_num = @pcnum                            
                            ")
        End If

        strQuery.Append("
                         UPDATE tbl_Host_Names SET active = 'No' WHERE PC_num = @pcnum
                         UPDATE tbl_Assigned_To SET active = 'No' WHERE id = '" & param_id & "'
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
            System.Web.HttpContext.Current.Response.Write("<script>alert(" & ControlChars.Quote & ex.Message & " -DeleteAcc" & ControlChars.Quote & ");</script>")
        Finally
            oConnection.Close()
        End Try
    End Sub

    Public Function validateHostName(strValue As String) As Integer
        Dim sQuery As New StringBuilder
        Dim count As Integer = 0

        If strValue <> "N/A" And strValue <> "None" Then
            sQuery.Append("SELECT * FROM tbl_Host_Names ")
            sQuery.Append("WHERE Host_Name = '" & strValue & "' AND active = 'Yes' ")
        End If

        Try
            Dim oreader = execReader(sQuery.ToString())

            While oreader.Read()
                count = count + 1
            End While

        Catch ex As Exception
            System.Web.HttpContext.Current.Response.Write("<script>alert(" & ControlChars.Quote & ex.Message & " -validateHostName" & ControlChars.Quote & ");</script>")
        Finally
            CloseNewConnection()
        End Try

        Return count
    End Function
End Class
