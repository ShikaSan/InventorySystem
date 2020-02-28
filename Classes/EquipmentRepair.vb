Public Class EquipmentRepair : Inherits dbhelper
    Public Function getData()

    End Function

    Public Function getStatus() As DataSet
        Dim result = New DataSet
        Dim strQuery As New StringBuilder
        Dim ds As DataSet

        strQuery.Append("SELECT DISTINCT Equipment_Status FROM tbl_Equipments WHERE Equipment_Status NOT IN ('On Stock', 'Assigned')")

        ds = GetDataSet(strQuery.ToString, "")

        If Not ds Is Nothing Then
            result = ds
        Else
            result = Nothing
        End If
        Return result
    End Function
End Class
