Imports System.Data
Imports System.Data.Sql
Imports System.Data.SqlClient
Imports System.Configuration
Imports System.Web.Services
Imports System.Web.Script.Services
Imports System.Web.Script.Serialization

Public Class AssignAdditional_Equip
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Protected Sub updateBtn_Click(sender As Object, e As EventArgs)
        Dim getEquipTypeValues() As String = Request.Form.GetValues("select_type")
        Dim getEquipDescValues() As String = Request.Form.GetValues("select_desc")
        Dim getEquipPPEValues() As String = Request.Form.GetValues("select_desc")
        Dim getEquipAssetValues() As String = Request.Form.GetValues("select_desc")
        Dim getEquipSerialValues() As String = Request.Form.GetValues("select_desc")
        Dim counterVar As Integer = hiddenCounterField.Value

        For i As Integer = 0 To counterVar - 1
            Response.Write("<script>alert('" + getEquipTypeValues(i) + "')</script>")
        Next

    End Sub

    'This is a WebMethod that gets called by the functions on the client-side code (JQuery AJAX)
    <WebMethod>
    Public Shared Function GetDropDownValues(DropDown_Param As String, EquipType_Param As String, EquipName_Param As String, EquipPPE_Param As String)
        Dim DropDown_Options As New List(Of String)
        Dim serializer As New JavaScriptSerializer
        Dim sqlConn As New SqlConnection
        Dim reader As SqlDataReader

        sqlConn.ConnectionString = ConfigurationManager.ConnectionStrings("ITSGinventory").ConnectionString
        Dim SQL_commandString As String = ""

        If DropDown_Param = "Equipment_Description" Then
            SQL_commandString = "SELECT DISTINCT(" + DropDown_Param + ") FROM tbl_Equipments WHERE Equipment_Type = '" + EquipType_Param + "' ORDER BY " + DropDown_Param + " ASC"
        ElseIf DropDown_Param = "Equipment_PPE_Number" Then
            SQL_commandString = "SELECT DISTINCT(" + DropDown_Param + ") FROM tbl_Equipments WHERE Equipment_Description = '" + EquipType_Param + "' ORDER BY " + DropDown_Param + " ASC"
        ElseIf DropDown_Param = "Equipment_Asset_Number" Then
            SQL_commandString = "SELECT " + DropDown_Param + " FROM tbl_Equipments WHERE Equipment_PPE_Number = '" + EquipType_Param + "' AND Equipment_Description='" + EquipName_Param + "' ORDER BY " + DropDown_Param + " ASC"
        Else
            SQL_commandString = "SELECT " + DropDown_Param + " FROM tbl_Equipments WHERE Equipment_Description = '" + EquipName_Param + "' AND Equipment_PPE_Number= '" + EquipPPE_Param + "' AND  Equipment_Asset_Number = '" + EquipType_Param + "' ORDER BY Equipment_Serial"
        End If

        Dim sqlCmd As New SqlCommand(SQL_commandString, sqlConn)

        sqlConn.Open()

        reader = sqlCmd.ExecuteReader

        While reader.Read()
            DropDown_Options.Add(reader(DropDown_Param))
        End While

        sqlConn.Close()

        Return serializer.Serialize(DropDown_Options)
    End Function

    Protected Shared Function GetEquipType()
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

        Return serializer.Serialize(equipType_Options)
    End Function

End Class