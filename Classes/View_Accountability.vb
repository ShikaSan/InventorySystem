Public Class View_Accountability : Inherits dbhelper

    Public Function getGVview(param_id As String)
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
                         WHERE d.id = '" & param_id & "' AND Equipment_Status NOT IN ('Defective', 'For Repair')
                         ")

        ds = GetDataSet(strQuery.ToString, "")

        If Not ds Is Nothing Then
            getGVview = ds
        Else
            getGVview = Nothing
        End If
    End Function

    Public Function getAllItems(param_id As String, param_type As String) As DataSet
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
						 INNER JOIN tbl_Host_Names as c ON b.PC_num = c.PC_num
                         INNER JOIN tbl_Assigned_To as d on c.PC_num = d.PC_num
                         WHERE d.id = '" & param_id & "'
                         ")
        If param_type <> "" Then
            strQuery.Append("
                         And c.PC_Type = '" & param_type.Trim & "' 
                         ")
        End If
        ds = GetDataSet(strQuery.ToString, "")
        If Not ds Is Nothing Then
            getAllItems = ds
        Else
            getAllItems = Nothing
        End If
    End Function

    Public Function getAllItems_Add(param_id As String, param_type As String) As DataSet
        Dim strQuery As New StringBuilder
        Dim ds As DataSet

        strQuery.Append("
                         SELECT Equipment_ID, 
                         Equipment_Type As Type, 
                         Equipment_Description as Description, 
                         Equipment_PPE_Number as 'PPE #', 
                         Equipment_Asset_Number As 'Asset #', 
                         Equipment_Serial AS 'Serial #'
                         FROM tbl_Equipments WHERE Equipment_Status = 'On Stock'
                         ")
        'If param_type <> "" Then
        'strQuery.Append("
        'And c.PC_Type = '" & param_type.Trim & "' 
        '")
        'End If
        ds = GetDataSet(strQuery.ToString, "")
        If Not ds Is Nothing Then
            getAllItems_Add = ds
        Else
            getAllItems_Add = Nothing
        End If
    End Function

    Public Function printView(param_search As String, param_type As String) As DataSet
        Dim strQuery As New StringBuilder
        Dim ds As DataSet

        strQuery.Append("
                         SELECT b.PC_Num as 'PC #', 
                         a.Equipment_Type As Type, 
                         a.Equipment_Description as Description, 
                         a.Equipment_PPE_Number as 'PPE #', 
                         a.Equipment_Asset_Number As 'Asset #', 
                         a.Equipment_Serial AS 'Serial #',
                         a.Equipment_Remarks as Remarks,
                         c.Host_Name as Host_Name
                         FROM tbl_Equipments as a INNER JOIN 
                         tbl_Accountability as b ON a.Equipment_ID = b.Equipment_ID
						 INNER JOIN tbl_Host_Names as c ON b.PC_num = c.PC_num
                         INNER JOIN tbl_Assigned_To as d on c.PC_num = d.PC_num
                         WHERE d.Assigned_To = '" & param_search.Trim & "'
                         ")
        If param_type <> "" Then
            strQuery.Append("
                         And c.PC_Type = '" & param_type.Trim & "' 
                         ")
        End If
        ds = GetDataSet(strQuery.ToString, "")
        If Not ds Is Nothing Then
            printView = ds
        Else
            printView = Nothing
        End If
    End Function

    Public Function getLoc(param_id As String) As String
        Dim strQuery As New StringBuilder
        Dim loc As String = ""
        strQuery.Append("SELECT DISTINCT a.Equipment_Location 
                        FROM tbl_Equipments as a 
                        INNER JOIN tbl_Accountability as b 
						ON a.Equipment_ID = b.Equipment_ID 
                        INNER JOIN tbl_Assigned_To as c 
                        ON b.PC_num = c.PC_num
                        WHERE c.id = '" & param_id.Trim & "'
                        ")
        Try
            Dim oreader = execReader(strQuery.ToString())

            While oreader.Read()
                loc = oreader("Equipment_Location").ToString()
            End While

        Catch ex As Exception
            System.Web.HttpContext.Current.Response.Write("<script>alert(" & ControlChars.Quote & ex.Message & " -getloc" & ControlChars.Quote & ");</script>")
        Finally
            CloseNewConnection()
        End Try

        Return loc
    End Function

    Public Function getACode(param_id As String) As String
        Dim strQuery As New StringBuilder
        Dim acode As String = ""
        strQuery.Append("SELECT aCode FROM tbl_Assigned_To
                        WHERE id = '" & param_id.Trim & "'
                        ")
        Try
            Dim oreader = execReader(strQuery.ToString())

            While oreader.Read()
                acode = oreader("aCode").ToString()
            End While

        Catch ex As Exception
            System.Web.HttpContext.Current.Response.Write("<script>alert(" & ControlChars.Quote & ex.Message & " -getloc" & ControlChars.Quote & ");</script>")
        Finally
            CloseNewConnection()
        End Try

        Return acode
    End Function

    Public Function getRemarks(param_id As String) As String
        Dim strQuery As New StringBuilder
        Dim Remarks As String = ""
        strQuery.Append("SELECT Remarks
                        FROM tbl_Assigned_To						
                        WHERE id = '" & param_id.Trim & "'
                        ")
        Try
            Dim oreader = execReader(strQuery.ToString())

            While oreader.Read()
                Remarks = oreader("Remarks").ToString()
            End While

        Catch ex As Exception
            System.Web.HttpContext.Current.Response.Write("<script>alert(" & ControlChars.Quote & ex.Message & " -getloc" & ControlChars.Quote & ");</script>")
        Finally
            CloseNewConnection()
        End Try

        Return Remarks
    End Function

    Public Function getTypes(param_id As String) As String
        Dim strQuery As New StringBuilder
        Dim pcType As String = ""
        strQuery.Append("SELECT a.PC_Type 
                        FROM tbl_Host_Names as a 
                        INNER JOIN tbl_Assigned_To as b 
                        ON a.PC_num = b.PC_num 
                        WHERE b.id = '" & param_id.Trim & "'
                        ")
        Try
            Dim oreader = execReader(strQuery.ToString())

            While oreader.Read()
                pcType = oreader("PC_Type").ToString()
            End While

        Catch ex As Exception
            System.Web.HttpContext.Current.Response.Write("<script>alert(" & ControlChars.Quote & ex.Message & " -getloc" & ControlChars.Quote & ");</script>")
        Finally
            CloseNewConnection()
        End Try

        Return pcType
    End Function

    Public Function getDescription(param_type As String) As DataSet
        Dim strQuery As New StringBuilder
        Dim ds As DataSet

        strQuery.Append("SELECT DISTINCT Equipment_Description FROM tbl_Equipments ")
        strQuery.Append("WHERE Equipment_Type = '" & param_type & "' ")
        strQuery.Append("ORDER BY Equipment_Description ASC")

        ds = GetDataSet(strQuery.ToString, "")

        If Not ds Is Nothing Then
            getDescription = ds
        Else
            getDescription = Nothing
        End If
    End Function

    Public Function getPPEList(param_type As String, param_desc As String) As DataSet
        Dim strQuery As New StringBuilder
        Dim ds As DataSet

        strQuery.Append("SELECT DISTINCT Equipment_PPE_Number FROM tbl_Equipments ")
        strQuery.Append(" WHERE Equipment_Type = '" & param_type.Trim & "' AND Equipment_Description = '" & param_desc.Trim & "'")
        strQuery.Append(" ORDER BY Equipment_PPE_Number ASC")

        ds = GetDataSet(strQuery.ToString, "")

        If Not ds Is Nothing Then
            getPPEList = ds
        Else
            getPPEList = Nothing
        End If
    End Function

    Public Function getAssetList(param_type As String, param_desc As String) As DataSet
        Dim strQuery As New StringBuilder
        Dim ds As DataSet

        strQuery.Append("SELECT DISTINCT Equipment_Asset_Number FROM tbl_Equipments ")
        strQuery.Append("WHERE Equipment_Type = '" & param_type.Trim & "' AND Equipment_Description = '" & param_desc.Trim & "' ")
        strQuery.Append("ORDER BY Equipment_Asset_Number ASC")

        ds = GetDataSet(strQuery.ToString, "")

        If Not ds Is Nothing Then
            getAssetList = ds
        Else
            getAssetList = Nothing
        End If
    End Function

    Public Function getSerialList(param_type As String, param_desc As String) As DataSet
        Dim strQuery As New StringBuilder
        Dim ds As DataSet

        strQuery.Append("SELECT DISTINCT Equipment_Serial FROM tbl_Equipments ")
        strQuery.Append("WHERE Equipment_Type = '" & param_type.Trim & "' AND Equipment_Description = '" & param_desc.Trim & "' ")
        strQuery.Append("ORDER BY Equipment_Serial ASC")

        ds = GetDataSet(strQuery.ToString, "")

        If Not ds Is Nothing Then
            getSerialList = ds
        Else
            getSerialList = Nothing
        End If
    End Function
End Class
