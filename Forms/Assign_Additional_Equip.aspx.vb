Imports System.Data
Imports System.Data.Sql
Imports System.Data.SqlClient
Imports System.Configuration
Imports System.Web.Services
Imports System.Web.Script.Services
Imports System.Web.Script.Serialization
Imports Newtonsoft.Json


Public Class Assign_Additional_Equip
    Inherits System.Web.UI.Page

    Protected Sub Button1_Click(sender As Object, e As EventArgs)
        'Dim counterVar As Integer = hiddenCounter.Value
        'Reference code 1
        'Dim typeValues() As String = Request.Form.GetValues("selectType")
        'Dim descValues() As String = Request.Form.GetValues("selectDesc")
        'Dim ppeValues() As String = Request.Form.GetValues("selectPPE")
        'Dim assetValues() As String = Request.Form.GetValues("selectAsset")
        'Dim serialValues() As String = Request.Form.GetValues("selectSerial")

        'Dim isValidated As Boolean = False

        'For index As Integer = 0 To counterVar
        '    If typeValues(index) = 0 Then
        '        isValidated = False
        '        Exit For
        '    ElseIf descValues(index) = 0 Then
        '        isValidated = False
        '        Exit For
        '    ElseIf ppeValues(index) = 0 Then
        '        isValidated = False
        '        Exit For
        '    ElseIf assetValues(index) = 0 Then
        '        isValidated = False
        '        Exit For
        '    ElseIf serialValues(index) = 0 Then
        '        isValidated = False
        '        Exit For
        '    Else
        '        isValidated = True
        '    End If
        'Next

        'If isValidated = True Then
        '    For index As Integer = 0 To counterVar
        '        'Code to insert to database here
        '        Response.Write("<script>alert('Yehey!!!')</script>")
        '    Next
        'Else
        '    Response.Write("<script>alert('Error! One or more fields are empty.')</script>")
        'End If

        'End of reference code 1

        'Reference code 2
        'Dim ctr As Integer = 0
        'Dim selectType As String = ""
        'Dim selectDesc As String = ""
        'Dim selectPPE As String = ""
        'Dim selectAsset As String = ""
        'Dim selectSerial As String = ""


        'Do While ctr <= 4
        '    selectType = Request.Form("selectType" & ctr).ToString
        '    selectDesc = Request.Form("selectDesc" & ctr).ToString
        '    selectPPE = Request.Form("selectPPE" & ctr).ToString
        '    selectAsset = Request.Form("selectAsset" & ctr).ToString
        '    selectSerial = Request.Form("selectSerial" & ctr).ToString

        '    Response.Write("<script>alert('" + selectType + "')</script>")
        '    And so on 

        '    ctr = ctr + 1
        'Loop
        'End Of Reference code 2
    End Sub

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
    End Sub

    Protected Function GetEquipType()
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

    <WebMethod>
    Public Shared Function GetEquipDescription(EquipType As String)
        Dim equipDesc_Options As New List(Of String)
        Dim serializer As New JavaScriptSerializer
        Dim sqlConn As New SqlConnection
        Dim reader As SqlDataReader

        sqlConn.ConnectionString = ConfigurationManager.ConnectionStrings("ITSGinventory").ConnectionString

        Dim commString As New SqlCommand("SELECT DISTINCT(Equipment_Description) FROM tbl_Equipments WHERE Equipment_Type = '" + EquipType + "' ORDER BY Equipment_Description ASC", sqlConn)

        sqlConn.Open()

        reader = commString.ExecuteReader

        While reader.Read()
            equipDesc_Options.Add(reader("Equipment_Description"))
        End While

        sqlConn.Close()

        Return equipDesc_Options
    End Function

    Protected Sub Button2_Click(sender As Object, e As EventArgs)
        Response.Redirect("~/Forms/ViewAccountability.aspx")
    End Sub

End Class