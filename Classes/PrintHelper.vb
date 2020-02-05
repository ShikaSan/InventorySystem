Imports System.IO

Public Class PrintHelper : Inherits dbhelper
    'Public Sub New()
    'End Sub

    Public Shared Sub PrintWebControl(ByVal ctrl As Control)
        PrintWebControl(ctrl, String.Empty)
    End Sub

    Public Shared Sub PrintWebControl(ByVal ctrl As Control, ByVal Script As String)
        Dim stringWrite As StringWriter = New StringWriter()
        Dim htmlWrite As System.Web.UI.HtmlTextWriter = New System.Web.UI.HtmlTextWriter(stringWrite)
        If TypeOf ctrl Is WebControl Then
            Dim w As Unit = New Unit(100, UnitType.Percentage)
            CType(ctrl, WebControl).Width = w
        End If
        Dim pg As Page = New Page()
        pg.EnableEventValidation = False
        If Script <> String.Empty Then
            pg.ClientScript.RegisterStartupScript(pg.GetType(), "PrintJavaScript", Script)
        End If
        Dim frm As HtmlForm = New HtmlForm()
        pg.Controls.Add(frm)
        frm.Attributes.Add("runat", "server")
        frm.Controls.Add(ctrl)
        pg.DesignerInitialize()
        pg.RenderControl(htmlWrite)
        Dim strHTML As String = stringWrite.ToString()
        HttpContext.Current.Response.Clear()
        HttpContext.Current.Response.Write(strHTML)
        HttpContext.Current.Response.Write("<script>window.print();</script>")
        HttpContext.Current.Response.End()
    End Sub

    Public Function getNamesAcc() As DataSet
        Dim strQuery As New StringBuilder
        Dim ds As DataSet

        strQuery.Append("SELECT id, Assigned_To
                        FROM tbl_Assigned_To WHERE active = 'Yes'
                        ")

        ds = GetDataSet(strQuery.ToString, "")

        If Not ds Is Nothing Then
            getNamesAcc = ds
        Else
            getNamesAcc = Nothing
        End If
    End Function

    Public Function getPrintHeader(strID As String) As DataSet

        Dim strQuery As New StringBuilder
        Dim ds As DataSet

        strQuery.Append("Select a.[PC_num],
                        a.[Assigned_To],
                        b.Host_Name
                        From [tbl_Assigned_To] As a 
                        INNER Join tbl_Host_Names As b 
                        On a.PC_num = b.PC_num
                        Where a.id = '" & strID & "'")

        ds = GetDataSet(strQuery.ToString, "")

        If Not ds Is Nothing Then
            getPrintHeader = ds
        Else
            getPrintHeader = Nothing
        End If
    End Function

    Public Function getEqAcc(param_search As String) As DataSet
        Dim strQuery As New StringBuilder
        Dim ds As DataSet

        strQuery.Append("
                         SELECT 
                         a.Equipment_Type As Type, 
                         a.Equipment_Description as Description, 
                         a.Equipment_PPE_Number as 'PPE #', 
                         a.Equipment_Asset_Number As 'Asset #', 
                         a.Equipment_Serial AS 'Serial #',
                         a.Equipment_Remarks,
                         a.Equipment_Date_Acquired
                         FROM tbl_Equipments as a INNER JOIN 
                         tbl_Accountability as b ON a.Equipment_ID = b.Equipment_ID                     
                         WHERE b.PC_num = '" & param_search.Trim & "'
                         ")

        ds = GetDataSet(strQuery.ToString, "")

        If Not ds Is Nothing Then
            getEqAcc = ds
        Else
            getEqAcc = Nothing
        End If
    End Function

    Public Function LineItems(param_IDrft As String) As DataSet
        Dim strQuery As New StringBuilder
        Dim ds As DataSet

        strQuery.Append("
                         SELECT 
                         a.[ppe], 
                         a.[description],   
                         a.[serialNo],
                         a.[qty]
                         FROM [a_admin_rft_items] as a INNER JOIN 
                         [a_admin_rft_details] as b ON a.[idRFT] = b.[idRFT]                     
                         WHERE b.[idRFT] = '" & param_IDrft.Trim & "'
                         ")

        ds = GetDataSet(strQuery.ToString, "")

        If Not ds Is Nothing Then
            LineItems = ds
        Else
            LineItems = Nothing
        End If
    End Function

End Class
