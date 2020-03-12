Imports System.Data
Imports System.Data.Sql
Imports System.Data.SqlClient
Imports System.Configuration
Imports System.Web.Services
Imports System.Web.Script.Services
Imports System.Web.Script.Serialization

Public Class AssignAdditional_Equip
    Inherits System.Web.UI.Page
    Private Shared prevName As String = String.Empty
    Private Shared strUserName As String = Nothing
    Private Shared strFullName As String = Nothing
    Private Shared ctrlData As Integer = 0
    'Private Shared mode As String = "Update"
    Private Shared userid As Integer
    Private Shared configConnString = ConfigurationManager.ConnectionStrings("ITSGinventory").ConnectionString

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        HttpContext.Current.Response.Cache.SetCacheability(HttpCacheability.NoCache)
        HttpContext.Current.Response.Cache.SetNoServerCaching()
        HttpContext.Current.Response.Cache.SetNoStore()

        If Not IsPostBack Then
            Try
                Dim i As Integer = 0
                While i < Session.Contents.Count
                    If Session.Keys(i).ToString() = "Username" Then
                        strUserName = Session("Username").ToString()
                    ElseIf Session.Keys(i).ToString() = "FullName" Then
                        strFullName = Session("FullName").ToString()
                    End If
                    i += 1
                End While

                If Session("FullName").ToString() <> String.Empty Then
                    TextBox1.Text = Session("employeeName").ToString()
                    hiddenUserId.Value = Integer.Parse(Session("AssignedID"))
                    Me.Master.getUserName(strFullName, strUserName)
                    Session.Remove("employeeName")
                    Session.Remove("AssignedID")
                    'Session.Remove("Username")
                    'Session.Remove("FullName")
                    'Dim cls As New User_Validation
                    'If cls.userLevel(strUserName) = "RO" Then
                    '    btnAddAcc.Visible = False
                    'End If
                Else
                    Session.RemoveAll()
                    Response.Redirect("~/Forms/Login.aspx")
                End If
            Catch ex As Exception
                Session.RemoveAll()
                Response.Redirect("~/Forms/Login.aspx")
            End Try
        End If
    End Sub

    Protected Sub updateBtn_Click(sender As Object, e As EventArgs)
        Dim getEquipTypeValues() As String = Request.Form.GetValues("select_type")
        Dim getEquipDescValues() As String = Request.Form.GetValues("select_desc")
        Dim getEquipPPEValues() As String = Request.Form.GetValues("select_ppe")
        Dim getEquipAssetValues() As String = Request.Form.GetValues("select_asset")
        Dim getEquipSerialValues() As String = Request.Form.GetValues("select_serial")
        Dim counterVar As Integer = hiddenCounterField.Value

        Dim spName As String = "dbo.[UpdateAccountability]"

        Try
            Using SqlConn As SqlConnection = New SqlConnection(configConnString)

                Dim cmd As SqlCommand = New SqlCommand(spName, SqlConn)

                cmd.CommandType = CommandType.StoredProcedure

                SqlConn.Open()

                For i As Integer = 0 To counterVar - 1
                    cmd.Parameters.Clear()

                    cmd.Parameters.AddWithValue("@UserId", hiddenUserId.Value)
                    cmd.Parameters.AddWithValue("@AssignedTo", TextBox1.Text)
                    cmd.Parameters.AddWithValue("@User_Location", LocationDropDownList.Text)
                    cmd.Parameters.AddWithValue("@EquipType", getEquipTypeValues(i))
                    cmd.Parameters.AddWithValue("@EquipDesc", getEquipDescValues(i))
                    cmd.Parameters.AddWithValue("@EquipPPE", getEquipPPEValues(i))
                    cmd.Parameters.AddWithValue("@EquipAsset", getEquipAssetValues(i))
                    cmd.Parameters.AddWithValue("@EquipSerial", getEquipSerialValues(i))

                    cmd.ExecuteNonQuery()
                Next

                SqlConn.Close()
            End Using
        Catch er As SqlException
            Response.Write("<script>alert('" + er.ToString() + "');</script>")
        Finally
            Response.Write("<script>alert('Equipment Accountability Successfully Added');</script>")
        End Try
    End Sub

    'This is a WebMethod that gets called by the functions on the client-side code (JQuery AJAX)
    <WebMethod>
    Public Shared Function GetDropDownValues(DropDown_Param As String, Value_Param As String, EquipName_Param As String, EquipPPE_Param As String)
        Dim DropDown_Options As New List(Of String)
        Dim serializer As New JavaScriptSerializer

        Using sqlConn As SqlConnection = New SqlConnection

            sqlConn.ConnectionString = configConnString

            sqlConn.Open()

            Dim SQL_commandString As String = ""

            If DropDown_Param = "Equipment_Description" Then
                SQL_commandString = "SELECT DISTINCT(" + DropDown_Param + ") FROM tbl_Equipments WHERE Equipment_Type = '" + Value_Param + "' ORDER BY " + DropDown_Param + " ASC"
            ElseIf DropDown_Param = "Equipment_PPE_Number" Then
                SQL_commandString = "SELECT DISTINCT(" + DropDown_Param + ") FROM tbl_Equipments WHERE Equipment_Description = '" + Value_Param + "' ORDER BY " + DropDown_Param + " ASC"
            ElseIf DropDown_Param = "Equipment_Asset_Number" Then
                SQL_commandString = "SELECT " + DropDown_Param + " FROM tbl_Equipments WHERE Equipment_PPE_Number = '" + Value_Param + "' AND Equipment_Description='" + EquipName_Param + "' AND Equipment_Status = 'On Stock' ORDER BY " + DropDown_Param + " ASC"
            Else
                SQL_commandString = "SELECT " + DropDown_Param + " FROM tbl_Equipments WHERE Equipment_Description = '" + EquipName_Param + "' AND Equipment_PPE_Number= '" + EquipPPE_Param + "' AND  Equipment_Asset_Number = '" + Value_Param + "' ORDER BY Equipment_Serial"
            End If

            Dim sqlCmd As SqlCommand = New SqlCommand(SQL_commandString, sqlConn)

            Using reader As SqlDataReader = sqlCmd.ExecuteReader
                While reader.Read()
                    DropDown_Options.Add(reader(DropDown_Param))
                End While
            End Using

            sqlConn.Close()
            sqlConn.Dispose()

        End Using

        Return serializer.Serialize(DropDown_Options)
    End Function

    <WebMethod>
    Public Shared Function getEquipListing()
        Dim equipType_Options As New List(Of String)
        Dim serializer As New JavaScriptSerializer
        Dim sqlConn As New SqlConnection
        Dim reader As SqlDataReader

        sqlConn.ConnectionString = ConfigurationManager.ConnectionStrings("ITSGinventory").ConnectionString

        Dim sqlCmd As New SqlCommand("SELECT DISTINCT(Equipment_Type) FROM tbl_Equipments ORDER BY Equipment_Type ASC", sqlConn)

        sqlConn.Open()

        reader = sqlCmd.ExecuteReader

        While reader.Read()
            equipType_Options.Add(reader("Equipment_Type"))
        End While

        sqlConn.Close()
        sqlConn.Dispose()

        Return serializer.Serialize(equipType_Options)
    End Function

    Protected Sub cancelBtn_Click(sender As Object, e As EventArgs)
        Response.Redirect("~/Forms/UserAccountability.aspx")
    End Sub

End Class