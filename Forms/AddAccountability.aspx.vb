Imports Microsoft.Reporting.WebForms
Imports InventorySystem.Authentication
Imports System.DirectoryServices.AccountManagement
Imports System.Web.Script.Serialization.JavaScriptSerializer


Public Class AssignEquipment
    Inherits System.Web.UI.Page

    Private Shared strUserName As String = Nothing
    Private Shared strFullName As String = Nothing
    Private Shared ctrlData As Integer = 0
    Private Shared mode As String = "Save"
    Public searchAD As New List(Of String)()

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
                    lblAcode.Text = codegen()
                    setinital()
                    'setDept()
                    GetUserList()

                    Dim strdtNow As Date = DateTime.Now.ToShortDateString()

                    txtAssignDate.Text = strdtNow.ToString("MM/dd/yyyy")

                    Me.Master.getUserName(strFullName, strUserName)
                    'Session.Remove("Username")
                    'Session.Remove("FullName")
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

    Public Sub GetUserList()
        '--Initialize variables for user domain search editable dropdownlist field--
        Dim ctx As PrincipalContext = New PrincipalContext(ContextType.Domain)
        Dim searchExample As Principal = New UserPrincipal(ctx)
        Dim ps As PrincipalSearcher = New PrincipalSearcher(searchExample)
        Dim searchAD As New List(Of String)()
        '--END--

        '--For user domain search textbox autocomplete--
        For Each p As Principal In ps.FindAll()
            searchAD.Add(p.SamAccountName)
        Next
        '--END--

        txtSearchEmployeeDomain.DataSource = searchAD
        txtSearchEmployeeDomain.DataBind()
    End Sub

    Private Function codegen() As String
        Dim s As String = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789"
        Dim r As New Random
        Dim sb As New StringBuilder
        Dim out As String = ""
        Dim obj As New Inventory

start:
        For i As Integer = 1 To 4
            Dim idx As Integer = r.Next(0, 35)
            sb.Append(s.Substring(idx, 1))
        Next

        If obj.databaseValidation("tbl_Assigned_To", "aCode", sb.ToString) = 0 Then
            out = sb.ToString
        Else
            GoTo start
        End If

        Return out
    End Function

    Private Function getDesc(param_type As String) As DataTable
        Dim getData As New Assign_Equip
        Dim ds As New DataSet

        ds = getData.getDesc_OnAdd(param_type, ctrlData)
        getDesc = ds.Tables(0)
    End Function

    Private Function getPPEList(param_type As String, param_desc As String) As DataTable
        Dim getData As New Assign_Equip
        Dim ds As New DataSet

        ds = getData.getPPEList(param_type, param_desc, ctrlData)
        getPPEList = ds.Tables(0)
    End Function

    Private Function getAssetList(param_type As String, param_desc As String) As DataTable
        Dim getData As New Assign_Equip
        Dim ds As New DataSet

        ds = getData.getAssetList(param_type, param_desc, ctrlData)
        getAssetList = ds.Tables(0)
    End Function

    Private Function getSerialList(param_type As String, param_desc As String) As DataTable
        Dim getData As New Assign_Equip
        Dim ds As New DataSet

        ds = getData.getSerialList(param_type, param_desc, ctrlData)
        getSerialList = ds.Tables(0)
    End Function

    Private Sub getAllData(param_id As String)
        Try
            setinital()
            'setDept()
            Dim getData As New View_Accountability
            Dim ds As New DataSet
            Dim getInit As New Inventory

            'ds = getInit.getUserAccountability(param_search, "", "")

            'txtHostName.Text = ds.Tables(0).Rows(0)(0).ToString()
            'txtAssignedTo.Text = ds.Tables(0).Rows(0)(1).ToString()
            'txtAssignDate.Text = ds.Tables(0).Rows(0)(3).ToString()
            'ddlDepartment.SelectedValue = ds.Tables(0).Rows(0)(5).ToString()
            'txtItemOS.Text = ds.Tables(0).Rows(0)(6).ToString()
            'txtPosition.Text = ds.Tables(0).Rows(0)(7).ToString()
            '"" = ds.Tables(0).Rows(0)(8).ToString()
            '"" = ds.Tables(0).Rows(0)(9).ToString()
            ''prevName = txtAssignedTo.Text

            'ddlLocation.Text = getData.getLoc(txtAssignedTo.Text)

            Dim type As String = getData.getTypes(param_id)
            ddlType.Text = type

            setDropDownList()
            If type = "Laptop" Then
                ds.Clear()
                dvLaptop.Visible = True
                ds = getData.getAllItems_Add(param_id, "Laptop")
                For x = 0 To ds.Tables(0).Rows.Count - 1
                    Select Case ds.Tables(0).Rows(x)(1).ToString()
                        Case "Laptop"
                            ddlLaptopDesc.Text = ds.Tables(0).Rows(x)(2).ToString()
                            setLaptopDetails()
                            ddlLaptopPPE.Text = ds.Tables(0).Rows(x)(3).ToString()
                            ddlLaptopAsset.Text = ds.Tables(0).Rows(x)(4).ToString()
                            ddlLaptopSerial.Text = ds.Tables(0).Rows(x)(5).ToString()
                        Case "AC Adaptor"
                            ddlACDesc.Text = ds.Tables(0).Rows(x)(2).ToString()
                            setAcDetails()
                            ddlACPPE.Text = ds.Tables(0).Rows(x)(3).ToString()
                            ddlACAsset.Text = ds.Tables(0).Rows(x)(4).ToString()
                            ddlACSerial.Text = ds.Tables(0).Rows(x)(5).ToString()
                        Case "Battery"
                            ddlBatteryDesc.Text = ds.Tables(0).Rows(x)(2).ToString()
                            setBatteryDetails()
                            ddlBatteryPPE.Text = ds.Tables(0).Rows(x)(3).ToString()
                            ddlBatteryAsset.Text = ds.Tables(0).Rows(x)(4).ToString()
                            ddlBatterySerial.Text = ds.Tables(0).Rows(x)(5).ToString()
                        Case "RAM"
                            ddlRAMldesc.Text = ds.Tables(0).Rows(x)(2).ToString()
                            setRamLDetails()
                            ddlRAMlppe.Text = ds.Tables(0).Rows(x)(3).ToString()
                            ddlRAMlasset.Text = ds.Tables(0).Rows(x)(4).ToString()
                            ddlRAMlserial.Text = ds.Tables(0).Rows(x)(5).ToString()
                    End Select
                Next
            ElseIf type = "Desktop" Then
                ds.Clear()
                dvDesktop.Visible = True
                ds = getData.getAllItems(param_id, "Desktop")
                For x = 0 To ds.Tables(0).Rows.Count - 1
                    Select Case ds.Tables(0).Rows(x)(1).ToString()
                        Case "Casing"
                            ddlCasingDesc.Text = ds.Tables(0).Rows(x)(2).ToString()
                            setCasingDetails()
                            ddlCasingPPE.Text = ds.Tables(0).Rows(x)(3).ToString()
                            ddlCasingAsset.Text = ds.Tables(0).Rows(x)(4).ToString()
                            ddlCasingSerial.Text = ds.Tables(0).Rows(x)(5).ToString()
                        Case "CPU"
                            ddlCPUDesc.Text = ds.Tables(0).Rows(x)(2).ToString()
                            setCpuDetails()
                            ddlCPUPPE.Text = ds.Tables(0).Rows(x)(3).ToString()
                            ddlCPUAsset.Text = ds.Tables(0).Rows(x)(4).ToString()
                            ddlCPUSerial.Text = ds.Tables(0).Rows(x)(5).ToString()
                        Case "HDD"
                            ddlHDDDesc.Text = ds.Tables(0).Rows(x)(2).ToString()
                            setHddDetails()
                            ddlHDDPPE.Text = ds.Tables(0).Rows(x)(3).ToString()
                            ddlHDDAsset.Text = ds.Tables(0).Rows(x)(4).ToString()
                            ddlHDDSerial.Text = ds.Tables(0).Rows(x)(5).ToString()
                        Case "MOBO"
                            ddlMOBODesc.Text = ds.Tables(0).Rows(x)(2).ToString()
                            setMoboDetails()
                            ddlMOBOPPE.Text = ds.Tables(0).Rows(x)(3).ToString()
                            ddlMOBOAsset.Text = ds.Tables(0).Rows(x)(4).ToString()
                            ddlMOBOSerial.Text = ds.Tables(0).Rows(x)(5).ToString()
                        Case "RAM"
                            ddlRAMDesc.Text = ds.Tables(0).Rows(x)(2).ToString()
                            setRamDetails()
                            ddlRAMPPE.Text = ds.Tables(0).Rows(x)(3).ToString()
                            ddlRAMAsset.Text = ds.Tables(0).Rows(x)(4).ToString()
                            ddlRAMSerial.Text = ds.Tables(0).Rows(x)(5).ToString()
                        Case "Monitor"
                            ddlMonitorDesc.Text = ds.Tables(0).Rows(x)(2).ToString()
                            setMonitorDetails()
                            ddlMonitorPPE.Text = ds.Tables(0).Rows(x)(3).ToString()
                            ddlMonitorAsset.Text = ds.Tables(0).Rows(x)(4).ToString()
                            ddlMonitorSerial.Text = ds.Tables(0).Rows(x)(5).ToString()
                        Case "Mouse"
                            ddlMouseDesc.Text = ds.Tables(0).Rows(x)(2).ToString()
                            setMouseDetails()
                            ddlMousePPE.Text = ds.Tables(0).Rows(x)(3).ToString()
                            ddlMouseAsset.Text = ds.Tables(0).Rows(x)(4).ToString()
                            ddlMouseSerial.Text = ds.Tables(0).Rows(x)(5).ToString()
                        Case "Keyboard"
                            ddlKeyboardDesc.Text = ds.Tables(0).Rows(x)(2).ToString()
                            setKeyboardDetails()
                            ddlKeyboardPPE.Text = ds.Tables(0).Rows(x)(3).ToString()
                            ddlKeyboardAsset.Text = ds.Tables(0).Rows(x)(4).ToString()
                            ddlKeyboardSerial.Text = ds.Tables(0).Rows(x)(5).ToString()
                        Case "PSU"
                            ddlPSUDesc.Text = ds.Tables(0).Rows(x)(2).ToString()
                            setPsuDetails()
                            ddlPSUPPE.Text = ds.Tables(0).Rows(x)(3).ToString()
                            ddlPSUAsset.Text = ds.Tables(0).Rows(x)(4).ToString()
                            ddlPSUSerial.Text = ds.Tables(0).Rows(x)(5).ToString()
                        Case "IP Phone"
                            ddlIPPhoneDesc.Text = ds.Tables(0).Rows(x)(2).ToString()
                            setIpPhoneDetails()
                            ddlIPPhonePPE.Text = ds.Tables(0).Rows(x)(3).ToString()
                            ddlIPPhoneAsset.Text = ds.Tables(0).Rows(x)(4).ToString()
                            ddlIPPhoneSerial.Text = ds.Tables(0).Rows(x)(5).ToString()
                    End Select
                Next
            End If
            ds.Clear()
            ds = getData.getAllItems(param_id, "Additional")
            If ds.Tables.Count <> 0 Then
                dvGVadditional.Visible = True
                gvEqpList.DataSource = ds
                gvEqpList.DataBind()
                ds.Clear()
            End If
        Catch ex As Exception
            System.Web.HttpContext.Current.Response.Write("<script>alert(" & ControlChars.Quote & ex.ToString() & ControlChars.Quote & ");</script>")
        End Try
    End Sub

    Private Sub setDropDownList()
        Try
            If ddlType.Text = "Laptop" Then
                dvLaptop.Visible = True
                dvDesktop.Visible = False
                dvGVadditional.Visible = False
                dvAdditional.Visible = True

                getTypes()

                ddlLaptopDesc.DataSource = getDesc("Laptop")
                ddlLaptopDesc.DataTextField = "Equipment_Description"
                ddlLaptopDesc.DataBind()
                ddlLaptopDesc.Items.Insert(0, "")
                ddlLaptopDesc.Items(0).Value = 0
                ddlBatteryDesc.DataSource = getDesc("Battery")
                ddlBatteryDesc.DataTextField = "Equipment_Description"
                ddlBatteryDesc.DataBind()
                ddlBatteryDesc.Items.Insert(0, "")
                ddlBatteryDesc.Items(0).Value = 0

                ddlACDesc.DataSource = getDesc("AC Adaptor")
                ddlACDesc.DataTextField = "Equipment_Description"
                ddlACDesc.DataBind()
                ddlACDesc.Items.Insert(0, "")
                ddlACDesc.Items(0).Value = 0

                ddlRAMldesc.DataSource = getDesc("Memory")
                ddlRAMldesc.DataTextField = "Equipment_Description"
                ddlRAMldesc.DataBind()
                ddlRAMldesc.Items.Insert(0, "")
                ddlRAMldesc.Items(0).Value = 0
                'System.Web.HttpContext.Current.Response.Write("<script>alert(" & ControlChars.Quote & "LAPTOP" & ControlChars.Quote & ");</script>")
            ElseIf ddlType.Text = "Desktop" Then
                dvDesktop.Visible = True
                dvLaptop.Visible = False
                dvGVadditional.Visible = False
                dvAdditional.Visible = True
                getTypes()
                ddlCasingDesc.DataSource = getDesc("Casing")
                ddlCasingDesc.DataTextField = "Equipment_Description"
                ddlCasingDesc.DataBind()
                ddlCasingDesc.Items.Insert(0, "")
                ddlCasingDesc.Items(0).Value = 0
                ddlCPUDesc.DataSource = getDesc("CPU")
                ddlCPUDesc.DataTextField = "Equipment_Description"
                ddlCPUDesc.DataBind()
                ddlCPUDesc.Items.Insert(0, "")
                ddlCPUDesc.Items(0).Value = 0
                ddlHDDDesc.DataSource = getDesc("HDD")
                ddlHDDDesc.DataTextField = "Equipment_Description"
                ddlHDDDesc.DataBind()
                ddlHDDDesc.Items.Insert(0, "")
                ddlHDDDesc.Items(0).Value = 0
                ddlMOBODesc.DataSource = getDesc("MOBO")
                ddlMOBODesc.DataTextField = "Equipment_Description"
                ddlMOBODesc.DataBind()
                ddlMOBODesc.Items.Insert(0, "")
                ddlMOBODesc.Items(0).Value = 0
                ddlRAMDesc.DataSource = getDesc("RAM")
                ddlRAMDesc.DataTextField = "Equipment_Description"
                ddlRAMDesc.DataBind()
                ddlRAMDesc.Items.Insert(0, "")
                ddlRAMDesc.Items(0).Value = 0
                ddlKeyboardDesc.DataSource = getDesc("Keyboard")
                ddlKeyboardDesc.DataTextField = "Equipment_Description"
                ddlKeyboardDesc.DataBind()
                ddlKeyboardDesc.Items.Insert(0, "")
                ddlKeyboardDesc.Items(0).Value = 0
                ddlMouseDesc.DataSource = getDesc("Mouse")
                ddlMouseDesc.DataTextField = "Equipment_Description"
                ddlMouseDesc.DataBind()
                ddlMouseDesc.Items.Insert(0, "")
                ddlMouseDesc.Items(0).Value = 0
                ddlMonitorDesc.DataSource = getDesc("Monitor")
                ddlMonitorDesc.DataTextField = "Equipment_Description"
                ddlMonitorDesc.DataBind()
                ddlMonitorDesc.Items.Insert(0, "")
                ddlMonitorDesc.Items(0).Value = 0
                ddlPSUDesc.DataSource = getDesc("PSU")
                ddlPSUDesc.DataTextField = "Equipment_Description"
                ddlPSUDesc.DataBind()
                ddlPSUDesc.Items.Insert(0, "")
                ddlPSUDesc.Items(0).Value = 0
                ddlIPPhoneDesc.DataSource = getDesc("IP Phone")
                ddlIPPhoneDesc.DataTextField = "Equipment_Description"
                ddlIPPhoneDesc.DataBind()
                ddlIPPhoneDesc.Items.Insert(0, "")
                ddlIPPhoneDesc.Items(0).Value = 0
                'System.Web.HttpContext.Current.Response.Write("<script>alert(" & ControlChars.Quote & "DESKTOP" & ControlChars.Quote & ");</script>")
            ElseIf ddlType.Text = "Others" Then
                dvDesktop.Visible = False
                dvLaptop.Visible = False
                dvGVadditional.Visible = True
                dvAdditional.Visible = False
                getTypes()
                'System.Web.HttpContext.Current.Response.Write("<script>alert(" & ControlChars.Quote & "OTHERS" & ControlChars.Quote & ");</script>")
            Else
                dvDesktop.Visible = False
                dvLaptop.Visible = False
                dvGVadditional.Visible = False
                dvAdditional.Visible = False
                'System.Web.HttpContext.Current.Response.Write("<script>alert(" & ControlChars.Quote & "ELSE" & ControlChars.Quote & ");</script>")
            End If
        Catch ex As Exception
            'System.Web.HttpContext.Current.Response.Write("<script>alert(" & ControlChars.Quote & ex.ToString() & ControlChars.Quote & ");</script>")
            MsgBox(ex.Message)
        End Try
    End Sub

    Protected Sub ddlType_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlType.SelectedIndexChanged
        setDropDownList()
    End Sub

    Protected Sub btnSave_Click(sender As Object, e As EventArgs)
        Dim clsI As New Inventory
        If clsI.databaseValidation("tbl_Assigned_To", "Assigned_To", txtAssignedTo.Text.Trim & "' AND active = 'Yes ") = 0 Then
            'If clsI.databaseValidation("tbl_Host_Names", "RFTnum", "".Trim) = 0 Then
            Dim save As New Assign_Equip
            Dim pcnum As Integer = 0
            Dim counter As Integer = 0

            If save.validatePCnumber(txtHostName.Text, ddlType.Text) > 0 Then
                pcnum = 0
            Else
                pcnum = save.getPCnumber() + 1
            End If

            If ddlType.Text = "Laptop" Then
                If txtHostName.Text.Trim <> "" And txtAssignedTo.Text.Trim <> "" And txtPosition.Text.Trim <> "" And txtAssignDate.Text.Trim <> "" And txtItemOS.Text.Trim <> "" And txtDepartment.Text <> "" And ddlLocation.Text <> "" And ddlType.Text <> "" Then
                    If ddlLaptopDesc.Text <> "" Then
                        Dim result = save.saveAssign("Laptop", ddlLaptopPPE.Text, ddlLaptopAsset.Text, ddlLaptopSerial.Text, txtAssignedTo.Text, txtAssignDate.Text, ddlType.Text, ddlLocation.SelectedIndex, txtHostName.Text, pcnum, "", "")
                        counter = counter + 1
                    End If
                    If ddlACDesc.Text <> "" Then
                        save.saveAssign("AC Adaptor", ddlACPPE.Text, ddlACAsset.Text, ddlACSerial.Text, txtAssignedTo.Text, txtAssignDate.Text, ddlType.Text, ddlLocation.SelectedIndex, txtHostName.Text, pcnum, "", "")
                        counter = counter + 1
                    End If
                    If ddlBatteryDesc.Text <> "" Then
                        save.saveAssign("Battery", ddlBatteryPPE.Text, ddlBatteryAsset.Text, ddlBatterySerial.Text, txtAssignedTo.Text, txtAssignDate.Text, ddlType.Text, ddlLocation.SelectedIndex, txtHostName.Text, pcnum, "", "")
                        counter = counter + 1
                    End If
                    If ddlRAMldesc.Text <> "" Then
                        save.saveAssign("RAM", ddlRAMlppe.Text, ddlRAMlasset.Text, ddlRAMlserial.Text, txtAssignedTo.Text, txtAssignDate.Text, ddlType.Text, ddlLocation.SelectedIndex, txtHostName.Text, pcnum, "", "")
                        counter = counter + 1
                    End If
                    If counter > 0 Then
                        save.saveInfo(txtHostName.Text, txtAssignedTo.Text, txtAssignDate.Text, txtDepartment.Text, txtItemOS.Text, txtPosition.Text, ddlType.Text, "Laptop", ddlLaptopDesc.Text, ddlLaptopPPE.Text, ddlLaptopAsset.Text, ddlLaptopSerial.Text, "", "", mode, lblAcode.Text)
                    End If
                Else
                    System.Web.HttpContext.Current.Response.Write("<script>alert(" & ControlChars.Quote & "Please enter required fields!" & ControlChars.Quote & ");</script>")
                End If
            ElseIf ddlType.Text = "Desktop" Then
                If txtHostName.Text.Trim <> "" And txtAssignedTo.Text.Trim <> "" And txtPosition.Text.Trim <> "" And txtAssignDate.Text.Trim <> "" And txtItemOS.Text.Trim <> "" And txtDepartment.Text <> "" And ddlLocation.Text <> "" And ddlType.Text <> "" Then
                    If ddlCasingDesc.Text <> "" Then
                        save.saveAssign("Casing", ddlCasingPPE.Text, ddlCasingAsset.Text, ddlCasingSerial.Text, txtAssignedTo.Text, txtAssignDate.Text, ddlType.Text, ddlLocation.SelectedIndex, txtHostName.Text, pcnum, "", "")
                        counter = counter + 1
                    End If
                    If ddlCPUDesc.Text <> "" Then
                        save.saveAssign("CPU", ddlCPUPPE.Text, ddlCPUAsset.Text, ddlCPUSerial.Text, txtAssignedTo.Text, txtAssignDate.Text, ddlType.Text, ddlLocation.SelectedIndex, txtHostName.Text, pcnum, "", "")
                        counter = counter + 1
                    End If
                    If ddlHDDDesc.Text <> "" Then
                        save.saveAssign("HDD", ddlHDDPPE.Text, ddlHDDAsset.Text, ddlHDDSerial.Text, txtAssignedTo.Text, txtAssignDate.Text, ddlType.Text, ddlLocation.SelectedIndex, txtHostName.Text, pcnum, "", "")
                        counter = counter + 1
                    End If
                    If ddlMOBODesc.Text <> "" Then
                        save.saveAssign("MOBO", ddlMOBOPPE.Text, ddlMOBOAsset.Text, ddlMOBOSerial.Text, txtAssignedTo.Text, txtAssignDate.Text, ddlType.Text, ddlLocation.SelectedIndex, txtHostName.Text, pcnum, "", "")
                        counter = counter + 1
                    End If
                    If ddlRAMDesc.Text <> "" Then
                        save.saveAssign("RAM", ddlRAMPPE.Text, ddlRAMAsset.Text, ddlRAMSerial.Text, txtAssignedTo.Text, txtAssignDate.Text, ddlType.Text, ddlLocation.SelectedIndex, txtHostName.Text, pcnum, "", "")
                        counter = counter + 1
                    End If
                    If ddlKeyboardDesc.Text <> "" Then
                        save.saveAssign("Keyboard", ddlKeyboardPPE.Text, ddlKeyboardAsset.Text, ddlKeyboardSerial.Text, txtAssignedTo.Text, txtAssignDate.Text, ddlType.Text, ddlLocation.SelectedIndex, txtHostName.Text, pcnum, "", "")
                        counter = counter + 1
                    End If
                    If ddlMouseDesc.Text <> "" Then
                        save.saveAssign("Mouse", ddlMousePPE.Text, ddlMouseAsset.Text, ddlMouseSerial.Text, txtAssignedTo.Text, txtAssignDate.Text, ddlType.Text, ddlLocation.SelectedIndex, txtHostName.Text, pcnum, "", "")
                        counter = counter + 1
                    End If
                    If ddlMonitorDesc.Text <> "" Then
                        save.saveAssign("Monitor", ddlMonitorPPE.Text, ddlMonitorAsset.Text, ddlMonitorSerial.Text, txtAssignedTo.Text, txtAssignDate.Text, ddlType.Text, ddlLocation.SelectedIndex, txtHostName.Text, pcnum, "", "")
                        counter = counter + 1
                    End If
                    If ddlIPPhoneDesc.Text <> "" Then
                        save.saveAssign("IP Phone", ddlIPPhonePPE.Text, ddlIPPhoneAsset.Text, ddlIPPhoneSerial.Text, txtAssignedTo.Text, txtAssignDate.Text, ddlType.Text, ddlLocation.SelectedIndex, txtHostName.Text, pcnum, "", "")
                        counter = counter + 1
                    End If
                    If ddlPSUDesc.Text <> "" Then
                        save.saveAssign("PSU", ddlPSUPPE.Text, ddlPSUAsset.Text, ddlPSUSerial.Text, txtAssignedTo.Text, txtAssignDate.Text, ddlType.Text, ddlLocation.SelectedIndex, txtHostName.Text, pcnum, "", "")
                        counter = counter + 1
                    End If
                    If counter > 0 Then
                        save.saveInfo(txtHostName.Text, txtAssignedTo.Text, txtAssignDate.Text, txtDepartment.Text, txtItemOS.Text, txtPosition.Text, ddlType.Text, "Casing", ddlCasingDesc.Text, ddlCasingPPE.Text, ddlCasingAsset.Text, ddlCasingSerial.Text, "", "", mode, lblAcode.Text)
                    End If
                Else
                    System.Web.HttpContext.Current.Response.Write("<script>alert(" & ControlChars.Quote & "Please enter required fields!" & ControlChars.Quote & ");</script>")
                End If
            ElseIf ddlType.Text = "Others" Then
                If gvEqpList.Rows.Count > 0 Then
                    Dim dt As New DataTable
                    dt.Columns.Add("Type")
                    dt.Columns.Add("Description")
                    dt.Columns.Add("PPE")
                    dt.Columns.Add("Asset")
                    dt.Columns.Add("Serial")
                    If gvEqpList.Rows.Count <> 0 Then
                        For Each row As GridViewRow In gvEqpList.Rows
                            Dim dr As DataRow = dt.NewRow()
                            For j = 0 To row.Cells.Count - 1
                                dr(j) = row.Cells(j).Text
                            Next
                            dt.Rows.Add(dr)
                        Next
                    End If

                    For x = 0 To dt.Rows.Count - 1
                        Dim add As New Assign_Equip
                        add.saveAssign(dt.Rows(x)(0).ToString(), dt.Rows(x)(2).ToString(), dt.Rows(x)(4).ToString(),
                                     dt.Rows(x)(3).ToString(), txtAssignedTo.Text, txtAssignDate.Text, "Others", ddlLocation.SelectedIndex, txtHostName.Text, pcnum, "", "")
                        counter = counter + 1
                    Next
                    If counter > 0 Then
                        save.saveInfo(txtHostName.Text,
                                  txtAssignedTo.Text,
                                  txtAssignDate.Text,
                                  txtDepartment.Text,
                                  txtItemOS.Text,
                                  txtPosition.Text,
                                  ddlType.Text,
                                  "Others",
                                  dt.Rows(0)(1).ToString(),
                                  dt.Rows(0)(2).ToString(),
                                  dt.Rows(0)(4).ToString(),
                                  dt.Rows(0)(3).ToString(),
                                  "",
                                  "",
                                  mode, lblAcode.Text)
                    End If
                End If
            End If
            If dvGVadditional.Visible = True Then
                If gvEqpList.Rows.Count > 0 Then
                    Dim dt As New DataTable
                    dt.Columns.Add("Type")
                    dt.Columns.Add("Description")
                    dt.Columns.Add("PPE")
                    dt.Columns.Add("Asset")
                    dt.Columns.Add("Serial")
                    If gvEqpList.Rows.Count <> 0 Then
                        For Each row As GridViewRow In gvEqpList.Rows
                            Dim dr As DataRow = dt.NewRow()
                            For j = 0 To row.Cells.Count - 1
                                dr(j) = row.Cells(j).Text
                            Next
                            dt.Rows.Add(dr)
                        Next
                    End If

                    For x = 0 To dt.Rows.Count - 1
                        Dim add As New Assign_Equip
                        add.saveAssign(dt.Rows(x)(0).ToString(), dt.Rows(x)(2).ToString(), dt.Rows(x)(4).ToString(),
                                     dt.Rows(x)(3).ToString(), txtAssignedTo.Text, txtAssignDate.Text, "Additional", ddlLocation.SelectedIndex, txtHostName.Text, pcnum, "", "")
                        counter = counter + 1
                    Next
                    If counter > 0 Then
                        If ddlType.Text = "Laptop" Then
                            save.saveInfo(txtHostName.Text, txtAssignedTo.Text, txtAssignDate.Text, txtDepartment.Text, txtItemOS.Text, txtPosition.Text, ddlType.Text, "Additional", ddlLaptopDesc.Text, ddlLaptopPPE.Text, ddlLaptopAsset.Text, ddlLaptopSerial.Text, "", "", mode, lblAcode.Text)
                        ElseIf ddlType.Text = "Desktop" Then
                            save.saveInfo(txtHostName.Text, txtAssignedTo.Text, txtAssignDate.Text, txtDepartment.Text, txtItemOS.Text, txtPosition.Text, ddlType.Text, "Additional", ddlCasingDesc.Text, ddlCasingPPE.Text, ddlCasingAsset.Text, ddlCasingSerial.Text, "", "", mode, lblAcode.Text)
                        End If
                    End If
                End If
            End If
            If counter > 0 Then
                'Dim send As New clsEmailNotif
                'send.SendMultipleRows(txtAssignedTo.Text, ddlDepartment.SelectedValue)

                'updPnl1.Update()
                'idMesBox.Visible = True
                'ScriptManager.RegisterStartupScript(Me, Me.[GetType](), "Pop", "openMsgBox();", True)
                System.Web.HttpContext.Current.Response.Write("<script>alert(" & ControlChars.Quote & "User added to database" & ControlChars.Quote & ");</script>")
                clear(1)
                Session("Username") = strUserName
                Session("FullName") = strFullName
                strUserName = ""
                strFullName = ""
                Response.Redirect("~/Forms/UserAccountability.aspx")
            Else
                System.Web.HttpContext.Current.Response.Write("<script>alert(" & ControlChars.Quote & "Please select at least 1 equipment to assign" & ControlChars.Quote & ");</script>")
            End If
            'Else
            'System.Web.HttpContext.Current.Response.Write("<script>alert(" & ControlChars.Quote & "Please enter required fields!" & ControlChars.Quote & ");</script>")
            'End If
        Else
            System.Web.HttpContext.Current.Response.Write("<script>alert(" & ControlChars.Quote & "User already exist." & ControlChars.Quote & ");</script>")
        End If
    End Sub

    Private Sub clear(param_mode As Integer)
        Try
            If param_mode <> 0 Then
                txtDepartment.Text = ""
                txtHostName.Text = ""
                txtAssignDate.Text = ""
                txtAssignedTo.Text = ""
                txtPosition.Text = ""
                txtItemOS.Text = ""
                ddlLocation.SelectedIndex = 0
                ddlType.SelectedIndex = 0
            End If
            If dvLaptop.Visible = True Then
                ddlLaptopDesc.SelectedIndex = 0
                ddlLaptopPPE.SelectedIndex = 0
                ddlLaptopAsset.SelectedIndex = 0
                ddlLaptopSerial.SelectedIndex = 0
                ddlACDesc.SelectedIndex = 0
                ddlACPPE.SelectedIndex = 0
                ddlACAsset.SelectedIndex = 0
                ddlACSerial.SelectedIndex = 0
                ddlBatteryDesc.SelectedIndex = 0
                ddlBatteryPPE.SelectedIndex = 0
                ddlBatteryAsset.SelectedIndex = 0
                ddlBatterySerial.SelectedIndex = 0
                ddlRAMldesc.SelectedIndex = 0
                ddlRAMlppe.SelectedIndex = 0
                ddlRAMlasset.SelectedIndex = 0
                ddlRAMlserial.SelectedIndex = 0
            End If
            If dvDesktop.Visible = True Then
                ddlCasingDesc.SelectedIndex = 0
                ddlCasingPPE.SelectedIndex = 0
                ddlCasingAsset.SelectedIndex = 0
                ddlCasingSerial.SelectedIndex = 0
                ddlCPUDesc.SelectedIndex = 0
                ddlCPUPPE.SelectedIndex = 0
                ddlCPUAsset.SelectedIndex = 0
                ddlCPUSerial.SelectedIndex = 0
                ddlHDDDesc.SelectedIndex = 0
                ddlHDDPPE.SelectedIndex = 0
                ddlHDDAsset.SelectedIndex = 0
                ddlHDDSerial.SelectedIndex = 0
                ddlMOBODesc.SelectedIndex = 0
                ddlMOBOPPE.SelectedIndex = 0
                ddlMOBOAsset.SelectedIndex = 0
                ddlMOBOSerial.SelectedIndex = 0
                ddlRAMDesc.SelectedIndex = 0
                ddlRAMPPE.SelectedIndex = 0
                ddlRAMAsset.SelectedIndex = 0
                ddlRAMSerial.SelectedIndex = 0
                ddlKeyboardDesc.SelectedIndex = 0
                ddlKeyboardPPE.SelectedIndex = 0
                ddlKeyboardAsset.SelectedIndex = 0
                ddlKeyboardSerial.SelectedIndex = 0
                ddlMouseDesc.SelectedIndex = 0
                ddlMousePPE.SelectedIndex = 0
                ddlMouseAsset.SelectedIndex = 0
                ddlMouseSerial.SelectedIndex = 0
                ddlMonitorDesc.SelectedIndex = 0
                ddlMonitorPPE.SelectedIndex = 0
                ddlMonitorAsset.SelectedIndex = 0
                ddlMonitorSerial.SelectedIndex = 0
                ddlIPPhoneDesc.SelectedIndex = 0
                ddlIPPhonePPE.SelectedIndex = 0
                ddlIPPhoneAsset.SelectedIndex = 0
                ddlIPPhoneSerial.SelectedIndex = 0
                ddlPSUDesc.SelectedIndex = 0
                ddlPSUPPE.SelectedIndex = 0
                ddlPSUAsset.SelectedIndex = 0
                ddlPSUSerial.SelectedIndex = 0
            End If
        Catch
            'Session("Username") = strUserName
            'Session("FullName") = strFullName
            'strUserName = ""
            'strFullName = ""
            'Response.Redirect("~/Forms/UserAccountability.aspx")
        End Try
    End Sub

    Protected Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        clear(1)
        'Session("Username") = strUserName
        'Session("FullName") = strFullName
        'strUserName = ""
        'strFullName = ""2
        Response.Redirect("~/Forms/UserAccountability.aspx")
    End Sub
    '#Region "Fields"
    '    Protected Sub ddlCasingPPE_SelectedIndexChanged(sender As Object, e As EventArgs)
    '        Dim getOthers As New clsAssignEquip
    '        Dim ds As New DataSet

    '        ds = getOthers.getOtherDetails("PPE", ddlCasingPPE.Text, ddlCasingDesc.Text)
    '        ddlCasingAsset.Text = ds.Tables(0).Rows(0)(1).ToString()
    '        ddlCasingSerial.Text = ds.Tables(0).Rows(0)(0).ToString()
    '    End Sub

    '    Protected Sub ddlCasingAsset_SelectedIndexChanged(sender As Object, e As EventArgs)
    '        Dim getOthers As New clsAssignEquip
    '        Dim ds As New DataSet

    '        ds = getOthers.getOtherDetails("Asset", ddlCasingAsset.Text, ddlCasingDesc.Text)
    '        ddlCasingPPE.Text = ds.Tables(0).Rows(0)(1).ToString()
    '        ddlCasingSerial.Text = ds.Tables(0).Rows(0)(0).ToString()
    '    End Sub

    '    Protected Sub ddlCasingSerial_SelectedIndexChanged(sender As Object, e As EventArgs)
    '        Dim getOthers As New clsAssignEquip
    '        Dim ds As New DataSet

    '        ds = getOthers.getOtherDetails("Serial", ddlCasingSerial.Text, ddlCasingDesc.Text)
    '        ddlCasingAsset.Text = ds.Tables(0).Rows(0)(1).ToString()
    '        ddlCasingPPE.Text = ds.Tables(0).Rows(0)(0).ToString()
    '    End Sub

    '    Protected Sub ddlCPUPPE_SelectedIndexChanged(sender As Object, e As EventArgs)
    '        Dim getOthers As New clsAssignEquip
    '        Dim ds As New DataSet

    '        ds = getOthers.getOtherDetails("PPE", ddlCPUPPE.Text, ddlCPUDesc.Text)
    '        ddlCPUAsset.Text = ds.Tables(0).Rows(0)(1).ToString()
    '        ddlCPUSerial.Text = ds.Tables(0).Rows(0)(0).ToString()
    '    End Sub

    '    Protected Sub ddlCPUAsset_SelectedIndexChanged(sender As Object, e As EventArgs)
    '        Dim getOthers As New clsAssignEquip
    '        Dim ds As New DataSet

    '        ds = getOthers.getOtherDetails("Asset", ddlCPUAsset.Text, ddlCPUDesc.Text)
    '        ddlCPUPPE.Text = ds.Tables(0).Rows(0)(1).ToString()
    '        ddlCPUSerial.Text = ds.Tables(0).Rows(0)(0).ToString()
    '    End Sub

    '    Protected Sub ddlCPUSerial_SelectedIndexChanged(sender As Object, e As EventArgs)
    '        Dim getOthers As New clsAssignEquip
    '        Dim ds As New DataSet

    '        ds = getOthers.getOtherDetails("Serial", ddlCPUSerial.Text, ddlCPUDesc.Text)
    '        ddlCPUAsset.Text = ds.Tables(0).Rows(0)(1).ToString()
    '        ddlCPUPPE.Text = ds.Tables(0).Rows(0)(0).ToString()
    '    End Sub

    '    Protected Sub ddlHDDPPE_SelectedIndexChanged(sender As Object, e As EventArgs)
    '        Dim getOthers As New clsAssignEquip
    '        Dim ds As New DataSet

    '        ds = getOthers.getOtherDetails("PPE", ddlHDDPPE.Text, ddlHDDDesc.Text)
    '        ddlHDDAsset.Text = ds.Tables(0).Rows(0)(1).ToString()
    '        ddlHDDSerial.Text = ds.Tables(0).Rows(0)(0).ToString()
    '    End Sub

    '    Protected Sub ddlHDDAsset_SelectedIndexChanged(sender As Object, e As EventArgs)
    '        Dim getOthers As New clsAssignEquip
    '        Dim ds As New DataSet

    '        ds = getOthers.getOtherDetails("Asset", ddlHDDAsset.Text, ddlHDDDesc.Text)
    '        ddlHDDPPE.Text = ds.Tables(0).Rows(0)(1).ToString()
    '        ddlHDDSerial.Text = ds.Tables(0).Rows(0)(0).ToString()
    '    End Sub

    '    Protected Sub ddlHDDSerial_SelectedIndexChanged(sender As Object, e As EventArgs)
    '        Dim getOthers As New clsAssignEquip
    '        Dim ds As New DataSet

    '        ds = getOthers.getOtherDetails("Serial", ddlHDDSerial.Text, ddlHDDDesc.Text)
    '        ddlHDDAsset.Text = ds.Tables(0).Rows(0)(1).ToString()
    '        ddlHDDPPE.Text = ds.Tables(0).Rows(0)(0).ToString()
    '    End Sub

    '    Protected Sub ddlMOBOPPE_SelectedIndexChanged(sender As Object, e As EventArgs)
    '        Dim getOthers As New clsAssignEquip
    '        Dim ds As New DataSet

    '        ds = getOthers.getOtherDetails("PPE", ddlMOBOPPE.Text, ddlMOBODesc.Text)
    '        ddlMOBOAsset.Text = ds.Tables(0).Rows(0)(1).ToString()
    '        ddlMOBOSerial.Text = ds.Tables(0).Rows(0)(0).ToString()
    '    End Sub

    '    Protected Sub ddlMOBOAsset_SelectedIndexChanged(sender As Object, e As EventArgs)
    '        Dim getOthers As New clsAssignEquip
    '        Dim ds As New DataSet

    '        ds = getOthers.getOtherDetails("Asset", ddlMOBOAsset.Text, ddlMOBODesc.Text)
    '        ddlMOBOPPE.Text = ds.Tables(0).Rows(0)(1).ToString()
    '        ddlMOBOSerial.Text = ds.Tables(0).Rows(0)(0).ToString()
    '    End Sub

    '    Protected Sub ddlMOBOSerial_SelectedIndexChanged(sender As Object, e As EventArgs)
    '        Dim getOthers As New clsAssignEquip
    '        Dim ds As New DataSet

    '        ds = getOthers.getOtherDetails("Serial", ddlMOBOSerial.Text, ddlMOBODesc.Text)
    '        ddlMOBOAsset.Text = ds.Tables(0).Rows(0)(1).ToString()
    '        ddlMOBOPPE.Text = ds.Tables(0).Rows(0)(0).ToString()
    '    End Sub

    '    Protected Sub ddlRAMPPE_SelectedIndexChanged(sender As Object, e As EventArgs)
    '        Dim getOthers As New clsAssignEquip
    '        Dim ds As New DataSet

    '        ds = getOthers.getOtherDetails("PPE", ddlRAMPPE.Text, ddlRAMDesc.Text)
    '        ddlRAMAsset.Text = ds.Tables(0).Rows(0)(1).ToString()
    '        ddlRAMSerial.Text = ds.Tables(0).Rows(0)(0).ToString()
    '    End Sub

    '    Protected Sub ddlRAMAsset_SelectedIndexChanged(sender As Object, e As EventArgs)
    '        Dim getOthers As New clsAssignEquip
    '        Dim ds As New DataSet

    '        ds = getOthers.getOtherDetails("Asset", ddlRAMAsset.Text, ddlRAMDesc.Text)
    '        ddlRAMPPE.Text = ds.Tables(0).Rows(0)(1).ToString()
    '        ddlRAMSerial.Text = ds.Tables(0).Rows(0)(0).ToString()
    '    End Sub

    '    Protected Sub ddlRAMSerial_SelectedIndexChanged(sender As Object, e As EventArgs)
    '        Dim getOthers As New clsAssignEquip
    '        Dim ds As New DataSet

    '        ds = getOthers.getOtherDetails("Serial", ddlRAMSerial.Text, ddlRAMDesc.Text)
    '        ddlRAMAsset.Text = ds.Tables(0).Rows(0)(1).ToString()
    '        ddlRAMPPE.Text = ds.Tables(0).Rows(0)(0).ToString()
    '    End Sub

    '    Protected Sub ddlMonitorPPE_SelectedIndexChanged(sender As Object, e As EventArgs)
    '        Dim getOthers As New clsAssignEquip
    '        Dim ds As New DataSet

    '        ds = getOthers.getOtherDetails("PPE", ddlMonitorPPE.Text, ddlMonitorDesc.Text)
    '        ddlMonitorAsset.Text = ds.Tables(0).Rows(0)(1).ToString()
    '        ddlMonitorSerial.Text = ds.Tables(0).Rows(0)(0).ToString()
    '    End Sub

    '    Protected Sub ddlMonitorAsset_SelectedIndexChanged(sender As Object, e As EventArgs)
    '        Dim getOthers As New clsAssignEquip
    '        Dim ds As New DataSet

    '        ds = getOthers.getOtherDetails("Asset", ddlMonitorAsset.Text, ddlMonitorDesc.Text)
    '        ddlMonitorPPE.Text = ds.Tables(0).Rows(0)(1).ToString()
    '        ddlMonitorSerial.Text = ds.Tables(0).Rows(0)(0).ToString()
    '    End Sub

    '    Protected Sub ddlMonitorSerial_SelectedIndexChanged(sender As Object, e As EventArgs)
    '        Dim getOthers As New clsAssignEquip
    '        Dim ds As New DataSet

    '        ds = getOthers.getOtherDetails("Serial", ddlMonitorSerial.Text, ddlMonitorDesc.Text)
    '        ddlMonitorAsset.Text = ds.Tables(0).Rows(0)(1).ToString()
    '        ddlMonitorPPE.Text = ds.Tables(0).Rows(0)(0).ToString()
    '    End Sub

    '    Protected Sub ddlKeyboardPPE_SelectedIndexChanged(sender As Object, e As EventArgs)
    '        Dim getOthers As New clsAssignEquip
    '        Dim ds As New DataSet

    '        ds = getOthers.getOtherDetails("PPE", ddlKeyboardPPE.Text, ddlKeyboardDesc.Text)
    '        ddlKeyboardAsset.Text = ds.Tables(0).Rows(0)(1).ToString()
    '        ddlKeyboardSerial.Text = ds.Tables(0).Rows(0)(0).ToString()
    '    End Sub

    '    Protected Sub ddlKeyboardAsset_SelectedIndexChanged(sender As Object, e As EventArgs)
    '        Dim getOthers As New clsAssignEquip
    '        Dim ds As New DataSet

    '        ds = getOthers.getOtherDetails("Asset", ddlKeyboardAsset.Text, ddlKeyboardDesc.Text)
    '        ddlKeyboardPPE.Text = ds.Tables(0).Rows(0)(1).ToString()
    '        ddlKeyboardSerial.Text = ds.Tables(0).Rows(0)(0).ToString()
    '    End Sub

    '    Protected Sub ddlKeyboardSerial_SelectedIndexChanged(sender As Object, e As EventArgs)
    '        Dim getOthers As New clsAssignEquip
    '        Dim ds As New DataSet

    '        ds = getOthers.getOtherDetails("Serial", ddlKeyboardSerial.Text, ddlKeyboardDesc.Text)
    '        ddlKeyboardAsset.Text = ds.Tables(0).Rows(0)(1).ToString()
    '        ddlKeyboardPPE.Text = ds.Tables(0).Rows(0)(0).ToString()
    '    End Sub

    '    Protected Sub ddlMousePPE_SelectedIndexChanged(sender As Object, e As EventArgs)
    '        Dim getOthers As New clsAssignEquip
    '        Dim ds As New DataSet

    '        ds = getOthers.getOtherDetails("PPE", ddlMousePPE.Text, ddlMouseDesc.Text)
    '        ddlMouseAsset.Text = ds.Tables(0).Rows(0)(1).ToString()
    '        ddlMouseSerial.Text = ds.Tables(0).Rows(0)(0).ToString()
    '    End Sub

    '    Protected Sub ddlMouseAsset_SelectedIndexChanged(sender As Object, e As EventArgs)
    '        Dim getOthers As New clsAssignEquip
    '        Dim ds As New DataSet

    '        ds = getOthers.getOtherDetails("Asset", ddlMouseAsset.Text, ddlMouseDesc.Text)
    '        ddlMousePPE.Text = ds.Tables(0).Rows(0)(1).ToString()
    '        ddlMouseSerial.Text = ds.Tables(0).Rows(0)(0).ToString()
    '    End Sub

    '    Protected Sub ddlMouseSerial_SelectedIndexChanged(sender As Object, e As EventArgs)
    '        Dim getOthers As New clsAssignEquip
    '        Dim ds As New DataSet

    '        ds = getOthers.getOtherDetails("Serial", ddlMouseSerial.Text, ddlMouseDesc.Text)
    '        ddlMouseAsset.Text = ds.Tables(0).Rows(0)(1).ToString()
    '        ddlMousePPE.Text = ds.Tables(0).Rows(0)(0).ToString()
    '    End Sub

    '    Protected Sub ddlPSUPPE_SelectedIndexChanged(sender As Object, e As EventArgs)
    '        Dim getOthers As New clsAssignEquip
    '        Dim ds As New DataSet

    '        ds = getOthers.getOtherDetails("PPE", ddlPSUPPE.Text, ddlPSUDesc.Text)
    '        ddlPSUAsset.Text = ds.Tables(0).Rows(0)(1).ToString()
    '        ddlPSUSerial.Text = ds.Tables(0).Rows(0)(0).ToString()
    '    End Sub

    '    Protected Sub ddlPSUAsset_SelectedIndexChanged(sender As Object, e As EventArgs)
    '        Dim getOthers As New clsAssignEquip
    '        Dim ds As New DataSet

    '        ds = getOthers.getOtherDetails("Asset", ddlPSUAsset.Text, ddlPSUDesc.Text)
    '        ddlPSUPPE.Text = ds.Tables(0).Rows(0)(1).ToString()
    '        ddlPSUSerial.Text = ds.Tables(0).Rows(0)(0).ToString()
    '    End Sub

    '    Protected Sub ddlPSUSerial_SelectedIndexChanged(sender As Object, e As EventArgs)
    '        Dim getOthers As New clsAssignEquip
    '        Dim ds As New DataSet

    '        ds = getOthers.getOtherDetails("Serial", ddlPSUSerial.Text, ddlPSUDesc.Text)
    '        ddlPSUAsset.Text = ds.Tables(0).Rows(0)(1).ToString()
    '        ddlPSUPPE.Text = ds.Tables(0).Rows(0)(0).ToString()
    '    End Sub

    '    Protected Sub ddlIPPhonePPE_SelectedIndexChanged(sender As Object, e As EventArgs)
    '        Dim getOthers As New clsAssignEquip
    '        Dim ds As New DataSet

    '        ds = getOthers.getOtherDetails("PPE", ddlIPPhonePPE.Text, ddlIPPhoneDesc.Text)
    '        ddlIPPhoneAsset.Text = ds.Tables(0).Rows(0)(1).ToString()
    '        ddlIPPhoneSerial.Text = ds.Tables(0).Rows(0)(0).ToString()
    '    End Sub

    '    Protected Sub ddlIPPhoneAsset_SelectedIndexChanged(sender As Object, e As EventArgs)
    '        Dim getOthers As New clsAssignEquip
    '        Dim ds As New DataSet

    '        ds = getOthers.getOtherDetails("Asset", ddlIPPhoneAsset.Text, ddlIPPhoneDesc.Text)
    '        ddlIPPhonePPE.Text = ds.Tables(0).Rows(0)(1).ToString()
    '        ddlIPPhoneSerial.Text = ds.Tables(0).Rows(0)(0).ToString()
    '    End Sub

    '    Protected Sub ddlIPPhoneSerial_SelectedIndexChanged(sender As Object, e As EventArgs)
    '        Dim getOthers As New clsAssignEquip
    '        Dim ds As New DataSet

    '        ds = getOthers.getOtherDetails("Serial", ddlIPPhoneSerial.Text, ddlIPPhoneDesc.Text)
    '        ddlIPPhoneAsset.Text = ds.Tables(0).Rows(0)(1).ToString()
    '        ddlIPPhonePPE.Text = ds.Tables(0).Rows(0)(0).ToString()
    '    End Sub

    '    Protected Sub ddlLaptopPPE_SelectedIndexChanged(sender As Object, e As EventArgs)
    '        Dim getOthers As New clsAssignEquip
    '        Dim ds As New DataSet

    '        ds = getOthers.getOtherDetails("PPE", ddlLaptopPPE.Text, ddlLaptopDesc.Text)
    '        ddlLaptopAsset.Text = ds.Tables(0).Rows(0)(1).ToString()
    '        ddlLaptopSerial.Text = ds.Tables(0).Rows(0)(0).ToString()
    '    End Sub

    '    Protected Sub ddlLaptopAsset_SelectedIndexChanged(sender As Object, e As EventArgs)
    '        Dim getOthers As New clsAssignEquip
    '        Dim ds As New DataSet

    '        ds = getOthers.getOtherDetails("Asset", ddlLaptopAsset.Text, ddlLaptopDesc.Text)
    '        ddlLaptopPPE.Text = ds.Tables(0).Rows(0)(1).ToString()
    '        ddlLaptopSerial.Text = ds.Tables(0).Rows(0)(0).ToString()
    '    End Sub

    '    Protected Sub ddlLaptopSerial_SelectedIndexChanged(sender As Object, e As EventArgs)
    '        Dim getOthers As New clsAssignEquip
    '        Dim ds As New DataSet

    '        ds = getOthers.getOtherDetails("Serial", ddlLaptopSerial.Text, ddlLaptopDesc.Text)
    '        ddlLaptopAsset.Text = ds.Tables(0).Rows(0)(1).ToString()
    '        ddlLaptopPPE.Text = ds.Tables(0).Rows(0)(0).ToString()
    '    End Sub

    '    Protected Sub ddlBatteryPPE_SelectedIndexChanged(sender As Object, e As EventArgs)
    '        Dim getOthers As New clsAssignEquip
    '        Dim ds As New DataSet

    '        ds = getOthers.getOtherDetails("PPE", ddlBatteryPPE.Text, ddlBatteryDesc.Text)
    '        ddlBatteryAsset.Text = ds.Tables(0).Rows(0)(1).ToString()
    '        ddlBatterySerial.Text = ds.Tables(0).Rows(0)(0).ToString()
    '    End Sub

    '    Protected Sub ddlBatteryAsset_SelectedIndexChanged(sender As Object, e As EventArgs)
    '        Dim getOthers As New clsAssignEquip
    '        Dim ds As New DataSet

    '        ds = getOthers.getOtherDetails("Asset", ddlBatteryAsset.Text, ddlBatteryDesc.Text)
    '        ddlBatteryPPE.Text = ds.Tables(0).Rows(0)(1).ToString()
    '        ddlBatterySerial.Text = ds.Tables(0).Rows(0)(0).ToString()
    '    End Sub

    '    Protected Sub ddlBatterySerial_SelectedIndexChanged(sender As Object, e As EventArgs)
    '        Dim getOthers As New clsAssignEquip
    '        Dim ds As New DataSet

    '        ds = getOthers.getOtherDetails("Serial", ddlBatterySerial.Text, ddlBatteryDesc.Text)
    '        ddlBatteryAsset.Text = ds.Tables(0).Rows(0)(1).ToString()
    '        ddlBatteryPPE.Text = ds.Tables(0).Rows(0)(0).ToString()
    '    End Sub

    '    Protected Sub ddlACPPE_SelectedIndexChanged(sender As Object, e As EventArgs)
    '        Dim getOthers As New clsAssignEquip
    '        Dim ds As New DataSet

    '        ds = getOthers.getOtherDetails("PPE", ddlACPPE.Text, ddlACDesc.Text)
    '        ddlACAsset.Text = ds.Tables(0).Rows(0)(1).ToString()
    '        ddlACSerial.Text = ds.Tables(0).Rows(0)(0).ToString()
    '    End Sub

    '    Protected Sub ddlACAsset_SelectedIndexChanged(sender As Object, e As EventArgs)
    '        Dim getOthers As New clsAssignEquip
    '        Dim ds As New DataSet

    '        ds = getOthers.getOtherDetails("Asset", ddlACAsset.Text, ddlACDesc.Text)
    '        ddlACPPE.Text = ds.Tables(0).Rows(0)(1).ToString()
    '        ddlACSerial.Text = ds.Tables(0).Rows(0)(0).ToString()
    '    End Sub

    '    Protected Sub ddlACSerial_SelectedIndexChanged(sender As Object, e As EventArgs)
    '        Dim getOthers As New clsAssignEquip
    '        Dim ds As New DataSet

    '        ds = getOthers.getOtherDetails("Serial", ddlACSerial.Text, ddlACDesc.Text)
    '        ddlACAsset.Text = ds.Tables(0).Rows(0)(1).ToString()
    '        ddlACPPE.Text = ds.Tables(0).Rows(0)(0).ToString()
    '    End Sub

    '    Protected Sub ddlRAMlPPE_SelectedIndexChanged(sender As Object, e As EventArgs)
    '        Dim getOthers As New clsAssignEquip
    '        Dim ds As New DataSet

    '        ds = getOthers.getOtherDetails("PPE", ddlRAMlppe.Text, ddlRAMldesc.Text)
    '        ddlRAMlasset.Text = ds.Tables(0).Rows(0)(1).ToString()
    '        ddlRAMlserial.Text = ds.Tables(0).Rows(0)(0).ToString()
    '    End Sub

    '    Protected Sub ddlRAMlAsset_SelectedIndexChanged(sender As Object, e As EventArgs)
    '        Dim getOthers As New clsAssignEquip
    '        Dim ds As New DataSet

    '        ds = getOthers.getOtherDetails("Asset", ddlRAMlasset.Text, ddlRAMldesc.Text)
    '        ddlRAMlppe.Text = ds.Tables(0).Rows(0)(1).ToString()
    '        ddlRAMlserial.Text = ds.Tables(0).Rows(0)(0).ToString()
    '    End Sub

    '    Protected Sub ddlRAMlSerial_SelectedIndexChanged(sender As Object, e As EventArgs)
    '        Dim getOthers As New clsAssignEquip
    '        Dim ds As New DataSet

    '        ds = getOthers.getOtherDetails("Serial", ddlRAMlserial.Text, ddlRAMldesc.Text)
    '        ddlRAMlasset.Text = ds.Tables(0).Rows(0)(1).ToString()
    '        ddlRAMlppe.Text = ds.Tables(0).Rows(0)(0).ToString()
    '    End Sub

    '    Protected Sub ddlCPUDesc_SelectedIndexChanged(sender As Object, e As EventArgs)
    '        ddlCPUPPE.DataSource = getPPEList("CPU", ddlCPUDesc.Text)
    '        ddlCPUPPE.DataTextField = "Equipment_PPE_Number"
    '        ddlCPUPPE.DataBind()
    '        ddlCPUPPE.Items.Insert(0, "")
    '        ddlCPUPPE.Items(0).Value = 0
    '        ddlCPUAsset.DataSource = getAssetList("CPU", ddlCPUDesc.Text)
    '        ddlCPUAsset.DataTextField = "Equipment_Asset_Number"
    '        ddlCPUAsset.DataBind()
    '        ddlCPUAsset.Items.Insert(0, "")
    '        ddlCPUAsset.Items(0).Value = 0
    '        ddlCPUSerial.DataSource = getSerialList("CPU", ddlCPUDesc.Text)
    '        ddlCPUSerial.DataTextField = "Equipment_Serial"
    '        ddlCPUSerial.DataBind()
    '        ddlCPUSerial.Items.Insert(0, "")
    '        ddlCPUSerial.Items(0).Value = 0
    '    End Sub

    '    Protected Sub ddlCasingDesc_SelectedIndexChanged(sender As Object, e As EventArgs)
    '        ddlCasingPPE.DataSource = getPPEList("Casing", ddlCasingDesc.Text)
    '        ddlCasingPPE.DataTextField = "Equipment_PPE_Number"
    '        ddlCasingPPE.DataBind()
    '        ddlCasingPPE.Items.Insert(0, "")
    '        ddlCasingPPE.Items(0).Value = 0
    '        ddlCasingAsset.DataSource = getAssetList("Casing", ddlCasingDesc.Text)
    '        ddlCasingAsset.DataTextField = "Equipment_Asset_Number"
    '        ddlCasingAsset.DataBind()
    '        ddlCasingAsset.Items.Insert(0, "")
    '        ddlCasingAsset.Items(0).Value = 0
    '        ddlCasingSerial.DataSource = getSerialList("Casing", ddlCasingDesc.Text)
    '        ddlCasingSerial.DataTextField = "Equipment_Serial"
    '        ddlCasingSerial.DataBind()
    '        ddlCasingSerial.Items.Insert(0, "")
    '        ddlCasingSerial.Items(0).Value = 0
    '    End Sub

    '    Protected Sub ddlHDDDesc_SelectedIndexChanged(sender As Object, e As EventArgs)
    '        ddlHDDPPE.DataSource = getPPEList("HDD", ddlHDDDesc.Text)
    '        ddlHDDPPE.DataTextField = "Equipment_PPE_Number"
    '        ddlHDDPPE.DataBind()
    '        ddlHDDPPE.Items.Insert(0, "")
    '        ddlHDDPPE.Items(0).Value = 0
    '        ddlHDDAsset.DataSource = getAssetList("HDD", ddlHDDDesc.Text)
    '        ddlHDDAsset.DataTextField = "Equipment_Asset_Number"
    '        ddlHDDAsset.DataBind()
    '        ddlHDDAsset.Items.Insert(0, "")
    '        ddlHDDAsset.Items(0).Value = 0
    '        ddlHDDSerial.DataSource = getSerialList("HDD", ddlHDDDesc.Text)
    '        ddlHDDSerial.DataTextField = "Equipment_Serial"
    '        ddlHDDSerial.DataBind()
    '        ddlHDDSerial.Items.Insert(0, "")
    '        ddlHDDSerial.Items(0).Value = 0
    '    End Sub

    '    Protected Sub ddlMOBODesc_SelectedIndexChanged(sender As Object, e As EventArgs)
    '        ddlMOBOPPE.DataSource = getPPEList("MOBO", ddlMOBODesc.Text)
    '        ddlMOBOPPE.DataTextField = "Equipment_PPE_Number"
    '        ddlMOBOPPE.DataBind()
    '        ddlMOBOPPE.Items.Insert(0, "")
    '        ddlMOBOPPE.Items(0).Value = 0
    '        ddlMOBOAsset.DataSource = getAssetList("MOBO", ddlMOBODesc.Text)
    '        ddlMOBOAsset.DataTextField = "Equipment_Asset_Number"
    '        ddlMOBOAsset.DataBind()
    '        ddlMOBOAsset.Items.Insert(0, "")
    '        ddlMOBOAsset.Items(0).Value = 0
    '        ddlMOBOSerial.DataSource = getSerialList("MOBO", ddlMOBODesc.Text)
    '        ddlMOBOSerial.DataTextField = "Equipment_Serial"
    '        ddlMOBOSerial.DataBind()
    '        ddlMOBOSerial.Items.Insert(0, "")
    '        ddlMOBOSerial.Items(0).Value = 0
    '    End Sub

    '    Protected Sub ddlRAMDesc_SelectedIndexChanged(sender As Object, e As EventArgs)
    '        ddlRAMPPE.DataSource = getPPEList("RAM", ddlRAMDesc.Text)
    '        ddlRAMPPE.DataTextField = "Equipment_PPE_Number"
    '        ddlRAMPPE.DataBind()
    '        ddlRAMPPE.Items.Insert(0, "")
    '        ddlRAMPPE.Items(0).Value = 0
    '        ddlRAMAsset.DataSource = getAssetList("RAM", ddlRAMDesc.Text)
    '        ddlRAMAsset.DataTextField = "Equipment_Asset_Number"
    '        ddlRAMAsset.DataBind()
    '        ddlRAMAsset.Items.Insert(0, "")
    '        ddlRAMAsset.Items(0).Value = 0
    '        ddlRAMSerial.DataSource = getSerialList("RAM", ddlRAMDesc.Text)
    '        ddlRAMSerial.DataTextField = "Equipment_Serial"
    '        ddlRAMSerial.DataBind()
    '        ddlRAMSerial.Items.Insert(0, "")
    '        ddlRAMSerial.Items(0).Value = 0
    '    End Sub

    '    Protected Sub ddlMonitorDesc_SelectedIndexChanged(sender As Object, e As EventArgs)
    '        ddlMonitorPPE.DataSource = getPPEList("Monitor", ddlMonitorDesc.Text)
    '        ddlMonitorPPE.DataTextField = "Equipment_PPE_Number"
    '        ddlMonitorPPE.DataBind()
    '        ddlMonitorPPE.Items.Insert(0, "")
    '        ddlMonitorPPE.Items(0).Value = 0
    '        ddlMonitorAsset.DataSource = getAssetList("Monitor", ddlMonitorDesc.Text)
    '        ddlMonitorAsset.DataTextField = "Equipment_Asset_Number"
    '        ddlMonitorAsset.DataBind()
    '        ddlMonitorAsset.Items.Insert(0, "")
    '        ddlMonitorAsset.Items(0).Value = 0
    '        ddlMonitorSerial.DataSource = getSerialList("Monitor", ddlMonitorDesc.Text)
    '        ddlMonitorSerial.DataTextField = "Equipment_Serial"
    '        ddlMonitorSerial.DataBind()
    '        ddlMonitorSerial.Items.Insert(0, "")
    '        ddlMonitorSerial.Items(0).Value = 0
    '    End Sub

    '    Protected Sub ddlKeyboardDesc_SelectedIndexChanged(sender As Object, e As EventArgs)
    '        ddlKeyboardPPE.DataSource = getPPEList("Keyboard", ddlKeyboardDesc.Text)
    '        ddlKeyboardPPE.DataTextField = "Equipment_PPE_Number"
    '        ddlKeyboardPPE.DataBind()
    '        ddlKeyboardPPE.Items.Insert(0, "")
    '        ddlKeyboardPPE.Items(0).Value = 0
    '        ddlKeyboardAsset.DataSource = getAssetList("Keyboard", ddlKeyboardDesc.Text)
    '        ddlKeyboardAsset.DataTextField = "Equipment_Asset_Number"
    '        ddlKeyboardAsset.DataBind()
    '        ddlKeyboardAsset.Items.Insert(0, "")
    '        ddlKeyboardAsset.Items(0).Value = 0
    '        ddlKeyboardSerial.DataSource = getSerialList("Keyboard", ddlKeyboardDesc.Text)
    '        ddlKeyboardSerial.DataTextField = "Equipment_Serial"
    '        ddlKeyboardSerial.DataBind()
    '        ddlKeyboardSerial.Items.Insert(0, "")
    '        ddlKeyboardSerial.Items(0).Value = 0
    '    End Sub

    '    Protected Sub ddlMouseDesc_SelectedIndexChanged(sender As Object, e As EventArgs)
    '        ddlMousePPE.DataSource = getPPEList("Mouse", ddlMouseDesc.Text)
    '        ddlMousePPE.DataTextField = "Equipment_PPE_Number"
    '        ddlMousePPE.DataBind()
    '        ddlMousePPE.Items.Insert(0, "")
    '        ddlMousePPE.Items(0).Value = 0
    '        ddlMouseAsset.DataSource = getAssetList("Mouse", ddlMouseDesc.Text)
    '        ddlMouseAsset.DataTextField = "Equipment_Asset_Number"
    '        ddlMouseAsset.DataBind()
    '        ddlMouseAsset.Items.Insert(0, "")
    '        ddlMouseAsset.Items(0).Value = 0
    '        ddlMouseSerial.DataSource = getSerialList("Mouse", ddlMouseDesc.Text)
    '        ddlMouseSerial.DataTextField = "Equipment_Serial"
    '        ddlMouseSerial.DataBind()
    '        ddlMouseSerial.Items.Insert(0, "")
    '        ddlMouseSerial.Items(0).Value = 0
    '    End Sub

    '    Protected Sub ddlPSUDesc_SelectedIndexChanged(sender As Object, e As EventArgs)
    '        ddlPSUPPE.DataSource = getPPEList("PSU", ddlPSUDesc.Text)
    '        ddlPSUPPE.DataTextField = "Equipment_PPE_Number"
    '        ddlPSUPPE.DataBind()
    '        ddlPSUPPE.Items.Insert(0, "")
    '        ddlPSUPPE.Items(0).Value = 0
    '        ddlPSUAsset.DataSource = getAssetList("PSU", ddlPSUDesc.Text)
    '        ddlPSUAsset.DataTextField = "Equipment_Asset_Number"
    '        ddlPSUAsset.DataBind()
    '        ddlPSUAsset.Items.Insert(0, "")
    '        ddlPSUAsset.Items(0).Value = 0
    '        ddlPSUSerial.DataSource = getSerialList("PSU", ddlPSUDesc.Text)
    '        ddlPSUSerial.DataTextField = "Equipment_Serial"
    '        ddlPSUSerial.DataBind()
    '        ddlPSUSerial.Items.Insert(0, "")
    '        ddlPSUSerial.Items(0).Value = 0
    '    End Sub

    '    Protected Sub ddlIPPhoneDesc_SelectedIndexChanged(sender As Object, e As EventArgs)
    '        ddlIPPhonePPE.DataSource = getPPEList("IP Phone", ddlIPPhoneDesc.Text)
    '        ddlIPPhonePPE.DataTextField = "Equipment_PPE_Number"
    '        ddlIPPhonePPE.DataBind()
    '        ddlIPPhonePPE.Items.Insert(0, "")
    '        ddlIPPhonePPE.Items(0).Value = 0
    '        ddlIPPhoneAsset.DataSource = getAssetList("IP Phone", ddlIPPhoneDesc.Text)
    '        ddlIPPhoneAsset.DataTextField = "Equipment_Asset_Number"
    '        ddlIPPhoneAsset.DataBind()
    '        ddlIPPhoneAsset.Items.Insert(0, "")
    '        ddlIPPhoneAsset.Items(0).Value = 0
    '        ddlIPPhoneSerial.DataSource = getSerialList("IP Phone", ddlIPPhoneDesc.Text)
    '        ddlIPPhoneSerial.DataTextField = "Equipment_Serial"
    '        ddlIPPhoneSerial.DataBind()
    '        ddlIPPhoneSerial.Items.Insert(0, "")
    '        ddlIPPhoneSerial.Items(0).Value = 0
    '    End Sub

    '    Protected Sub ddlLaptopDesc_SelectedIndexChanged(sender As Object, e As EventArgs)
    '        ddlLaptopPPE.DataSource = getPPEList("Laptop", ddlLaptopDesc.Text)
    '        ddlLaptopPPE.DataTextField = "Equipment_PPE_Number"
    '        ddlLaptopPPE.DataBind()
    '        ddlCasingPPE.Items.Insert(0, "")
    '        ddlCasingPPE.Items(0).Value = 0
    '        ddlLaptopAsset.DataSource = getAssetList("Laptop", ddlLaptopDesc.Text)
    '        ddlLaptopAsset.DataTextField = "Equipment_Asset_Number"
    '        ddlLaptopAsset.DataBind()
    '        ddlLaptopAsset.Items.Insert(0, "")
    '        ddlLaptopAsset.Items(0).Value = 0
    '        ddlLaptopSerial.DataSource = getSerialList("Laptop", ddlLaptopDesc.Text)
    '        ddlLaptopSerial.DataTextField = "Equipment_Serial"
    '        ddlLaptopSerial.DataBind()
    '        ddlLaptopSerial.Items.Insert(0, "")
    '        ddlLaptopSerial.Items(0).Value = 0
    '    End Sub

    '    Protected Sub ddlBatteryDesc_SelectedIndexChanged(sender As Object, e As EventArgs)
    '        ddlBatteryPPE.DataSource = getPPEList("Battery", ddlBatteryDesc.Text)
    '        ddlBatteryPPE.DataTextField = "Equipment_PPE_Number"
    '        ddlBatteryPPE.DataBind()
    '        ddlBatteryPPE.Items.Insert(0, "")
    '        ddlBatteryPPE.Items(0).Value = 0
    '        ddlBatteryAsset.DataSource = getAssetList("Battery", ddlBatteryDesc.Text)
    '        ddlBatteryAsset.DataTextField = "Equipment_Asset_Number"
    '        ddlBatteryAsset.DataBind()
    '        ddlBatteryAsset.Items.Insert(0, "")
    '        ddlBatteryAsset.Items(0).Value = 0
    '        ddlBatterySerial.DataSource = getSerialList("Battery", ddlBatteryDesc.Text)
    '        ddlBatterySerial.DataTextField = "Equipment_Serial"
    '        ddlBatterySerial.DataBind()
    '        ddlBatterySerial.Items.Insert(0, "")
    '        ddlBatterySerial.Items(0).Value = 0
    '    End Sub

    '    Protected Sub ddlACDesc_SelectedIndexChanged(sender As Object, e As EventArgs)
    '        ddlACPPE.DataSource = getPPEList("AC Adaptor", ddlACDesc.Text)
    '        ddlACPPE.DataTextField = "Equipment_PPE_Number"
    '        ddlACPPE.DataBind()
    '        ddlACPPE.Items.Insert(0, "")
    '        ddlACPPE.Items(0).Value = 0
    '        ddlACAsset.DataSource = getAssetList("AC Adaptor", ddlACDesc.Text)
    '        ddlACAsset.DataTextField = "Equipment_Asset_Number"
    '        ddlACAsset.DataBind()
    '        ddlACAsset.Items.Insert(0, "")
    '        ddlACAsset.Items(0).Value = 0
    '        ddlACSerial.DataSource = getSerialList("AC Adaptor", ddlACDesc.Text)
    '        ddlACSerial.DataTextField = "Equipment_Serial"
    '        ddlACSerial.DataBind()
    '        ddlACSerial.Items.Insert(0, "")
    '        ddlACSerial.Items(0).Value = 0
    '    End Sub

    '    Protected Sub ddlRAMldesc_SelectedIndexChanged(sender As Object, e As EventArgs)
    '        ddlRAMlppe.DataSource = getPPEList("RAM", ddlRAMldesc.Text)
    '        ddlRAMlppe.DataTextField = "Equipment_PPE_Number"
    '        ddlRAMlppe.DataBind()
    '        ddlRAMlppe.Items.Insert(0, "")
    '        ddlRAMlppe.Items(0).Value = 0
    '        ddlRAMlasset.DataSource = getAssetList("RAM", ddlRAMldesc.Text)
    '        ddlRAMlasset.DataTextField = "Equipment_Asset_Number"
    '        ddlRAMlasset.DataBind()
    '        ddlRAMlasset.Items.Insert(0, "")
    '        ddlRAMlasset.Items(0).Value = 0
    '        ddlRAMlserial.DataSource = getSerialList("RAM", ddlRAMldesc.Text)
    '        ddlRAMlserial.DataTextField = "Equipment_Serial"
    '        ddlRAMlserial.DataBind()
    '        ddlRAMlserial.Items.Insert(0, "")
    '        ddlRAMlserial.Items(0).Value = 0
    '    End Sub
    '#End Region

#Region "Fields"
    Protected Sub ddlCasingPPE_SelectedIndexChanged(sender As Object, e As EventArgs)
        Dim getOthers As New Assign_Equip
        Dim ds As New DataSet

        Try
            ds = getOthers.getOtherDetails("PPE", ddlCasingPPE.Text, ddlCasingDesc.Text, "")
            ddlCasingAsset.Text = ds.Tables(0).Rows(0)(1).ToString()
            ddlCasingSerial.Text = ds.Tables(0).Rows(0)(0).ToString()
        Catch
            ddlCasingAsset.SelectedIndex = 0
            ddlCasingSerial.SelectedIndex = 0
        End Try
    End Sub

    Protected Sub ddlCasingAsset_SelectedIndexChanged(sender As Object, e As EventArgs)
        Dim getOthers As New Assign_Equip
        Dim ds As New DataSet

        Try
            ds = getOthers.getOtherDetails("Asset", ddlCasingAsset.Text, ddlCasingDesc.Text, "")
            ddlCasingPPE.Text = ds.Tables(0).Rows(0)(1).ToString()
            ddlCasingSerial.Text = ds.Tables(0).Rows(0)(0).ToString()
        Catch
            ddlCasingPPE.SelectedIndex = 0
            ddlCasingSerial.SelectedIndex = 0
        End Try
    End Sub

    Protected Sub ddlCasingSerial_SelectedIndexChanged(sender As Object, e As EventArgs)
        Dim getOthers As New Assign_Equip
        Dim ds As New DataSet

        Try
            ds = getOthers.getOtherDetails("Serial", ddlCasingSerial.Text, ddlCasingDesc.Text, "")
            ddlCasingAsset.Text = ds.Tables(0).Rows(0)(1).ToString()
            ddlCasingPPE.Text = ds.Tables(0).Rows(0)(0).ToString()
        Catch
            ddlCasingPPE.SelectedIndex = 0
            ddlCasingAsset.SelectedIndex = 0
        End Try
    End Sub

    Protected Sub ddlCPUPPE_SelectedIndexChanged(sender As Object, e As EventArgs)
        Dim getOthers As New Assign_Equip
        Dim ds As New DataSet

        Try
            ds = getOthers.getOtherDetails("PPE", ddlCPUPPE.Text, ddlCPUDesc.Text, "")
            ddlCPUAsset.Text = ds.Tables(0).Rows(0)(1).ToString()
            ddlCPUSerial.Text = ds.Tables(0).Rows(0)(0).ToString()
        Catch
            ddlCPUAsset.SelectedIndex = 0
            ddlCPUSerial.SelectedIndex = 0
        End Try
    End Sub

    Protected Sub ddlCPUAsset_SelectedIndexChanged(sender As Object, e As EventArgs)
        Dim getOthers As New Assign_Equip
        Dim ds As New DataSet

        Try
            ds = getOthers.getOtherDetails("Asset", ddlCPUAsset.Text, ddlCPUDesc.Text, "")
            ddlCPUPPE.Text = ds.Tables(0).Rows(0)(1).ToString()
            ddlCPUSerial.Text = ds.Tables(0).Rows(0)(0).ToString()
        Catch
            ddlCPUPPE.SelectedIndex = 0
            ddlCPUSerial.SelectedIndex = 0
        End Try
    End Sub

    Protected Sub ddlCPUSerial_SelectedIndexChanged(sender As Object, e As EventArgs)
        Dim getOthers As New Assign_Equip
        Dim ds As New DataSet

        Try
            ds = getOthers.getOtherDetails("Serial", ddlCPUSerial.Text, ddlCPUDesc.Text, "")
            ddlCPUAsset.Text = ds.Tables(0).Rows(0)(1).ToString()
            ddlCPUPPE.Text = ds.Tables(0).Rows(0)(0).ToString()
        Catch
            ddlCPUPPE.SelectedIndex = 0
            ddlCPUAsset.SelectedIndex = 0
        End Try
    End Sub

    Protected Sub ddlHDDPPE_SelectedIndexChanged(sender As Object, e As EventArgs)
        Dim getOthers As New Assign_Equip
        Dim ds As New DataSet

        Try
            ds = getOthers.getOtherDetails("PPE", ddlHDDPPE.Text, ddlHDDDesc.Text, "")
            ddlHDDAsset.Text = ds.Tables(0).Rows(0)(1).ToString()
            ddlHDDSerial.Text = ds.Tables(0).Rows(0)(0).ToString()
        Catch
            ddlHDDAsset.SelectedIndex = 0
            ddlHDDSerial.SelectedIndex = 0
        End Try
    End Sub

    Protected Sub ddlHDDAsset_SelectedIndexChanged(sender As Object, e As EventArgs)
        Dim getOthers As New Assign_Equip
        Dim ds As New DataSet

        Try
            ds = getOthers.getOtherDetails("Asset", ddlHDDAsset.Text, ddlHDDDesc.Text, "")
            ddlHDDPPE.Text = ds.Tables(0).Rows(0)(1).ToString()
            ddlHDDSerial.Text = ds.Tables(0).Rows(0)(0).ToString()
        Catch
            ddlHDDPPE.SelectedIndex = 0
            ddlHDDSerial.SelectedIndex = 0
        End Try
    End Sub

    Protected Sub ddlHDDSerial_SelectedIndexChanged(sender As Object, e As EventArgs)
        Dim getOthers As New Assign_Equip
        Dim ds As New DataSet

        Try
            ds = getOthers.getOtherDetails("Serial", ddlHDDSerial.Text, ddlHDDDesc.Text, "")
            ddlHDDAsset.Text = ds.Tables(0).Rows(0)(1).ToString()
            ddlHDDPPE.Text = ds.Tables(0).Rows(0)(0).ToString()
        Catch
            ddlHDDPPE.SelectedIndex = 0
            ddlHDDAsset.SelectedIndex = 0
        End Try
    End Sub

    Protected Sub ddlMOBOPPE_SelectedIndexChanged(sender As Object, e As EventArgs)
        Dim getOthers As New Assign_Equip
        Dim ds As New DataSet

        Try
            ds = getOthers.getOtherDetails("PPE", ddlMOBOPPE.Text, ddlMOBODesc.Text, "")
            ddlMOBOAsset.Text = ds.Tables(0).Rows(0)(1).ToString()
            ddlMOBOSerial.Text = ds.Tables(0).Rows(0)(0).ToString()
        Catch
            ddlMOBOAsset.SelectedIndex = 0
            ddlMOBOSerial.SelectedIndex = 0
        End Try
    End Sub

    Protected Sub ddlMOBOAsset_SelectedIndexChanged(sender As Object, e As EventArgs)
        Dim getOthers As New Assign_Equip
        Dim ds As New DataSet

        Try
            ds = getOthers.getOtherDetails("Asset", ddlMOBOAsset.Text, ddlMOBODesc.Text, "")
            ddlMOBOPPE.Text = ds.Tables(0).Rows(0)(1).ToString()
            ddlMOBOSerial.Text = ds.Tables(0).Rows(0)(0).ToString()
        Catch
            ddlMOBOPPE.SelectedIndex = 0
            ddlMOBOSerial.SelectedIndex = 0
        End Try
    End Sub

    Protected Sub ddlMOBOSerial_SelectedIndexChanged(sender As Object, e As EventArgs)
        Dim getOthers As New Assign_Equip
        Dim ds As New DataSet

        Try
            ds = getOthers.getOtherDetails("Serial", ddlMOBOSerial.Text, ddlMOBODesc.Text, "")
            ddlMOBOAsset.Text = ds.Tables(0).Rows(0)(1).ToString()
            ddlMOBOPPE.Text = ds.Tables(0).Rows(0)(0).ToString()
        Catch
            ddlMOBOPPE.SelectedIndex = 0
            ddlMOBOAsset.SelectedIndex = 0
        End Try
    End Sub

    Protected Sub ddlRAMPPE_SelectedIndexChanged(sender As Object, e As EventArgs)
        Dim getOthers As New Assign_Equip
        Dim ds As New DataSet

        Try
            ds = getOthers.getOtherDetails("PPE", ddlRAMPPE.Text, ddlRAMDesc.Text, "")
            ddlRAMAsset.Text = ds.Tables(0).Rows(0)(1).ToString()
            ddlRAMSerial.Text = ds.Tables(0).Rows(0)(0).ToString()
        Catch
            ddlRAMAsset.SelectedIndex = 0
            ddlRAMSerial.SelectedIndex = 0
        End Try
    End Sub

    Protected Sub ddlRAMAsset_SelectedIndexChanged(sender As Object, e As EventArgs)
        Dim getOthers As New Assign_Equip
        Dim ds As New DataSet

        Try
            ds = getOthers.getOtherDetails("Asset", ddlRAMAsset.Text, ddlRAMDesc.Text, "")
            ddlRAMPPE.Text = ds.Tables(0).Rows(0)(1).ToString()
            ddlRAMSerial.Text = ds.Tables(0).Rows(0)(0).ToString()
        Catch
            ddlRAMPPE.SelectedIndex = 0
            ddlRAMSerial.SelectedIndex = 0
        End Try
    End Sub

    Protected Sub ddlRAMSerial_SelectedIndexChanged(sender As Object, e As EventArgs)
        Dim getOthers As New Assign_Equip
        Dim ds As New DataSet

        Try
            ds = getOthers.getOtherDetails("Serial", ddlRAMSerial.Text, ddlRAMDesc.Text, "")
            ddlRAMAsset.Text = ds.Tables(0).Rows(0)(1).ToString()
            ddlRAMPPE.Text = ds.Tables(0).Rows(0)(0).ToString()
        Catch
            ddlRAMPPE.SelectedIndex = 0
            ddlRAMAsset.SelectedIndex = 0
        End Try
    End Sub

    Protected Sub ddlMonitorPPE_SelectedIndexChanged(sender As Object, e As EventArgs)
        Dim getOthers As New Assign_Equip
        Dim ds As New DataSet

        Try
            ds = getOthers.getOtherDetails("PPE", ddlMonitorPPE.Text, ddlMonitorDesc.Text, "")
            ddlMonitorAsset.Text = ds.Tables(0).Rows(0)(1).ToString()
            ddlMonitorSerial.Text = ds.Tables(0).Rows(0)(0).ToString()
        Catch
            ddlMonitorAsset.SelectedIndex = 0
            ddlMonitorSerial.SelectedIndex = 0
        End Try
    End Sub

    Protected Sub ddlMonitorAsset_SelectedIndexChanged(sender As Object, e As EventArgs)
        Dim getOthers As New Assign_Equip
        Dim ds As New DataSet

        Try
            ds = getOthers.getOtherDetails("Asset", ddlMonitorAsset.Text, ddlMonitorDesc.Text, "")
            ddlMonitorPPE.Text = ds.Tables(0).Rows(0)(1).ToString()
            ddlMonitorSerial.Text = ds.Tables(0).Rows(0)(0).ToString()
        Catch
            ddlMonitorPPE.SelectedIndex = 0
            ddlMonitorSerial.SelectedIndex = 0
        End Try
    End Sub

    Protected Sub ddlMonitorSerial_SelectedIndexChanged(sender As Object, e As EventArgs)
        Dim getOthers As New Assign_Equip
        Dim ds As New DataSet

        Try
            ds = getOthers.getOtherDetails("Serial", ddlMonitorSerial.Text, ddlMonitorDesc.Text, "")
            ddlMonitorAsset.Text = ds.Tables(0).Rows(0)(1).ToString()
            ddlMonitorPPE.Text = ds.Tables(0).Rows(0)(0).ToString()
        Catch
            ddlMonitorPPE.SelectedIndex = 0
            ddlMonitorAsset.SelectedIndex = 0
        End Try
    End Sub

    Protected Sub ddlKeyboardPPE_SelectedIndexChanged(sender As Object, e As EventArgs)
        Dim getOthers As New Assign_Equip
        Dim ds As New DataSet

        Try
            ds = getOthers.getOtherDetails("PPE", ddlKeyboardPPE.Text, ddlKeyboardDesc.Text, "")
            ddlKeyboardAsset.Text = ds.Tables(0).Rows(0)(1).ToString()
            ddlKeyboardSerial.Text = ds.Tables(0).Rows(0)(0).ToString()
        Catch
            ddlKeyboardAsset.SelectedIndex = 0
            ddlKeyboardSerial.SelectedIndex = 0
        End Try
    End Sub

    Protected Sub ddlKeyboardAsset_SelectedIndexChanged(sender As Object, e As EventArgs)
        Dim getOthers As New Assign_Equip
        Dim ds As New DataSet

        Try
            ds = getOthers.getOtherDetails("Asset", ddlKeyboardAsset.Text, ddlKeyboardDesc.Text, "")
            ddlKeyboardPPE.Text = ds.Tables(0).Rows(0)(1).ToString()
            ddlKeyboardSerial.Text = ds.Tables(0).Rows(0)(0).ToString()
        Catch
            ddlKeyboardPPE.SelectedIndex = 0
            ddlKeyboardSerial.SelectedIndex = 0
        End Try
    End Sub

    Protected Sub ddlKeyboardSerial_SelectedIndexChanged(sender As Object, e As EventArgs)
        Dim getOthers As New Assign_Equip
        Dim ds As New DataSet

        Try
            ds = getOthers.getOtherDetails("Serial", ddlKeyboardSerial.Text, ddlKeyboardDesc.Text, "")
            ddlKeyboardAsset.Text = ds.Tables(0).Rows(0)(1).ToString()
            ddlKeyboardPPE.Text = ds.Tables(0).Rows(0)(0).ToString()
        Catch
            ddlKeyboardPPE.SelectedIndex = 0
            ddlKeyboardAsset.SelectedIndex = 0
        End Try
    End Sub

    Protected Sub ddlMousePPE_SelectedIndexChanged(sender As Object, e As EventArgs)
        Dim getOthers As New Assign_Equip
        Dim ds As New DataSet

        Try
            ds = getOthers.getOtherDetails("PPE", ddlMousePPE.Text, ddlMouseDesc.Text, "")
            ddlMouseAsset.Text = ds.Tables(0).Rows(0)(1).ToString()
            ddlMouseSerial.Text = ds.Tables(0).Rows(0)(0).ToString()
        Catch
            ddlMouseAsset.SelectedIndex = 0
            ddlMouseSerial.SelectedIndex = 0
        End Try
    End Sub

    Protected Sub ddlMouseAsset_SelectedIndexChanged(sender As Object, e As EventArgs)
        Dim getOthers As New Assign_Equip
        Dim ds As New DataSet

        Try
            ds = getOthers.getOtherDetails("Asset", ddlMouseAsset.Text, ddlMouseDesc.Text, "")
            ddlMousePPE.Text = ds.Tables(0).Rows(0)(1).ToString()
            ddlMouseSerial.Text = ds.Tables(0).Rows(0)(0).ToString()
        Catch
            ddlMousePPE.SelectedIndex = 0
            ddlMouseSerial.SelectedIndex = 0
        End Try
    End Sub

    Protected Sub ddlMouseSerial_SelectedIndexChanged(sender As Object, e As EventArgs)
        Dim getOthers As New Assign_Equip
        Dim ds As New DataSet

        Try
            ds = getOthers.getOtherDetails("Serial", ddlMouseSerial.Text, ddlMouseDesc.Text, "")
            ddlMouseAsset.Text = ds.Tables(0).Rows(0)(1).ToString()
            ddlMousePPE.Text = ds.Tables(0).Rows(0)(0).ToString()
        Catch
            ddlMousePPE.SelectedIndex = 0
            ddlMouseAsset.SelectedIndex = 0
        End Try
    End Sub

    Protected Sub ddlPSUPPE_SelectedIndexChanged(sender As Object, e As EventArgs)
        Dim getOthers As New Assign_Equip
        Dim ds As New DataSet

        Try
            ds = getOthers.getOtherDetails("PPE", ddlPSUPPE.Text, ddlPSUDesc.Text, "")
            ddlPSUAsset.Text = ds.Tables(0).Rows(0)(1).ToString()
            ddlPSUSerial.Text = ds.Tables(0).Rows(0)(0).ToString()
        Catch
            ddlPSUAsset.SelectedIndex = 0
            ddlPSUSerial.SelectedIndex = 0
        End Try
    End Sub

    Protected Sub ddlPSUAsset_SelectedIndexChanged(sender As Object, e As EventArgs)
        Dim getOthers As New Assign_Equip
        Dim ds As New DataSet

        Try
            ds = getOthers.getOtherDetails("Asset", ddlPSUAsset.Text, ddlPSUDesc.Text, "")
            ddlPSUPPE.Text = ds.Tables(0).Rows(0)(1).ToString()
            ddlPSUSerial.Text = ds.Tables(0).Rows(0)(0).ToString()
        Catch
            ddlPSUPPE.SelectedIndex = 0
            ddlPSUSerial.SelectedIndex = 0
        End Try
    End Sub

    Protected Sub ddlPSUSerial_SelectedIndexChanged(sender As Object, e As EventArgs)
        Dim getOthers As New Assign_Equip
        Dim ds As New DataSet

        Try
            ds = getOthers.getOtherDetails("Serial", ddlPSUSerial.Text, ddlPSUDesc.Text, "")
            ddlPSUAsset.Text = ds.Tables(0).Rows(0)(1).ToString()
            ddlPSUPPE.Text = ds.Tables(0).Rows(0)(0).ToString()
        Catch
            ddlPSUPPE.SelectedIndex = 0
            ddlPSUAsset.SelectedIndex = 0
        End Try
    End Sub

    Protected Sub ddlIPPhonePPE_SelectedIndexChanged(sender As Object, e As EventArgs)
        Dim getOthers As New Assign_Equip
        Dim ds As New DataSet

        Try
            ds = getOthers.getOtherDetails("PPE", ddlIPPhonePPE.Text, ddlIPPhoneDesc.Text, "")
            ddlIPPhoneAsset.Text = ds.Tables(0).Rows(0)(1).ToString()
            ddlIPPhoneSerial.Text = ds.Tables(0).Rows(0)(0).ToString()
        Catch
            ddlIPPhoneAsset.SelectedIndex = 0
            ddlIPPhoneSerial.SelectedIndex = 0
        End Try
    End Sub

    Protected Sub ddlIPPhoneAsset_SelectedIndexChanged(sender As Object, e As EventArgs)
        Dim getOthers As New Assign_Equip
        Dim ds As New DataSet

        Try
            ds = getOthers.getOtherDetails("Asset", ddlIPPhoneAsset.Text, ddlIPPhoneDesc.Text, "")
            ddlIPPhonePPE.Text = ds.Tables(0).Rows(0)(1).ToString()
            ddlIPPhoneSerial.Text = ds.Tables(0).Rows(0)(0).ToString()
        Catch
            ddlIPPhonePPE.SelectedIndex = 0
            ddlIPPhoneSerial.SelectedIndex = 0
        End Try
    End Sub

    Protected Sub ddlIPPhoneSerial_SelectedIndexChanged(sender As Object, e As EventArgs)
        Dim getOthers As New Assign_Equip
        Dim ds As New DataSet


        Try
            ds = getOthers.getOtherDetails("Serial", ddlIPPhoneSerial.Text, ddlIPPhoneDesc.Text, "")
            ddlIPPhoneAsset.Text = ds.Tables(0).Rows(0)(1).ToString()
            ddlIPPhonePPE.Text = ds.Tables(0).Rows(0)(0).ToString()
        Catch
            ddlIPPhonePPE.SelectedIndex = 0
            ddlIPPhoneAsset.SelectedIndex = 0
        End Try
    End Sub

    Protected Sub ddlLaptopPPE_SelectedIndexChanged(sender As Object, e As EventArgs)
        Dim getOthers As New Assign_Equip
        Dim ds As New DataSet

        Try
            ds = getOthers.getOtherDetails("PPE", ddlLaptopPPE.Text, ddlLaptopDesc.Text, "")
            ddlLaptopAsset.Text = ds.Tables(0).Rows(0)(1).ToString()
            ddlLaptopSerial.Text = ds.Tables(0).Rows(0)(0).ToString()
        Catch
            ddlLaptopAsset.SelectedIndex = 0
            ddlLaptopSerial.SelectedIndex = 0
        End Try
    End Sub

    Protected Sub ddlLaptopAsset_SelectedIndexChanged(sender As Object, e As EventArgs)
        Dim getOthers As New Assign_Equip
        Dim ds As New DataSet

        Try
            ds = getOthers.getOtherDetails("Asset", ddlLaptopAsset.Text, ddlLaptopDesc.Text, "")
            ddlLaptopPPE.Text = ds.Tables(0).Rows(0)(1).ToString()
            ddlLaptopSerial.Text = ds.Tables(0).Rows(0)(0).ToString()
        Catch
            ddlLaptopPPE.SelectedIndex = 0
            ddlLaptopSerial.SelectedIndex = 0
        End Try
    End Sub

    Protected Sub ddlLaptopSerial_SelectedIndexChanged(sender As Object, e As EventArgs)
        Dim getOthers As New Assign_Equip
        Dim ds As New DataSet

        Try
            ds = getOthers.getOtherDetails("Serial", ddlLaptopSerial.Text, ddlLaptopDesc.Text, "")
            ddlLaptopAsset.Text = ds.Tables(0).Rows(0)(1).ToString()
            ddlLaptopPPE.Text = ds.Tables(0).Rows(0)(0).ToString()
        Catch
            ddlLaptopPPE.SelectedIndex = 0
            ddlLaptopAsset.SelectedIndex = 0
        End Try
    End Sub

    Protected Sub ddlBatteryPPE_SelectedIndexChanged(sender As Object, e As EventArgs)
        Dim getOthers As New Assign_Equip
        Dim ds As New DataSet

        Try
            ds = getOthers.getOtherDetails("PPE", ddlBatteryPPE.Text, ddlBatteryDesc.Text, "")
            ddlBatteryAsset.Text = ds.Tables(0).Rows(0)(1).ToString()
            ddlBatterySerial.Text = ds.Tables(0).Rows(0)(0).ToString()
        Catch
            ddlBatteryAsset.SelectedIndex = 0
            ddlBatterySerial.SelectedIndex = 0
        End Try
    End Sub

    Protected Sub ddlBatteryAsset_SelectedIndexChanged(sender As Object, e As EventArgs)
        Dim getOthers As New Assign_Equip
        Dim ds As New DataSet

        Try
            ds = getOthers.getOtherDetails("Asset", ddlBatteryAsset.Text, ddlBatteryDesc.Text, "")
            ddlBatteryPPE.Text = ds.Tables(0).Rows(0)(1).ToString()
            ddlBatterySerial.Text = ds.Tables(0).Rows(0)(0).ToString()
        Catch
            ddlBatteryPPE.SelectedIndex = 0
            ddlBatterySerial.SelectedIndex = 0
        End Try
    End Sub

    Protected Sub ddlBatterySerial_SelectedIndexChanged(sender As Object, e As EventArgs)
        Dim getOthers As New Assign_Equip
        Dim ds As New DataSet

        Try
            ds = getOthers.getOtherDetails("Serial", ddlBatterySerial.Text, ddlBatteryDesc.Text, "")
            ddlBatteryAsset.Text = ds.Tables(0).Rows(0)(1).ToString()
            ddlBatteryPPE.Text = ds.Tables(0).Rows(0)(0).ToString()
        Catch
            ddlBatteryPPE.SelectedIndex = 0
            ddlBatteryAsset.SelectedIndex = 0
        End Try
    End Sub

    Protected Sub ddlACPPE_SelectedIndexChanged(sender As Object, e As EventArgs)
        Dim getOthers As New Assign_Equip
        Dim ds As New DataSet

        Try
            ds = getOthers.getOtherDetails("PPE", ddlACPPE.Text, ddlACDesc.Text, "")
            ddlACAsset.Text = ds.Tables(0).Rows(0)(1).ToString()
            ddlACSerial.Text = ds.Tables(0).Rows(0)(0).ToString()
        Catch
            ddlACAsset.SelectedIndex = 0
            ddlACSerial.SelectedIndex = 0
        End Try
    End Sub

    Protected Sub ddlACAsset_SelectedIndexChanged(sender As Object, e As EventArgs)
        Dim getOthers As New Assign_Equip
        Dim ds As New DataSet

        Try
            ds = getOthers.getOtherDetails("Asset", ddlACAsset.Text, ddlACDesc.Text, "")
            ddlACPPE.Text = ds.Tables(0).Rows(0)(1).ToString()
            ddlACSerial.Text = ds.Tables(0).Rows(0)(0).ToString()
        Catch
            ddlACPPE.SelectedIndex = 0
            ddlACSerial.SelectedIndex = 0
        End Try
    End Sub

    Protected Sub ddlACSerial_SelectedIndexChanged(sender As Object, e As EventArgs)
        Dim getOthers As New Assign_Equip
        Dim ds As New DataSet

        Try
            ds = getOthers.getOtherDetails("Serial", ddlACSerial.Text, ddlACDesc.Text, "")
            ddlACAsset.Text = ds.Tables(0).Rows(0)(1).ToString()
            ddlACPPE.Text = ds.Tables(0).Rows(0)(0).ToString()
        Catch
            ddlACPPE.SelectedIndex = 0
            ddlACAsset.SelectedIndex = 0
        End Try
    End Sub

    Protected Sub ddlRAMlPPE_SelectedIndexChanged(sender As Object, e As EventArgs)
        Dim getOthers As New Assign_Equip
        Dim ds As New DataSet

        Try
            ds = getOthers.getOtherDetails("PPE", ddlRAMlppe.Text, ddlRAMldesc.Text, "")
            ddlRAMlasset.Text = ds.Tables(0).Rows(0)(1).ToString()
            ddlRAMlserial.Text = ds.Tables(0).Rows(0)(0).ToString()
        Catch
            ddlRAMlasset.SelectedIndex = 0
            ddlRAMlserial.SelectedIndex = 0
        End Try
    End Sub

    Protected Sub ddlRAMlAsset_SelectedIndexChanged(sender As Object, e As EventArgs)
        Dim getOthers As New Assign_Equip
        Dim ds As New DataSet

        Try
            ds = getOthers.getOtherDetails("Asset", ddlRAMlasset.Text, ddlRAMldesc.Text, "")
            ddlRAMlppe.Text = ds.Tables(0).Rows(0)(1).ToString()
            ddlRAMlserial.Text = ds.Tables(0).Rows(0)(0).ToString()
        Catch
            ddlRAMlppe.SelectedIndex = 0
            ddlRAMlserial.SelectedIndex = 0
        End Try
    End Sub

    Protected Sub ddlRAMlSerial_SelectedIndexChanged(sender As Object, e As EventArgs)
        Dim getOthers As New Assign_Equip
        Dim ds As New DataSet

        Try
            ds = getOthers.getOtherDetails("Serial", ddlRAMlserial.Text, ddlRAMldesc.Text, "")
            ddlRAMlasset.Text = ds.Tables(0).Rows(0)(1).ToString()
            ddlRAMlppe.Text = ds.Tables(0).Rows(0)(0).ToString()
        Catch
            ddlRAMlppe.SelectedIndex = 0
            ddlRAMlasset.SelectedIndex = 0
        End Try
    End Sub

    Protected Sub ddlCPUDesc_SelectedIndexChanged(sender As Object, e As EventArgs)
        setCpuDetails()
    End Sub

    Private Sub setCpuDetails()
        ddlCPUPPE.DataSource = getPPEList("CPU", ddlCPUDesc.Text)
        ddlCPUPPE.DataTextField = "Equipment_PPE_Number"
        ddlCPUPPE.DataBind()
        ddlCPUPPE.Items.Insert(0, "")
        ddlCPUPPE.Items(0).Value = 0
        ddlCPUAsset.DataSource = getAssetList("CPU", ddlCPUDesc.Text)
        ddlCPUAsset.DataTextField = "Equipment_Asset_Number"
        ddlCPUAsset.DataBind()
        ddlCPUAsset.Items.Insert(0, "")
        ddlCPUAsset.Items(0).Value = 0
        ddlCPUSerial.DataSource = getSerialList("CPU", ddlCPUDesc.Text)
        ddlCPUSerial.DataTextField = "Equipment_Serial"
        ddlCPUSerial.DataBind()
        ddlCPUSerial.Items.Insert(0, "")
        ddlCPUSerial.Items(0).Value = 0
    End Sub

    Protected Sub ddlCasingDesc_SelectedIndexChanged(sender As Object, e As EventArgs)
        setCasingDetails()
    End Sub

    Private Sub setCasingDetails()
        ddlCasingPPE.DataSource = getPPEList("Casing", ddlCasingDesc.Text)
        ddlCasingPPE.DataTextField = "Equipment_PPE_Number"
        ddlCasingPPE.DataBind()
        ddlCasingPPE.Items.Insert(0, "")
        ddlCasingPPE.Items(0).Value = 0
        ddlCasingAsset.DataSource = getAssetList("Casing", ddlCasingDesc.Text)
        ddlCasingAsset.DataTextField = "Equipment_Asset_Number"
        ddlCasingAsset.DataBind()
        ddlCasingAsset.Items.Insert(0, "")
        ddlCasingAsset.Items(0).Value = 0
        ddlCasingSerial.DataSource = getSerialList("Casing", ddlCasingDesc.Text)
        ddlCasingSerial.DataTextField = "Equipment_Serial"
        ddlCasingSerial.DataBind()
        ddlCasingSerial.Items.Insert(0, "")
        ddlCasingSerial.Items(0).Value = 0
    End Sub

    Protected Sub ddlHDDDesc_SelectedIndexChanged(sender As Object, e As EventArgs)
        setHddDetails()
    End Sub

    Private Sub setHddDetails()
        ddlHDDPPE.DataSource = getPPEList("HDD", ddlHDDDesc.Text)
        ddlHDDPPE.DataTextField = "Equipment_PPE_Number"
        ddlHDDPPE.DataBind()
        ddlHDDPPE.Items.Insert(0, "")
        ddlHDDPPE.Items(0).Value = 0
        ddlHDDAsset.DataSource = getAssetList("HDD", ddlHDDDesc.Text)
        ddlHDDAsset.DataTextField = "Equipment_Asset_Number"
        ddlHDDAsset.DataBind()
        ddlHDDAsset.Items.Insert(0, "")
        ddlHDDAsset.Items(0).Value = 0
        ddlHDDSerial.DataSource = getSerialList("HDD", ddlHDDDesc.Text)
        ddlHDDSerial.DataTextField = "Equipment_Serial"
        ddlHDDSerial.DataBind()
        ddlHDDSerial.Items.Insert(0, "")
        ddlHDDSerial.Items(0).Value = 0
    End Sub

    Protected Sub ddlMOBODesc_SelectedIndexChanged(sender As Object, e As EventArgs)
        setMoboDetails()
    End Sub

    Private Sub setMoboDetails()
        ddlMOBOPPE.DataSource = getPPEList("MOBO", ddlMOBODesc.Text)
        ddlMOBOPPE.DataTextField = "Equipment_PPE_Number"
        ddlMOBOPPE.DataBind()
        ddlMOBOPPE.Items.Insert(0, "")
        ddlMOBOPPE.Items(0).Value = 0
        ddlMOBOAsset.DataSource = getAssetList("MOBO", ddlMOBODesc.Text)
        ddlMOBOAsset.DataTextField = "Equipment_Asset_Number"
        ddlMOBOAsset.DataBind()
        ddlMOBOAsset.Items.Insert(0, "")
        ddlMOBOAsset.Items(0).Value = 0
        ddlMOBOSerial.DataSource = getSerialList("MOBO", ddlMOBODesc.Text)
        ddlMOBOSerial.DataTextField = "Equipment_Serial"
        ddlMOBOSerial.DataBind()
        ddlMOBOSerial.Items.Insert(0, "")
        ddlMOBOSerial.Items(0).Value = 0
    End Sub

    Protected Sub ddlRAMDesc_SelectedIndexChanged(sender As Object, e As EventArgs)
        setRamDetails()
    End Sub

    Private Sub setRamDetails()
        ddlRAMPPE.DataSource = getPPEList("Memory", ddlRAMDesc.Text)
        ddlRAMPPE.DataTextField = "Equipment_PPE_Number"
        ddlRAMPPE.DataBind()
        ddlRAMPPE.Items.Insert(0, "")
        ddlRAMPPE.Items(0).Value = 0
        ddlRAMAsset.DataSource = getAssetList("Memory", ddlRAMDesc.Text)
        ddlRAMAsset.DataTextField = "Equipment_Asset_Number"
        ddlRAMAsset.DataBind()
        ddlRAMAsset.Items.Insert(0, "")
        ddlRAMAsset.Items(0).Value = 0
        ddlRAMSerial.DataSource = getSerialList("Memory", ddlRAMDesc.Text)
        ddlRAMSerial.DataTextField = "Equipment_Serial"
        ddlRAMSerial.DataBind()
        ddlRAMSerial.Items.Insert(0, "")
        ddlRAMSerial.Items(0).Value = 0
    End Sub

    Protected Sub ddlMonitorDesc_SelectedIndexChanged(sender As Object, e As EventArgs)
        setMonitorDetails()
    End Sub

    Private Sub setMonitorDetails()
        ddlMonitorPPE.DataSource = getPPEList("Monitor", ddlMonitorDesc.Text)
        ddlMonitorPPE.DataTextField = "Equipment_PPE_Number"
        ddlMonitorPPE.DataBind()
        ddlMonitorPPE.Items.Insert(0, "")
        ddlMonitorPPE.Items(0).Value = 0
        ddlMonitorAsset.DataSource = getAssetList("Monitor", ddlMonitorDesc.Text)
        ddlMonitorAsset.DataTextField = "Equipment_Asset_Number"
        ddlMonitorAsset.DataBind()
        ddlMonitorAsset.Items.Insert(0, "")
        ddlMonitorAsset.Items(0).Value = 0
        ddlMonitorSerial.DataSource = getSerialList("Monitor", ddlMonitorDesc.Text)
        ddlMonitorSerial.DataTextField = "Equipment_Serial"
        ddlMonitorSerial.DataBind()
        ddlMonitorSerial.Items.Insert(0, "")
        ddlMonitorSerial.Items(0).Value = 0
    End Sub

    Protected Sub ddlKeyboardDesc_SelectedIndexChanged(sender As Object, e As EventArgs)
        setKeyboardDetails()
    End Sub

    Private Sub setKeyboardDetails()
        ddlKeyboardPPE.DataSource = getPPEList("Keyboard", ddlKeyboardDesc.Text)
        ddlKeyboardPPE.DataTextField = "Equipment_PPE_Number"
        ddlKeyboardPPE.DataBind()
        ddlKeyboardPPE.Items.Insert(0, "")
        ddlKeyboardPPE.Items(0).Value = 0
        ddlKeyboardAsset.DataSource = getAssetList("Keyboard", ddlKeyboardDesc.Text)
        ddlKeyboardAsset.DataTextField = "Equipment_Asset_Number"
        ddlKeyboardAsset.DataBind()
        ddlKeyboardAsset.Items.Insert(0, "")
        ddlKeyboardAsset.Items(0).Value = 0
        ddlKeyboardSerial.DataSource = getSerialList("Keyboard", ddlKeyboardDesc.Text)
        ddlKeyboardSerial.DataTextField = "Equipment_Serial"
        ddlKeyboardSerial.DataBind()
        ddlKeyboardSerial.Items.Insert(0, "")
        ddlKeyboardSerial.Items(0).Value = 0
    End Sub

    Protected Sub ddlMouseDesc_SelectedIndexChanged(sender As Object, e As EventArgs)
        setMouseDetails()
    End Sub

    Private Sub setMouseDetails()
        ddlMousePPE.DataSource = getPPEList("Mouse", ddlMouseDesc.Text)
        ddlMousePPE.DataTextField = "Equipment_PPE_Number"
        ddlMousePPE.DataBind()
        ddlMousePPE.Items.Insert(0, "")
        ddlMousePPE.Items(0).Value = 0
        ddlMouseAsset.DataSource = getAssetList("Mouse", ddlMouseDesc.Text)
        ddlMouseAsset.DataTextField = "Equipment_Asset_Number"
        ddlMouseAsset.DataBind()
        ddlMouseAsset.Items.Insert(0, "")
        ddlMouseAsset.Items(0).Value = 0
        ddlMouseSerial.DataSource = getSerialList("Mouse", ddlMouseDesc.Text)
        ddlMouseSerial.DataTextField = "Equipment_Serial"
        ddlMouseSerial.DataBind()
        ddlMouseSerial.Items.Insert(0, "")
        ddlMouseSerial.Items(0).Value = 0
    End Sub

    Protected Sub ddlPSUDesc_SelectedIndexChanged(sender As Object, e As EventArgs)
        setPsuDetails()
    End Sub

    Private Sub setPsuDetails()
        ddlPSUPPE.DataSource = getPPEList("PSU", ddlPSUDesc.Text)
        ddlPSUPPE.DataTextField = "Equipment_PPE_Number"
        ddlPSUPPE.DataBind()
        ddlPSUPPE.Items.Insert(0, "")
        ddlPSUPPE.Items(0).Value = 0
        ddlPSUAsset.DataSource = getAssetList("PSU", ddlPSUDesc.Text)
        ddlPSUAsset.DataTextField = "Equipment_Asset_Number"
        ddlPSUAsset.DataBind()
        ddlPSUAsset.Items.Insert(0, "")
        ddlPSUAsset.Items(0).Value = 0
        ddlPSUSerial.DataSource = getSerialList("PSU", ddlPSUDesc.Text)
        ddlPSUSerial.DataTextField = "Equipment_Serial"
        ddlPSUSerial.DataBind()
        ddlPSUSerial.Items.Insert(0, "")
        ddlPSUSerial.Items(0).Value = 0
    End Sub

    Protected Sub ddlIPPhoneDesc_SelectedIndexChanged(sender As Object, e As EventArgs)
        setIpPhoneDetails()
    End Sub

    Private Sub setIpPhoneDetails()
        ddlIPPhonePPE.DataSource = getPPEList("IP Phone", ddlIPPhoneDesc.Text)
        ddlIPPhonePPE.DataTextField = "Equipment_PPE_Number"
        ddlIPPhonePPE.DataBind()
        ddlIPPhonePPE.Items.Insert(0, "")
        ddlIPPhonePPE.Items(0).Value = 0
        ddlIPPhoneAsset.DataSource = getAssetList("IP Phone", ddlIPPhoneDesc.Text)
        ddlIPPhoneAsset.DataTextField = "Equipment_Asset_Number"
        ddlIPPhoneAsset.DataBind()
        ddlIPPhoneAsset.Items.Insert(0, "")
        ddlIPPhoneAsset.Items(0).Value = 0
        ddlIPPhoneSerial.DataSource = getSerialList("IP Phone", ddlIPPhoneDesc.Text)
        ddlIPPhoneSerial.DataTextField = "Equipment_Serial"
        ddlIPPhoneSerial.DataBind()
        ddlIPPhoneSerial.Items.Insert(0, "")
        ddlIPPhoneSerial.Items(0).Value = 0
    End Sub

    Protected Sub ddlLaptopDesc_SelectedIndexChanged(sender As Object, e As EventArgs)
        setLaptopDetails()
    End Sub

    Private Sub setLaptopDetails()
        ddlLaptopPPE.DataSource = getPPEList("Laptop", ddlLaptopDesc.Text)
        ddlLaptopPPE.DataTextField = "Equipment_PPE_Number"
        ddlLaptopPPE.DataBind()
        ddlLaptopPPE.Items.Insert(0, "")
        ddlLaptopPPE.Items(0).Value = 0
        ddlLaptopAsset.DataSource = getAssetList("Laptop", ddlLaptopDesc.Text)
        ddlLaptopAsset.DataTextField = "Equipment_Asset_Number"
        ddlLaptopAsset.DataBind()
        ddlLaptopAsset.Items.Insert(0, "")
        ddlLaptopAsset.Items(0).Value = 0
        ddlLaptopSerial.DataSource = getSerialList("Laptop", ddlLaptopDesc.Text)
        ddlLaptopSerial.DataTextField = "Equipment_Serial"
        ddlLaptopSerial.DataBind()
        ddlLaptopSerial.Items.Insert(0, "")
        ddlLaptopSerial.Items(0).Value = 0
    End Sub

    Protected Sub ddlBatteryDesc_SelectedIndexChanged(sender As Object, e As EventArgs)
        setBatteryDetails()
    End Sub

    Private Sub setBatteryDetails()
        ddlBatteryPPE.DataSource = getPPEList("Battery", ddlBatteryDesc.Text)
        ddlBatteryPPE.DataTextField = "Equipment_PPE_Number"
        ddlBatteryPPE.DataBind()
        ddlBatteryPPE.Items.Insert(0, "")
        ddlBatteryPPE.Items(0).Value = 0
        ddlBatteryAsset.DataSource = getAssetList("Battery", ddlBatteryDesc.Text)
        ddlBatteryAsset.DataTextField = "Equipment_Asset_Number"
        ddlBatteryAsset.DataBind()
        ddlBatteryAsset.Items.Insert(0, "")
        ddlBatteryAsset.Items(0).Value = 0
        ddlBatterySerial.DataSource = getSerialList("Battery", ddlBatteryDesc.Text)
        ddlBatterySerial.DataTextField = "Equipment_Serial"
        ddlBatterySerial.DataBind()
        ddlBatterySerial.Items.Insert(0, "")
        ddlBatterySerial.Items(0).Value = 0
    End Sub

    Protected Sub ddlACDesc_SelectedIndexChanged(sender As Object, e As EventArgs)
        setAcDetails()
    End Sub

    Private Sub setAcDetails()
        ddlACPPE.DataSource = getPPEList("AC Adaptor", ddlACDesc.Text)
        ddlACPPE.DataTextField = "Equipment_PPE_Number"
        ddlACPPE.DataBind()
        ddlACPPE.Items.Insert(0, "")
        ddlACPPE.Items(0).Value = 0
        ddlACAsset.DataSource = getAssetList("AC Adaptor", ddlACDesc.Text)
        ddlACAsset.DataTextField = "Equipment_Asset_Number"
        ddlACAsset.DataBind()
        ddlACAsset.Items.Insert(0, "")
        ddlACAsset.Items(0).Value = 0
        ddlACSerial.DataSource = getSerialList("AC Adaptor", ddlACDesc.Text)
        ddlACSerial.DataTextField = "Equipment_Serial"
        ddlACSerial.DataBind()
        ddlACSerial.Items.Insert(0, "")
        ddlACSerial.Items(0).Value = 0
    End Sub

    Protected Sub ddlRAMldesc_SelectedIndexChanged(sender As Object, e As EventArgs)
        setRamLDetails()
    End Sub

    Private Sub setRamLDetails()
        ddlRAMlppe.DataSource = getPPEList("Memory", ddlRAMldesc.Text)
        ddlRAMlppe.DataTextField = "Equipment_PPE_Number"
        ddlRAMlppe.DataBind()
        ddlRAMlppe.Items.Insert(0, "")
        ddlRAMlppe.Items(0).Value = 0
        ddlRAMlasset.DataSource = getAssetList("Memory", ddlRAMldesc.Text)
        ddlRAMlasset.DataTextField = "Equipment_Asset_Number"
        ddlRAMlasset.DataBind()
        ddlRAMlasset.Items.Insert(0, "")
        ddlRAMlasset.Items(0).Value = 0
        ddlRAMlserial.DataSource = getSerialList("Memory", ddlRAMldesc.Text)
        ddlRAMlserial.DataTextField = "Equipment_Serial"
        ddlRAMlserial.DataBind()
        ddlRAMlserial.Items.Insert(0, "")
        ddlRAMlserial.Items(0).Value = 0
    End Sub
#End Region
#Region "sub"

    Protected Sub ddlDesc2_SelectedIndexChanged(sender As Object, e As EventArgs)
        getDetails(ddlDesc2.Text, txtSearch.Text)
    End Sub

    Protected Sub ddlType2_SelectedIndexChanged(sender As Object, e As EventArgs)
        getDesc2(ddlType2.Text)
    End Sub

    Protected Sub gvAssignEquip_SelectedIndexChanged(sender As Object, e As EventArgs)
        Dim dt As New DataTable
        dt.Columns.Add("Type")
        dt.Columns.Add("Description")
        dt.Columns.Add("PPE")
        dt.Columns.Add("Serial")
        dt.Columns.Add("Asset")
        Dim x() As String
        If gvEqpList.Rows.Count <> 0 Then
            For Each row As GridViewRow In gvEqpList.Rows
                Dim dr As DataRow = dt.NewRow()
                For j = 0 To row.Cells.Count - 1
                    dr(j) = row.Cells(j).Text
                Next
                dt.Rows.Add(dr)
            Next
        End If
        x = {ddlType2.Text, ddlDesc2.Text, gvAssignEquip.SelectedRow.Cells(1).Text, gvAssignEquip.SelectedRow.Cells(2).Text, gvAssignEquip.SelectedRow.Cells(3).Text}
        dt.Rows.Add(x)
        gvEqpList.DataSource = dt
        gvEqpList.DataBind()
    End Sub

    Protected Sub gvAssignEquip_RowDataBound(sender As Object, e As GridViewRowEventArgs)
        If e.Row.RowType = DataControlRowType.DataRow Then
            e.Row.Attributes("onclick") = Page.ClientScript.GetPostBackClientHyperlink(gvAssignEquip, "Select$" & e.Row.RowIndex)
            e.Row.ToolTip = "Click to select this row."
        End If
    End Sub

    'Private Sub setDept()
    '    Dim getData As New Inventory
    '    Dim ds As New DataSet

    '    ds = getData.getDept()
    '    ddlDepartment.DataSource = ds
    '    ddlDepartment.DataTextField = "DeptName"
    '    ddlDepartment.DataValueField = "idDept"
    '    ddlDepartment.DataBind()
    '    ddlDepartment.Items.Insert(0, "")
    '    ddlDepartment.Items(0).Value = 0
    'End Sub

    Private Sub setinital()
        Dim dt As New DataTable
        gvAssignEquip.DataSource = dt
        gvAssignEquip.DataBind()
        gvEqpList.DataSource = dt
        gvEqpList.DataBind()
    End Sub

    Private Sub getDetails(param_details As String, param_search As String)
        Dim getData As New Inventory
        Dim ds As New DataSet

        ds = getData.getItemDetails(param_details, param_search)
        gvAssignEquip.DataSource = ds
        gvAssignEquip.DataBind()
    End Sub

    Private Sub getDesc2(param_search As String)
        Dim getData As New Inventory
        Dim ds As New DataSet

        ds = getData.getDescription(param_search)
        ddlDesc2.DataSource = ds
        ddlDesc2.DataTextField = "Equipment_Description"
        ddlDesc2.DataBind()
        ddlDesc2.Items.Insert(0, " ")
        ddlDesc2.Items(0).Value = 0
        'ddlDesc2.Items.Insert(0, " ")
        'ddlDesc2.Items(0).Value = 0
        'ddlDesc2.DataBind()
    End Sub

    Private Sub getTypes()
        Dim getData As New Inventory
        Dim ds As New DataSet

        ds = getData.getTypes()
        ddlType2.DataSource = ds
        ddlType2.DataTextField = "EQType"
        ddlType2.DataBind()
        ddlType2.Items.Insert(0, "")
        ddlType2.Items(0).Value = 0
        ddlType2.Items.Insert(ddlType2.Items.Count, "Others")
        ddlType2.Items(0).Value = ddlType2.Items.Count
    End Sub

    Protected Sub gvEqpList_SelectedIndexChanged(sender As Object, e As EventArgs)
        Dim dt As New DataTable
        dt.Columns.Add("Type")
        dt.Columns.Add("Description")
        dt.Columns.Add("PPE")
        dt.Columns.Add("Serial")
        dt.Columns.Add("Asset")
        If gvEqpList.Rows.Count <> 0 Then
            For Each row As GridViewRow In gvEqpList.Rows
                Dim dr As DataRow = dt.NewRow()
                For j = 0 To row.Cells.Count - 1
                    dr(j) = row.Cells(j).Text
                Next
                dt.Rows.Add(dr)
            Next
        End If
        dt.Rows.RemoveAt(gvEqpList.SelectedRow.RowIndex)
        gvEqpList.DataSource = dt
        gvEqpList.DataBind()
    End Sub

    Protected Sub gvEqpList_RowDataBound(sender As Object, e As GridViewRowEventArgs)
        If e.Row.RowType = DataControlRowType.DataRow Then
            e.Row.Attributes("onclick") = Page.ClientScript.GetPostBackClientHyperlink(gvEqpList, "Select$" & e.Row.RowIndex)
            e.Row.ToolTip = "Click to select this row."
        End If
    End Sub

    Protected Sub btnAdditional_Click(sender As Object, e As EventArgs)
        dvGVadditional.Visible = True
    End Sub

    Protected Sub gvAssignEquip_PageIndexChanging(sender As Object, e As GridViewPageEventArgs)
        gvAssignEquip.PageIndex = e.NewPageIndex
        getDetails(ddlDesc2.Text, txtSearch.Text)
    End Sub

    Protected Sub txtSearch_TextChanged(sender As Object, e As EventArgs)
        getDetails(ddlDesc2.Text, txtSearch.Text)
    End Sub

    Protected Sub btnSearchHost_Click(sender As Object, e As EventArgs)
        Try
            Dim clsI As New Inventory
            If clsI.databaseValidation("tbl_Host_Names", "Host_Name", txtHostName.Text.Trim & "' AND active = 'Yes ") = 0 Then
                ctrlData = 1
                Dim uid As String = ""
                Dim cls As New Assign_Equip

                uid = cls.getID(txtHostName.Text)
                If uid <> "" And cls.validateHostName(txtHostName.Text) = 0 Then
                    getAllData(uid)
                    mode = "Update"
                Else
                    ctrlData = 0
                    clear(0)
                    mode = "Save"
                    dvAdditional.Visible = False
                    dvDesktop.Visible = False
                    dvLaptop.Visible = False
                End If
            Else
                System.Web.HttpContext.Current.Response.Write("<script>alert(" & ControlChars.Quote & "Host name already exists" & ControlChars.Quote & ");</script>")
                txtHostName.Text = ""
            End If
        Catch ex As Exception
            System.Web.HttpContext.Current.Response.Write("<script>alert(" & ControlChars.Quote & ex.ToString() & ControlChars.Quote & ");</script>")
            ctrlData = 0
            mode = "Save"
            clear(0)
        End Try
    End Sub

    Protected Sub btnYes_Click(sender As Object, e As EventArgs)
        Dim getData As New View_Accountability
        Dim ds As New DataSet

        ds = getData.printView(txtAssignedTo.Text, "")
        gvView.Visible = True
        gvView.DataSource = ds
        gvView.DataBind()

        Session("ctrl") = Panel1
        Panel1.Visible = True
        ClientScript.RegisterStartupScript(Me.GetType(), "onclick", "<script language=javascript>window.open('Print.aspx','_blank');</script>")
        idMesBox.Visible = False
        clear(1)
        System.Web.HttpContext.Current.Response.Write("<script>alert(" & ControlChars.Quote & " -Save and print successful!" & ControlChars.Quote & ");</script>")

        'Session("Username") = strUserName
        'Session("FullName") = strFullName
        'strUserName = ""
        'strFullName = ""
        'Timer1.Enabled = True
        ' Response.Redirect("~/Forms/UserAccountability.aspx")
    End Sub

    Protected Sub btnNo_Click(sender As Object, e As EventArgs)
        'ScriptManager.RegisterStartupScript(Me, Me.[GetType](), "Pop", "closeMsgBox();", True)
        idMesBox.Visible = False
        clear(1)
        Session("Username") = strUserName
        Session("FullName") = strFullName
        strUserName = ""
        strFullName = ""

        Response.Redirect("~/Forms/UserAccountability.aspx")
    End Sub

    Protected Sub Button1_Click(sender As Object, e As EventArgs)
        Dim getData As New View_Accountability
        Dim ds As New DataSet

        ds = getData.getAllItems(txtAssignedTo.Text, "")
        gvView.Visible = True
        gvView.DataSource = ds
        gvView.DataBind()

        'updPnl1.Update()
        idMesBox.Visible = True

        'Dim stringwriter As New StringWriter()

        'Dim writer As New HtmlTextWriter(stringwriter)

        'writer.WriteBeginTag("p")
        'writer.Write(HtmlTextWriter.TagRightChar)
        'writer.Write("Dear Customer, here is Montly Report of Products.")
        'writer.WriteEndTag("p")


        'writer.WriteBeginTag("table")
        'writer.AddAttribute(HtmlTextWriterAttribute.Border, "1")
        'writer.Write(HtmlTextWriter.TagRightChar)

        'writer.WriteBeginTag("tr")
        'writer.Write(HtmlTextWriter.TagRightChar)
        'writer.WriteBeginTag("td")
        'writer.Write(HtmlTextWriter.TagRightChar)
        'writer.Write("Product Name")
        'writer.WriteEndTag("td")

        'writer.WriteBeginTag("td")
        'writer.Write(HtmlTextWriter.TagRightChar)
        'writer.Write("Quantity")
        'writer.WriteEndTag("td")


        'writer.WriteBeginTag("td")
        'writer.Write(HtmlTextWriter.TagRightChar)
        'writer.Write("Price")
        'writer.WriteEndTag("td")

        'writer.WriteEndTag("tr")
        'writer.WriteBeginTag("tr")

        'writer.WriteBeginTag("td")
        'writer.Write(HtmlTextWriter.TagRightChar)
        'writer.Write("Samsung Android")
        'writer.WriteEndTag("td")

        'writer.WriteBeginTag("td")
        'writer.Write(HtmlTextWriter.TagRightChar)
        'writer.Write("5")
        'writer.WriteEndTag("td")

        'writer.WriteBeginTag("td")
        'writer.Write(HtmlTextWriter.TagRightChar)
        'writer.Write(10750.51)
        'writer.WriteEndTag("td")

        'writer.WriteEndTag("tr")

        'writer.WriteEndTag("table")

        'File.WriteAllText(Server.MapPath("\Print\PrintAccountability.html"), writer.InnerWriter.ToString())

        'Render(writer)


    End Sub

    'Protected Overrides Sub Render(ByVal writer As HtmlTextWriter)
    '    Using htmlwriter As HtmlTextWriter = New HtmlTextWriter(New StringWriter())
    '        MyBase.Render(htmlwriter)
    '        Dim renderedContent As String = htmlwriter.ToString()
    '        File.WriteAllText("D:\test.html", renderedContent)
    '        writer.Write(renderedContent)
    '    End Using
    'End Sub

    Protected Sub btnBack_Click(sender As Object, e As EventArgs)
        Response.Redirect("~/Forms/UserAccountability.aspx")
    End Sub

    'Protected Sub Timer1_Tick(sender As Object, e As EventArgs)
    '    Response.Redirect("~/Forms/UserAccountability.aspx")
    '    Timer1.Enabled = False
    'End Sub
#End Region

    Private Sub ViewRFTprintPreview(param_accID As String)
        Dim lineitems As New PrintHelper
        Dim lData = lineitems.LineItems(param_accID)
        Dim dt1 As New DataTable
        Dim dt2 As New DataTable

        Dim i As Integer = 0
        Dim strTransferTo As String = Nothing
        Dim strTransferFrom As String = Nothing
        Dim strDepartment As String = Nothing
        Dim strDepartment2 As String = Nothing
        Dim strRFTdate As String = Nothing
        Dim strSignature As String = Nothing
        Dim strRFTcomment As String = Nothing

        Dim viewer As New ReportViewer
        viewer.ProcessingMode = ProcessingMode.Local

        viewer.LocalReport.ReportEmbeddedResource = "RFTprint.rdlc"

        Dim p1 As New ReportParameter("TransferTo", strTransferTo)
        Dim p2 As New ReportParameter("TransferFrom", strTransferFrom)
        Dim p3 As New ReportParameter("Department", strDepartment)
        Dim p4 As New ReportParameter("Department2", strDepartment2)
        Dim p5 As New ReportParameter("RFTdate", strRFTdate)
        Dim p6 As New ReportParameter("Signature", strSignature)
        Dim p7 As New ReportParameter("RFTcomment", strRFTcomment)
        Dim p8 As New ReportParameter("test", strRFTcomment)

        viewer.LocalReport.SetParameters(New ReportParameter() {p1, p2, p3, p4, p5, p6, p7, p8})

        Dim a As Integer = 0
        dt1 = lData.Tables(0).Clone
        dt2 = lData.Tables(0).Clone
        For a = 0 To 5
            dt1.ImportRow(lData.Tables(0).Rows(a))
        Next

        Dim repDataSource1 As ReportDataSource = New ReportDataSource("DataSet1", dt1)
        viewer.LocalReport.DataSources.Clear()
        viewer.LocalReport.DataSources.Add(repDataSource1)

        Dim warnings As Microsoft.Reporting.WebForms.Warning() = Nothing
        Dim streamids As String() = Nothing
        Dim mimeType As String = Nothing
        Dim encoding As String = Nothing
        Dim extension As String = Nothing
        Dim deviceInfo As String
        Dim bytes As Byte()
        Dim lr As New Microsoft.Reporting.WebForms.LocalReport

        deviceInfo = "<deviceinfo><simplepageheaders>True</simplepageheaders></deviceinfo>"
        bytes = viewer.LocalReport.Render("PDF")

        Response.Buffer = True
        Response.Clear()
        Response.ContentType = mimeType
        Response.AddHeader("content-lenght", bytes.Length.ToString())
        Response.BinaryWrite(bytes)
        ' create the file
        Response.Flush()

    End Sub

    Protected Sub btnSearchDomain_Click(sender As Object, e As EventArgs)
        Dim data = RetrieveData(txtSearchEmployeeDomain.Text.ToLower())
        If data.Properties.Contains("GivenName") Then
            txtAssignedTo.Text = data.Properties("GivenName").Value.ToString() + " " + data.Properties("sn").Value.ToString()
            txtPosition.Text = data.Properties("title").Value.ToString()
            txtDepartment.Text = data.Properties("department").Value.ToString()
        End If
    End Sub
End Class