Public Class clsMaintenance : Inherits dbhelper

    'Comment to test GitHub on VisualStudio
    Public Function getData(param_mode As Integer) As DataSet
        Dim strQuery As New StringBuilder
        Dim ds As DataSet

        If param_mode = 0 Then
            strQuery.Append("SELECT idDept, DeptName as 'Department Name' FROM tbl_Departments ORDER BY DeptName ASC ")
        ElseIf param_mode = 1 Then
            strQuery.Append("SELECT Types_ID, EQType as Type FROM tbl_Type ORDER BY EQType ASC ")
        ElseIf param_mode = 2 Then
            strQuery.Append("SELECT id , name as 'Name', domain as 'Domain', user_level as 'Access Type', active AS 'Active' FROM tbl_User_Access  ")
        End If

        ds = GetDataSet(strQuery.ToString, "")

        If Not ds Is Nothing Then
            getData = ds
        Else
            getData = Nothing
        End If
    End Function

    Public Sub saveData(param_mode1 As String, param_mode2 As String, obj As Maintenance_Model)
        Dim strQuery As New StringBuilder

        If param_mode1 = "Save" Then
            If param_mode2 = "Dept" Then
                strQuery.Append("INSERT INTO tbl_Departments VALUES ('" & obj.val1 & "') ")
            ElseIf param_mode2 = "Type" Then
                strQuery.Append("INSERT INTO tbl_Type VALUES ('" & obj.val1 & "') ")
            ElseIf param_mode2 = "UserAccess" Then
                strQuery.Append("INSERT INTO tbl_User_Access VALUES ('" & obj.val1 & "', '" & obj.val2 & "', '" & obj.val3 & "', '0') ")
            End If
        ElseIf param_mode1 = "Update" Then
            If param_mode2 = "Dept" Then
                strQuery.Append("UPDATE tbl_Departments SET DeptName = '" & obj.val1 & "' WHERE idDept = '" & obj.id & "' ")
            ElseIf param_mode2 = "Type" Then
                strQuery.Append("UPDATE tbl_Type SET EQType = '" & obj.val1 & "' WHERE Types_ID = '" & obj.id & "' ")
            ElseIf param_mode2 = "UserAccess" Then
                strQuery.Append("UPDATE tbl_User_Access SET name = '" & obj.val1 & "', domain = '" & obj.val2 & "', user_level = '" & obj.val3 & "' WHERE id = '" & obj.id & "' ")
            End If
        End If

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

    Public Sub saveUpload(param_type As String, param_desc As String, param_PPE As String,
                          param_Asset As String, param_Serial As String, param_DateAquired As String, param_Remarks As String)

        Dim strQuery As New StringBuilder
        strQuery.Append("
                          Declare @bdata varbinary(max) = null

                          INSERT INTO dbo.tbl_Equipments(
                          Equipment_Type,
                          Equipment_Description,
                          Equipment_PPE_Number,
                          Equipment_Serial,
                          Equipment_Asset_Number,
                          Equipment_Date_Acquired,
                          Equipment_Remarks,
                          Equipment_Status,
                          Equipment_Location,
                          Equip_Pic_Data,
                          timestamp,
                          ctrlIsBundled)
                          VALUES
                          ('" & param_type & "',
                          '" & param_desc & "',
                          '" & param_PPE & "',
                          '" & param_Serial & "',
                          '" & param_Asset & "',
                          '" & param_DateAquired & "',
                          '" & param_Remarks & "',
                          'On Stock',
                          '1',
                          @bdata,
                          '" & DateTime.Now.ToShortDateString() & "',
                          'No'
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


    Public Sub saveHistory(param_type As String, param_desc As String, param_PPE As String,
                          param_Asset As String, param_Serial As String, param_history As String)
        Dim strQuery As New StringBuilder
        strQuery.Append("
                          DECLARE @ID int = null

                          SELECT @ID = Equipment_ID
                          FROM tbl_Equipments
                          WHERE Equipment_Type = '" & param_type & "'
                          AND Equipment_Description = '" & param_desc & "'
                          AND Equipment_PPE_Number = '" & param_PPE & "'
                          AND Equipment_Serial = '" & param_Serial & "'
                          AND Equipment_Asset_Number = '" & param_Asset & "'

                          INSERT INTO dbo.tbl_History
                          (History_Equipment_ID,
                          History_Assigned_To,
                          History_Assigned_Date)
                          VALUES
                          (@ID,
                          '" & param_history & "',
                          '" & DateTime.Now.ToShortDateString() & "')
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
