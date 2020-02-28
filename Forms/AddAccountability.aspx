<%@ Page Title="Accountability - Add" Language="vb"
    AutoEventWireup="false"
    CodeBehind="AddAccountability.aspx.vb"
    Inherits="InventorySystem.AssignEquipment"
    EnableEventValidation="false"
    EnableSessionState="True"
    MasterPageFile="~/Forms/pmInventory.Master"
    MaintainScrollPositionOnPostback="true" %>
<%@ Import Namespace="System.Web.Script.Serialization" %>


<%@ MasterType VirtualPath="~/Forms/pmInventory.Master" %>

<%@ Register Assembly="EditableDropDownList" Namespace="EditableControls" TagPrefix="editable" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Accountability - Add</title>
    <link rel="stylesheet" href="../CSS/w3.css" />
    <link href="../css/jquery-ui.css" rel="stylesheet" type="text/css" />
    <script src="../js/jquery-1.6.4.min.js" type="text/javascript"></script>
    <script src="../js/jquery.ui.core.js" type="text/javascript"></script>
    <script src="../js/jquery.ui.widget.js" type="text/javascript"></script>
    <script src="../js/jquery.ui.button.js" type="text/javascript"></script>
    <script src="../js/jquery.ui.position.js" type="text/javascript"></script>
    <script src="../js/jquery.ui.autocomplete.js" type="text/javascript"></script>
    <script src="../js/jquery.ui.combobox.js" type="text/javascript"></script>
    <style>
        .labelSytle {
            font-size: 12px;
            font-weight: 700;
        }

        .textboxStyle {
            font-size: 12px;
        }

        .lineTextBoxStyle {
            padding: 6px;
            font-size: 12px;
            height: 35px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <%--<asp:Timer ID="Timer1" runat="server" Enabled="False" Interval="500" OnTick="Timer1_Tick"></asp:Timer>--%>
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <%-- <asp:UpdatePanel runat="server" UpdateMode="Conditional" ID="updPnl">
        <ContentTemplate>--%>
    <div class="w3-container" style="margin-left: 40px; margin-right: 40px; margin-top: 80px; overflow: auto">
        <div class="w3-card-4 w3-round-small w3-white" style="margin-top: 4px; margin-bottom: 4px;">
            <div class="w3-row">
                <div class="w3-row w3-padding">
                    <div class="w3-col s12 w3-padding-left" style="border-bottom: 1px solid #EEEEEE">
                        <div class="w3-left">
                            <h5 style="border-left: 6px solid #1e8cdb;">&nbsp;<b> User Information</b></h5>
                        </div>
                        <div class="w3-right">
                            <asp:Label runat="server" class="labelSytle" Text="Accountability Code : "></asp:Label>
                            <asp:Label runat="server" class="labelSytle" ID="lblAcode" BackColor="Yellow"></asp:Label>
                        </div>
                    </div>
                    <div class="w3-row" style="margin-top: 5px; margin-bottom: 5px;">
                        <div class="w3-quarter" style="padding: 4px 4px 10px 4px;">
                            <asp:Table runat="server" CellPadding="0" CellSpacing="0" Style="width: 100%; border: none">
                                <asp:TableRow>
                                    <asp:TableCell>
                            <asp:Label CssClass="labelSytle" runat="server" Text="Search Employee Domain : "></asp:Label>
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell Style=" width: 100%; padding-right: 20px">
                                        <editable:EditableDropDownList runat="server" ID="txtSearchEmployeeDomain" CssClass="w3-input w3-border textboxStyle" AutoPostBack="True"></editable:EditableDropDownList>
                                    </asp:TableCell>
                                    <asp:TableCell Style="width: 100%; padding-bottom: 8%; padding-left: 5%">
                                        <asp:Button runat="server" ID="btnSearchDomain" OnClick="btnSearchDomain_Click" Text="Search" CssClass="w3-button w3-border" Font-Size="12px" Font-Bold="true" BackColor="#1e8cdb" ForeColor="White" />
                                    </asp:TableCell>
                                </asp:TableRow>
                            </asp:Table>
                        </div>
                        <div class="w3-quarter" style="padding: 4px 4px 4px 4px;">
                        </div>
                        <div class="w3-quarter" style="padding: 4px 4px 4px 4px;">
                        </div>
                        <div class="w3-quarter" style="padding: 4px 4px 4px 4px;">
                        </div>
                    </div>
                    <div class="w3-row" style="margin-top: 5px; margin-bottom: 5px;">
                        <div class="w3-quarter" style="padding: 4px 4px 4px 4px;">
                            <asp:Label CssClass="labelSytle" runat="server" Text="Employee Name : "></asp:Label>
                            <asp:TextBox runat="server" ID="txtAssignedTo" CssClass="w3-input w3-border textboxStyle"></asp:TextBox>
                        </div>
                        <div class="w3-quarter" style="padding: 4px 4px 4px 4px;">
                            <asp:Label CssClass="labelSytle" runat="server" Text="Position : "></asp:Label>
                            <asp:TextBox runat="server" ID="txtPosition" CssClass="w3-input w3-border textboxStyle"></asp:TextBox>
                        </div>
                        <div class="w3-quarter" style="padding: 4px 4px 4px 4px;">
                            <asp:Label CssClass="labelSytle" runat="server" Text="Assignment Date : "></asp:Label>
                            <asp:TextBox runat="server" ID="txtAssignDate" CssClass="w3-input w3-border textboxStyle" TextMode="Date"></asp:TextBox>
                        </div>
                        <div class="w3-quarter" style="padding: 4px 4px 4px 4px;">
                            <asp:Table runat="server" CellPadding="0" CellSpacing="0" Style="width: 100%; border: none">
                                <asp:TableRow>
                                    <asp:TableCell>
                                                <asp:Label CssClass="labelSytle" runat="server" Text="Host Name : "></asp:Label>
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell Style="width: 100%">
                                        <asp:TextBox runat="server" ID="txtHostName" CssClass="w3-input w3-border textboxStyle" Width="100%"></asp:TextBox>
                                    </asp:TableCell>
                                    <asp:TableCell>
                                        <asp:Button runat="server" ID="btnSearchHost" OnClick="btnSearchHost_Click" Text="Search" CssClass="w3-button w3-border" Font-Size="12px" Font-Bold="true" BackColor="#1e8cdb" ForeColor="White" />
                                    </asp:TableCell>
                                </asp:TableRow>
                            </asp:Table>
                        </div>
                    </div>

                    <%--<div class="w3-row" style="margin-top: 5px; margin-bottom: 5px;">
                        <div class="w3-third" style="padding: 4px 4px 4px 4px;">
                            <asp:Label CssClass="labelSytle" runat="server" Text="RFT No. : "></asp:Label>
                            <asp:TextBox runat="server" ID="txtRFTno" CssClass="w3-input w3-border textboxStyle"></asp:TextBox>
                        </div>
                        <div class="w3-third" style="padding: 4px 4px 4px 4px;">
                            <asp:Label CssClass="labelSytle" runat="server" Text="RFT Date : "></asp:Label>
                            <asp:TextBox runat="server" ID="txtRFTdate" CssClass="w3-input w3-border textboxStyle"></asp:TextBox>
                        </div>
                        <div class="w3-third" style="padding: 4px 4px 4px 4px;">
                            <asp:Table runat="server" CellPadding="0" CellSpacing="0" Style="width: 100%; border: none">
                                <asp:TableRow>
                                    <asp:TableCell>
                                                <asp:Label CssClass="labelSytle" runat="server" Text="Host Name : "></asp:Label>
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell Style="width: 100%">
                                        <asp:TextBox runat="server" ID="txtHostName" CssClass="w3-input w3-border textboxStyle" Width="100%"></asp:TextBox>
                                    </asp:TableCell>
                                    <asp:TableCell>
                                        <asp:Button runat="server" ID="btnSearchHost" OnClick="btnSearchHost_Click" Text="Search" CssClass="w3-button w3-border" Font-Size="12px" Font-Bold="true" BackColor="#1e8cdb" ForeColor="White" />
                                    </asp:TableCell>
                                </asp:TableRow>
                            </asp:Table>
                        </div>
                    </div>--%>

                    <div class="w3-row" style="border-bottom: 1px solid #EEEEEE; margin-top: 4px; margin-bottom: 10px;">
                        <div class="w3-quarter" style="padding: 4px 4px 4px 4px;">
                            <asp:Label runat="server" Text="OS : " CssClass="labelSytle"></asp:Label><br />
                            <editable:EditableDropDownList Width="93%" runat="server" ID="txtItemOS" CssClass="w3-input w3-border textboxStyle">
                                <asp:ListItem>---</asp:ListItem>
                                <asp:ListItem>Windows 7</asp:ListItem>
                                <asp:ListItem>Windows 8</asp:ListItem>
                                <asp:ListItem>Windows 8.1</asp:ListItem>
                                <asp:ListItem>Windows 10 Pro</asp:ListItem>
                                <asp:ListItem>Windows 10 Enterprise</asp:ListItem>
                            </editable:EditableDropDownList>
                        </div>
                        <div class="w3-quarter" style="padding: 4px 4px 4px 4px;">
                            <asp:Label CssClass="labelSytle" runat="server" Text="Department : "></asp:Label>
                            <asp:TextBox runat="server" ID="txtDepartment" CssClass="w3-input w3-border textboxStyle"></asp:TextBox>
                            <%--<editable:EditableDropDownList Width="90%" runat="server" ID="ddlDepartment" CssClass="w3-input w3-border textboxStyle"></editable:EditableDropDownList>--%>
                        </div>
                        <div class="w3-quarter" style="padding: 4px 4px 4px 4px;">
                            <asp:Label CssClass="labelSytle" runat="server" Text="Location : "></asp:Label>
                            <editable:EditableDropDownList Width="93%" runat="server" ID="ddlLocation" CssClass="w3-input w3-border textboxStyle">
                                <asp:ListItem>---</asp:ListItem>
                                <asp:ListItem>ELVDI</asp:ListItem>
                                <asp:ListItem>ITSDI</asp:ListItem>
                                <asp:ListItem>ITSG</asp:ListItem>
                                <asp:ListItem>WSI MAKATI</asp:ListItem>
                                <asp:ListItem>WSI CEBU</asp:ListItem>
                                <asp:ListItem>WSI DAVAO</asp:ListItem>
                                <asp:ListItem>WAREHOUSE</asp:ListItem>
                            </editable:EditableDropDownList>
                        </div>
                        <div class="w3-quarter" style="padding: 4px 4px 4px 4px;">
                            <asp:Label CssClass="labelSytle" runat="server" Text="Unit Type : "></asp:Label>
                            <editable:EditableDropDownList Width="93%" runat="server" ID="ddlType" AutoPostBack="true" OnSelectedIndexChanged="ddlType_SelectedIndexChanged" CssClass="w3-input w3-border textboxStyle">
                                <asp:ListItem>---</asp:ListItem>
                                <asp:ListItem>Laptop</asp:ListItem>
                                <asp:ListItem>Desktop</asp:ListItem>
                                <asp:ListItem>Others</asp:ListItem>
                            </editable:EditableDropDownList>
                        </div>
                    </div>

                    <div runat="server" id="dvDesktop" visible="False">
                        <%-- ---------------------------- --%>
                        <%--<div class="w3-col s2 w3-padding-left" style="border-bottom: 1px solid #EEEEEE">
                            <h5 style="border-left: 6px solid #1e8cdb;">&nbsp;<b> Casing</b></h5>
                        </div>--%>
                        <div class="w3-card-4 w3-round-small w3-white" style="margin-top: 4px; margin-bottom: 10px;">
                            <div class="w3-col s2 w3-padding-left" style="padding: 4px 4px 4px 4px;">
                                <h5 style="border-left: 6px solid #1e8cdb;">&nbsp;<b> Casing</b></h5>
                            </div>
                            <div class="w3-fifth" style="padding: 4px 4px 4px 4px;">
                                <asp:Label CssClass="labelSytle" runat="server" Text="Model"></asp:Label><br>
                                <editable:EditableDropDownList Width="90%" runat="server" ID="ddlCasingDesc" AutoPostBack="true" OnSelectedIndexChanged="ddlCasingDesc_SelectedIndexChanged" CssClass="w3-input w3-border textboxStyle"></editable:EditableDropDownList>
                            </div>
                            <div class="w3-fifth" style="padding: 4px 4px 4px 4px;">
                                <asp:Label CssClass="labelSytle" runat="server" Text="PPE #"></asp:Label><br>
                                <editable:EditableDropDownList Width="90%" runat="server" ID="ddlCasingPPE" AutoPostBack="true" OnSelectedIndexChanged="ddlCasingPPE_SelectedIndexChanged" CssClass="w3-input w3-border textboxStyle"></editable:EditableDropDownList>
                            </div>
                            <div class="w3-fifth" style="padding: 4px 4px 4px 4px;">
                                <asp:Label CssClass="labelSytle" runat="server" Text="Asset #"></asp:Label><br>
                                <editable:EditableDropDownList Width="90%" runat="server" ID="ddlCasingAsset" AutoPostBack="true" OnSelectedIndexChanged="ddlCasingAsset_SelectedIndexChanged" CssClass="w3-input w3-border textboxStyle"></editable:EditableDropDownList>
                            </div>
                            <div class="w3-fifth" style="padding: 4px 4px 4px 4px;">
                                <asp:Label CssClass="labelSytle" runat="server" Text="Serial #"></asp:Label><br>
                                <editable:EditableDropDownList Width="90%" runat="server" ID="ddlCasingSerial" AutoPostBack="true" OnSelectedIndexChanged="ddlCasingSerial_SelectedIndexChanged" CssClass="w3-input w3-border textboxStyle"></editable:EditableDropDownList>
                            </div>
                        </div>
                        <%-- ---------------------------- --%>
                        <%--  <div class="w3-col s2 w3-padding-left" style="border-bottom: 1px solid #EEEEEE">
                            <h5 style="border-left: 6px solid #1e8cdb;">&nbsp;<b> CPU</b></h5>
                        </div>--%>
                        <div class="w3-card-4 w3-round-small w3-white" style="margin-top: 4px; margin-bottom: 10px;">
                            <div class="w3-col s2 w3-padding-left" style="padding: 4px 4px 4px 4px;">
                                <h5 style="border-left: 6px solid #1e8cdb;">&nbsp;<b> CPU</b></h5>
                            </div>
                            <div class="w3-fifth" style="padding: 4px 4px 4px 4px;">
                                <asp:Label CssClass="labelSytle" runat="server" Text="Model" Visible="False"></asp:Label><br>
                                <editable:EditableDropDownList Width="90%" runat="server" ID="ddlCPUDesc" AutoPostBack="true" OnSelectedIndexChanged="ddlCPUDesc_SelectedIndexChanged" CssClass="w3-input w3-border textboxStyle"></editable:EditableDropDownList>
                            </div>
                            <div class="w3-fifth" style="padding: 4px 4px 4px 4px;">
                                <asp:Label CssClass="labelSytle" runat="server" Text="PPE #" Visible="False"></asp:Label><br>
                                <editable:EditableDropDownList Width="90%" runat="server" ID="ddlCPUPPE" AutoPostBack="true" OnSelectedIndexChanged="ddlCPUPPE_SelectedIndexChanged" CssClass="w3-input w3-border textboxStyle"></editable:EditableDropDownList>
                            </div>
                            <div class="w3-fifth" style="padding: 4px 4px 4px 4px;">
                                <asp:Label CssClass="labelSytle" runat="server" Text="Asset #" Visible="False"></asp:Label><br>
                                <editable:EditableDropDownList Width="90%" runat="server" ID="ddlCPUAsset" AutoPostBack="true" OnSelectedIndexChanged="ddlCPUAsset_SelectedIndexChanged" CssClass="w3-input w3-border textboxStyle"></editable:EditableDropDownList>
                            </div>
                            <div class="w3-fifth" style="padding: 4px 4px 4px 4px;">
                                <asp:Label CssClass="labelSytle" runat="server" Text="Serial #" Visible="False"></asp:Label><br>
                                <editable:EditableDropDownList Width="90%" runat="server" ID="ddlCPUSerial" AutoPostBack="true" OnSelectedIndexChanged="ddlCPUSerial_SelectedIndexChanged" CssClass="w3-input w3-border textboxStyle"></editable:EditableDropDownList>
                            </div>
                        </div>
                        <%-- ---------------------------- --%>
                        <%-- <div class="w3-col s2 w3-padding-left" style="border-bottom: 1px solid #EEEEEE">
                            <h5 style="border-left: 6px solid #1e8cdb;">&nbsp;<b> HDD</b></h5>
                        </div>--%>
                        <div class="w3-card-4 w3-round-small w3-white" style="margin-top: 4px; margin-bottom: 10px;">
                            <div class="w3-col s2 w3-padding-left" style="padding: 4px 4px 4px 4px;">
                                <h5 style="border-left: 6px solid #1e8cdb;">&nbsp;<b> HDD</b></h5>
                            </div>
                            <div class="w3-fifth" style="padding: 4px 4px 4px 4px;">
                                <asp:Label CssClass="labelSytle" runat="server" Text="Model" Visible="False"></asp:Label><br>
                                <editable:EditableDropDownList Width="90%" runat="server" ID="ddlHDDDesc" AutoPostBack="true" OnSelectedIndexChanged="ddlHDDDesc_SelectedIndexChanged" CssClass="w3-input w3-border textboxStyle"></editable:EditableDropDownList>
                            </div>
                            <div class="w3-fifth" style="padding: 4px 4px 4px 4px;">
                                <asp:Label CssClass="labelSytle" runat="server" Text="PPE #" Visible="False"></asp:Label><br>
                                <editable:EditableDropDownList Width="90%" runat="server" ID="ddlHDDPPE" AutoPostBack="true" OnSelectedIndexChanged="ddlHDDPPE_SelectedIndexChanged" CssClass="w3-input w3-border textboxStyle"></editable:EditableDropDownList>
                            </div>
                            <div class="w3-fifth" style="padding: 4px 4px 4px 4px;">
                                <asp:Label CssClass="labelSytle" runat="server" Text="Asset #" Visible="False"></asp:Label><br>
                                <editable:EditableDropDownList Width="90%" runat="server" ID="ddlHDDAsset" AutoPostBack="true" OnSelectedIndexChanged="ddlHDDAsset_SelectedIndexChanged" CssClass="w3-input w3-border textboxStyle"></editable:EditableDropDownList>
                            </div>
                            <div class="w3-fifth" style="padding: 4px 4px 4px 4px;">
                                <asp:Label CssClass="labelSytle" runat="server" Text="Serial #" Visible="False"></asp:Label><br>
                                <editable:EditableDropDownList Width="90%" runat="server" ID="ddlHDDSerial" AutoPostBack="true" OnSelectedIndexChanged="ddlHDDSerial_SelectedIndexChanged" CssClass="w3-input w3-border textboxStyle"></editable:EditableDropDownList>
                            </div>
                        </div>
                        <%-- ---------------------------- --%>
                        <%--  <div class="w3-col s2 w3-padding-left" style="border-bottom: 1px solid #EEEEEE">
                            <h5 style="border-left: 6px solid #1e8cdb;">&nbsp;<b> MOBO</b></h5>
                        </div>--%>
                        <div class="w3-card-4 w3-round-small w3-white" style="margin-top: 4px; margin-bottom: 10px;">
                            <div class="w3-col s2 w3-padding-left" style="padding: 4px 4px 4px 4px;">
                                <h5 style="border-left: 6px solid #1e8cdb;">&nbsp;<b> MOBO</b></h5>
                            </div>
                            <div class="w3-fifth" style="padding: 4px 4px 4px 4px;">
                                <asp:Label CssClass="labelSytle" runat="server" Text="Model" Visible="False"></asp:Label><br>
                                <editable:EditableDropDownList Width="90%" runat="server" ID="ddlMOBODesc" AutoPostBack="true" OnSelectedIndexChanged="ddlMOBODesc_SelectedIndexChanged" CssClass="w3-input w3-border textboxStyle"></editable:EditableDropDownList>
                            </div>
                            <div class="w3-fifth" style="padding: 4px 4px 4px 4px;">
                                <asp:Label CssClass="labelSytle" runat="server" Text="PPE #" Visible="False"></asp:Label><br>
                                <editable:EditableDropDownList Width="90%" runat="server" ID="ddlMOBOPPE" AutoPostBack="true" OnSelectedIndexChanged="ddlMOBOPPE_SelectedIndexChanged" CssClass="w3-input w3-border textboxStyle"></editable:EditableDropDownList>
                            </div>
                            <div class="w3-fifth" style="padding: 4px 4px 4px 4px;">
                                <asp:Label CssClass="labelSytle" runat="server" Text="Asset #" Visible="False"></asp:Label><br>
                                <editable:EditableDropDownList Width="90%" runat="server" ID="ddlMOBOAsset" AutoPostBack="true" OnSelectedIndexChanged="ddlMOBOAsset_SelectedIndexChanged" CssClass="w3-input w3-border textboxStyle"></editable:EditableDropDownList>
                            </div>
                            <div class="w3-fifth" style="padding: 4px 4px 4px 4px;">
                                <asp:Label CssClass="labelSytle" runat="server" Text="Serial #" Visible="False"></asp:Label><br>
                                <editable:EditableDropDownList Width="90%" runat="server" ID="ddlMOBOSerial" AutoPostBack="true" OnSelectedIndexChanged="ddlMOBOSerial_SelectedIndexChanged" CssClass="w3-input w3-border textboxStyle"></editable:EditableDropDownList>
                            </div>
                        </div>
                        <%-- ---------------------------- --%>
                        <%--<div class="w3-col s2 w3-padding-left" style="border-bottom: 1px solid #EEEEEE">
                            <h5 style="border-left: 6px solid #1e8cdb;">&nbsp;<b> RAM</b></h5>
                        </div>--%>
                        <div class="w3-card-4 w3-round-small w3-white" style="margin-top: 4px; margin-bottom: 10px;">
                            <div class="w3-col s2 w3-padding-left" style="padding: 4px 4px 4px 4px;">
                                <h5 style="border-left: 6px solid #1e8cdb;">&nbsp;<b> RAM</b></h5>
                            </div>
                            <div class="w3-fifth" style="padding: 4px 4px 4px 4px;">
                                <asp:Label CssClass="labelSytle" runat="server" Text="Model" Visible="False"></asp:Label><br>
                                <editable:EditableDropDownList Width="90%" runat="server" ID="ddlRAMDesc" AutoPostBack="true" OnSelectedIndexChanged="ddlRAMDesc_SelectedIndexChanged" CssClass="w3-input w3-border textboxStyle"></editable:EditableDropDownList>
                            </div>
                            <div class="w3-fifth" style="padding: 4px 4px 4px 4px;">
                                <asp:Label CssClass="labelSytle" runat="server" Text="PPE #" Visible="False"></asp:Label><br>
                                <editable:EditableDropDownList Width="90%" runat="server" ID="ddlRAMPPE" AutoPostBack="true" OnSelectedIndexChanged="ddlRAMPPE_SelectedIndexChanged" CssClass="w3-input w3-border textboxStyle"></editable:EditableDropDownList>
                            </div>
                            <div class="w3-fifth" style="padding: 4px 4px 4px 4px;">
                                <asp:Label CssClass="labelSytle" runat="server" Text="Asset #" Visible="False"></asp:Label><br>
                                <editable:EditableDropDownList Width="90%" runat="server" ID="ddlRAMAsset" AutoPostBack="true" OnSelectedIndexChanged="ddlRAMAsset_SelectedIndexChanged" CssClass="w3-input w3-border textboxStyle"></editable:EditableDropDownList>
                            </div>
                            <div class="w3-fifth" style="padding: 4px 4px 4px 4px;">
                                <asp:Label CssClass="labelSytle" runat="server" Text="Serial #" Visible="False"></asp:Label><br>
                                <editable:EditableDropDownList Width="90%" runat="server" ID="ddlRAMSerial" AutoPostBack="true" OnSelectedIndexChanged="ddlRAMSerial_SelectedIndexChanged" CssClass="w3-input w3-border textboxStyle"></editable:EditableDropDownList>
                            </div>
                        </div>
                        <%-- ---------------------------- --%>
                        <%--<div class="w3-col s2 w3-padding-left" style="border-bottom: 1px solid #EEEEEE">
                            <h5 style="border-left: 6px solid #1e8cdb;">&nbsp;<b> Monitor</b></h5>
                        </div>--%>
                        <div class="w3-card-4 w3-round-small w3-white" style="margin-top: 4px; margin-bottom: 10px;">
                            <div class="w3-col s2 w3-padding-left" style="padding: 4px 4px 4px 4px;">
                                <h5 style="border-left: 6px solid #1e8cdb;">&nbsp;<b> Monitor</b></h5>
                            </div>
                            <div class="w3-fifth" style="padding: 4px 4px 4px 4px;">
                                <asp:Label CssClass="labelSytle" runat="server" Text="Model" Visible="False"></asp:Label><br>
                                <editable:EditableDropDownList Width="90%" runat="server" ID="ddlMonitorDesc" AutoPostBack="true" OnSelectedIndexChanged="ddlMonitorDesc_SelectedIndexChanged" CssClass="w3-input w3-border textboxStyle"></editable:EditableDropDownList>
                            </div>
                            <div class="w3-fifth" style="padding: 4px 4px 4px 4px;">
                                <asp:Label CssClass="labelSytle" runat="server" Text="PPE #" Visible="False"></asp:Label><br>
                                <editable:EditableDropDownList Width="90%" runat="server" ID="ddlMonitorPPE" AutoPostBack="true" OnSelectedIndexChanged="ddlMonitorPPE_SelectedIndexChanged" CssClass="w3-input w3-border textboxStyle"></editable:EditableDropDownList>
                            </div>
                            <div class="w3-fifth" style="padding: 4px 4px 4px 4px;">
                                <asp:Label CssClass="labelSytle" runat="server" Text="Asset #" Visible="False"></asp:Label><br>
                                <editable:EditableDropDownList Width="90%" runat="server" ID="ddlMonitorAsset" AutoPostBack="true" OnSelectedIndexChanged="ddlMonitorAsset_SelectedIndexChanged" CssClass="w3-input w3-border textboxStyle"></editable:EditableDropDownList>
                            </div>
                            <div class="w3-fifth" style="padding: 4px 4px 4px 4px;">
                                <asp:Label CssClass="labelSytle" runat="server" Text="Serial #" Visible="False"></asp:Label><br>
                                <editable:EditableDropDownList Width="90%" runat="server" ID="ddlMonitorSerial" AutoPostBack="true" OnSelectedIndexChanged="ddlMonitorSerial_SelectedIndexChanged" CssClass="w3-input w3-border textboxStyle"></editable:EditableDropDownList>
                            </div>
                        </div>
                        <%-- ---------------------------- --%>
                        <%--<div class="w3-col s2 w3-padding-left" style="border-bottom: 1px solid #EEEEEE">
                            <h5 style="border-left: 6px solid #1e8cdb;">&nbsp;<b> Keyboard</b></h5>
                        </div>--%>
                        <div class="w3-card-4 w3-round-small w3-white" style="margin-top: 4px; margin-bottom: 10px;">
                            <div class="w3-col s2 w3-padding-left" style="padding: 4px 4px 4px 4px;">
                                <h5 style="border-left: 6px solid #1e8cdb;">&nbsp;<b> Keyboard</b></h5>
                            </div>
                            <div class="w3-fifth" style="padding: 4px 4px 4px 4px;">
                                <asp:Label CssClass="labelSytle" runat="server" Text="Model" Visible="False"></asp:Label><br>
                                <editable:EditableDropDownList Width="90%" runat="server" ID="ddlKeyboardDesc" AutoPostBack="true" OnSelectedIndexChanged="ddlKeyboardDesc_SelectedIndexChanged" CssClass="w3-input w3-border textboxStyle"></editable:EditableDropDownList>
                            </div>
                            <div class="w3-fifth" style="padding: 4px 4px 4px 4px;">
                                <asp:Label CssClass="labelSytle" runat="server" Text="PPE #" Visible="False"></asp:Label><br>
                                <editable:EditableDropDownList Width="90%" runat="server" ID="ddlKeyboardPPE" AutoPostBack="true" OnSelectedIndexChanged="ddlKeyboardPPE_SelectedIndexChanged" CssClass="w3-input w3-border textboxStyle"></editable:EditableDropDownList>
                            </div>
                            <div class="w3-fifth" style="padding: 4px 4px 4px 4px;">
                                <asp:Label CssClass="labelSytle" runat="server" Text="Asset #" Visible="False"></asp:Label><br>
                                <editable:EditableDropDownList Width="90%" runat="server" ID="ddlKeyboardAsset" AutoPostBack="true" OnSelectedIndexChanged="ddlKeyboardAsset_SelectedIndexChanged" CssClass="w3-input w3-border textboxStyle"></editable:EditableDropDownList>
                            </div>
                            <div class="w3-fifth" style="padding: 4px 4px 4px 4px;">
                                <asp:Label CssClass="labelSytle" runat="server" Text="Serial #" Visible="False"></asp:Label><br>
                                <editable:EditableDropDownList Width="90%" runat="server" ID="ddlKeyboardSerial" AutoPostBack="true" OnSelectedIndexChanged="ddlKeyboardSerial_SelectedIndexChanged" CssClass="w3-input w3-border textboxStyle"></editable:EditableDropDownList>
                            </div>
                        </div>
                        <%-- ---------------------------- --%>
                        <%--<div class="w3-col s2 w3-padding-left" style="border-bottom: 1px solid #EEEEEE">
                            <h5 style="border-left: 6px solid #1e8cdb;">&nbsp;<b> Mouse</b></h5>
                        </div>--%>
                        <div class="w3-card-4 w3-round-small w3-white" style="margin-top: 4px; margin-bottom: 10px;">
                            <div class="w3-col s2 w3-padding-left" style="padding: 4px 4px 4px 4px;">
                                <h5 style="border-left: 6px solid #1e8cdb;">&nbsp;<b> Mouse</b></h5>
                            </div>
                            <div class="w3-fifth" style="padding: 4px 4px 4px 4px;">
                                <asp:Label CssClass="labelSytle" runat="server" Text="Model" Visible="False"></asp:Label><br>
                                <editable:EditableDropDownList Width="90%" runat="server" ID="ddlMouseDesc" AutoPostBack="true" OnSelectedIndexChanged="ddlMouseDesc_SelectedIndexChanged" CssClass="w3-input w3-border textboxStyle"></editable:EditableDropDownList>
                            </div>
                            <div class="w3-fifth" style="padding: 4px 4px 4px 4px;">
                                <asp:Label CssClass="labelSytle" runat="server" Text="PPE #" Visible="False"></asp:Label><br>
                                <editable:EditableDropDownList Width="90%" runat="server" ID="ddlMousePPE" AutoPostBack="true" OnSelectedIndexChanged="ddlMousePPE_SelectedIndexChanged" CssClass="w3-input w3-border textboxStyle"></editable:EditableDropDownList>
                            </div>
                            <div class="w3-fifth" style="padding: 4px 4px 4px 4px;">
                                <asp:Label CssClass="labelSytle" runat="server" Text="Asset #" Visible="False"></asp:Label><br>
                                <editable:EditableDropDownList Width="90%" runat="server" ID="ddlMouseAsset" AutoPostBack="true" OnSelectedIndexChanged="ddlMouseAsset_SelectedIndexChanged" CssClass="w3-input w3-border textboxStyle"></editable:EditableDropDownList>
                            </div>
                            <div class="w3-fifth" style="padding: 4px 4px 4px 4px;">
                                <asp:Label CssClass="labelSytle" runat="server" Text="Serial #" Visible="False"></asp:Label><br>
                                <editable:EditableDropDownList Width="90%" runat="server" ID="ddlMouseSerial" AutoPostBack="true" OnSelectedIndexChanged="ddlMouseSerial_SelectedIndexChanged" CssClass="w3-input w3-border textboxStyle"></editable:EditableDropDownList>
                            </div>
                        </div>
                        <%-- ---------------------------- --%>
                        <%--<div class="w3-col s2 w3-padding-left" style="border-bottom: 1px solid #EEEEEE">
                            <h5 style="border-left: 6px solid #1e8cdb;">&nbsp;<b> PSU</b></h5>
                        </div>--%>
                        <div class="w3-card-4 w3-round-small w3-white" style="margin-top: 4px; margin-bottom: 10px;">
                            <div class="w3-col s2 w3-padding-left" style="padding: 4px 4px 4px 4px;">
                                <h5 style="border-left: 6px solid #1e8cdb;">&nbsp;<b> PSU</b></h5>
                            </div>
                            <div class="w3-fifth" style="padding: 4px 4px 4px 4px;">
                                <asp:Label CssClass="labelSytle" runat="server" Text="Model" Visible="False"></asp:Label><br>
                                <editable:EditableDropDownList Width="90%" runat="server" ID="ddlPSUDesc" AutoPostBack="true" OnSelectedIndexChanged="ddlPSUDesc_SelectedIndexChanged" CssClass="w3-input w3-border textboxStyle"></editable:EditableDropDownList>
                            </div>
                            <div class="w3-fifth" style="padding: 4px 4px 4px 4px;">
                                <asp:Label CssClass="labelSytle" runat="server" Text="PPE #" Visible="False"></asp:Label><br>
                                <editable:EditableDropDownList Width="90%" runat="server" ID="ddlPSUPPE" AutoPostBack="true" OnSelectedIndexChanged="ddlPSUPPE_SelectedIndexChanged" CssClass="w3-input w3-border textboxStyle"></editable:EditableDropDownList>
                            </div>
                            <div class="w3-fifth" style="padding: 4px 4px 4px 4px;">
                                <asp:Label CssClass="labelSytle" runat="server" Text="Asset #" Visible="False"></asp:Label><br>
                                <editable:EditableDropDownList Width="90%" runat="server" ID="ddlPSUAsset" AutoPostBack="true" OnSelectedIndexChanged="ddlPSUAsset_SelectedIndexChanged" CssClass="w3-input w3-border textboxStyle"></editable:EditableDropDownList>
                            </div>
                            <div class="w3-fifth" style="padding: 4px 4px 4px 4px;">
                                <asp:Label CssClass="labelSytle" runat="server" Text="Serial #" Visible="False"></asp:Label><br>
                                <editable:EditableDropDownList Width="90%" runat="server" ID="ddlPSUSerial" AutoPostBack="true" OnSelectedIndexChanged="ddlPSUSerial_SelectedIndexChanged" CssClass="w3-input w3-border textboxStyle"></editable:EditableDropDownList>
                            </div>
                        </div>
                        <%-- ---------------------------- --%>
                        <%--<div class="w3-col s2 w3-padding-left" style="border-bottom: 1px solid #EEEEEE">
                            <h5 style="border-left: 6px solid #1e8cdb;">&nbsp;<b> IP Phone</b></h5>
                        </div>--%>
                        <div class="w3-card-4 w3-round-small w3-white" style="margin-top: 4px; margin-bottom: 10px;">
                            <div class="w3-col s2 w3-padding-left" style="padding: 4px 4px 4px 4px;">
                                <h5 style="border-left: 6px solid #1e8cdb;">&nbsp;<b> IP Phone</b></h5>
                            </div>
                            <div class="w3-fifth" style="padding: 4px 4px 4px 4px;">
                                <asp:Label CssClass="labelSytle" runat="server" Text="Model" Visible="False"></asp:Label><br>
                                <editable:EditableDropDownList Width="90%" runat="server" ID="ddlIPPhoneDesc" AutoPostBack="true" OnSelectedIndexChanged="ddlIPPhoneDesc_SelectedIndexChanged" CssClass="w3-input w3-border textboxStyle"></editable:EditableDropDownList>
                            </div>
                            <div class="w3-fifth" style="padding: 4px 4px 4px 4px;">
                                <asp:Label CssClass="labelSytle" runat="server" Text="PPE #" Visible="False"></asp:Label><br>
                                <editable:EditableDropDownList Width="90%" runat="server" ID="ddlIPPhonePPE" AutoPostBack="true" OnSelectedIndexChanged="ddlIPPhonePPE_SelectedIndexChanged" CssClass="w3-input w3-border textboxStyle"></editable:EditableDropDownList>
                            </div>
                            <div class="w3-fifth" style="padding: 4px 4px 4px 4px;">
                                <asp:Label CssClass="labelSytle" runat="server" Text="Asset #" Visible="False"></asp:Label><br>
                                <editable:EditableDropDownList Width="90%" runat="server" ID="ddlIPPhoneAsset" AutoPostBack="true" OnSelectedIndexChanged="ddlIPPhoneAsset_SelectedIndexChanged" CssClass="w3-input w3-border textboxStyle"></editable:EditableDropDownList>
                            </div>
                            <div class="w3-fifth" style="padding: 4px 4px 4px 4px;">
                                <asp:Label CssClass="labelSytle" runat="server" Text="Serial #" Visible="False"></asp:Label><br>
                                <editable:EditableDropDownList Width="90%" runat="server" ID="ddlIPPhoneSerial" AutoPostBack="true" OnSelectedIndexChanged="ddlIPPhoneSerial_SelectedIndexChanged" CssClass="w3-input w3-border textboxStyle"></editable:EditableDropDownList>
                            </div>
                        </div>

                    </div>
                    <div runat="server" id="dvLaptop" visible="False">
                        <%-- ---------------------------- --%>
                        <%--<div class="w3-col s2 w3-padding-left" style="border-bottom: 1px solid #EEEEEE">
                            <h5 style="border-left: 6px solid #1e8cdb;">&nbsp;<b> Laptop</b></h5>
                        </div>--%>
                        <div class="w3-card-4 w3-round-small w3-white" style="margin-top: 4px; margin-bottom: 10px;">
                            <div class="w3-col s2 w3-padding-left" style="padding: 4px 4px 4px 4px;">
                                <h5 style="border-left: 6px solid #1e8cdb;">&nbsp;<b> Laptop</b></h5>
                            </div>
                            <div class="w3-fifth" style="padding: 4px 4px 4px 4px;">
                                <asp:Label CssClass="labelSytle" runat="server" Text="Model"></asp:Label><br>
                                <editable:EditableDropDownList Width="90%" runat="server" ID="ddlLaptopDesc" AutoPostBack="true" OnSelectedIndexChanged="ddlLaptopDesc_SelectedIndexChanged" CssClass="w3-input w3-border textboxStyle">
                                    <asp:ListItem Value="0"></asp:ListItem>
                                </editable:EditableDropDownList>
                            </div>
                            <div class="w3-fifth" style="padding: 4px 4px 4px 4px;">
                                <asp:Label CssClass="labelSytle" runat="server" Text="PPE #"></asp:Label><br>
                                <editable:EditableDropDownList Width="90%" runat="server" ID="ddlLaptopPPE" AutoPostBack="true" OnSelectedIndexChanged="ddlLaptopPPE_SelectedIndexChanged" CssClass="w3-input w3-border textboxStyle"></editable:EditableDropDownList>
                            </div>
                            <div class="w3-fifth" style="padding: 4px 4px 4px 4px;">
                                <asp:Label CssClass="labelSytle" runat="server" Text="Asset #"></asp:Label><br>
                                <editable:EditableDropDownList Width="90%" runat="server" ID="ddlLaptopAsset" AutoPostBack="true" OnSelectedIndexChanged="ddlLaptopAsset_SelectedIndexChanged" CssClass="w3-input w3-border textboxStyle"></editable:EditableDropDownList>
                            </div>
                            <div class="w3-fifth" style="padding: 4px 4px 4px 4px;">
                                <asp:Label CssClass="labelSytle" runat="server" Text="Serial #"></asp:Label><br>
                                <editable:EditableDropDownList Width="90%" runat="server" ID="ddlLaptopSerial" AutoPostBack="true" OnSelectedIndexChanged="ddlLaptopSerial_SelectedIndexChanged" CssClass="w3-input w3-border textboxStyle"></editable:EditableDropDownList>
                            </div>
                        </div>
                        <%-- ---------------------------- --%>
                        <%--<div class="w3-col s2 w3-padding-left" style="border-bottom: 1px solid #EEEEEE">
                            <h5 style="border-left: 6px solid #1e8cdb;">&nbsp;<b> Battery</b></h5>
                        </div>--%>
                        <div class="w3-card-4 w3-round-small w3-white" style="margin-top: 4px; margin-bottom: 10px;">
                             <div class="w3-col s2 w3-padding-left" style="padding: 4px 4px 4px 4px;">
                                <h5 style="border-left: 6px solid #1e8cdb;">&nbsp;<b> Battery</b></h5>
                            </div>
                            <div class="w3-fifth" style="padding: 4px 4px 4px 4px;">
                                <asp:Label CssClass="labelSytle" runat="server" Text="Model" Visible="False"></asp:Label><br>
                                <editable:EditableDropDownList Width="90%" runat="server" ID="ddlBatteryDesc" AutoPostBack="true" OnSelectedIndexChanged="ddlBatteryDesc_SelectedIndexChanged" CssClass="w3-input w3-border textboxStyle"></editable:EditableDropDownList>
                            </div>
                            <div class="w3-fifth" style="padding: 4px 4px 4px 4px;">
                                <asp:Label CssClass="labelSytle" runat="server" Text="PPE #" Visible="False"></asp:Label><br>
                                <editable:EditableDropDownList Width="90%" runat="server" ID="ddlBatteryPPE" AutoPostBack="true" OnSelectedIndexChanged="ddlBatteryPPE_SelectedIndexChanged" CssClass="w3-input w3-border textboxStyle"></editable:EditableDropDownList>
                            </div>
                            <div class="w3-fifth" style="padding: 4px 4px 4px 4px;">
                                <asp:Label CssClass="labelSytle" runat="server" Text="Asset #" Visible="False"></asp:Label><br>
                                <editable:EditableDropDownList Width="90%" runat="server" ID="ddlBatteryAsset" AutoPostBack="true" OnSelectedIndexChanged="ddlBatteryAsset_SelectedIndexChanged" CssClass="w3-input w3-border textboxStyle"></editable:EditableDropDownList>
                            </div>
                            <div class="w3-fifth" style="padding: 4px 4px 4px 4px;">
                                <asp:Label CssClass="labelSytle" runat="server" Text="Serial #" Visible="False"></asp:Label><br>
                                <editable:EditableDropDownList Width="90%" runat="server" ID="ddlBatterySerial" AutoPostBack="true" OnSelectedIndexChanged="ddlBatterySerial_SelectedIndexChanged" CssClass="w3-input w3-border textboxStyle"></editable:EditableDropDownList>
                            </div>
                        </div>
                        <%-- ---------------------------- --%>
                        <%--<div class="w3-col s2 w3-padding-left" style="border-bottom: 1px solid #EEEEEE">
                            <h5 style="border-left: 6px solid #1e8cdb;">&nbsp;<b> AC Adaptor</b></h5>
                        </div>--%>
                        <div class="w3-card-4 w3-round-small w3-white" style="margin-top: 4px; margin-bottom: 10px;">
                            <div class="w3-col s2 w3-padding-left" style="padding: 4px 4px 4px 4px;">
                                <h5 style="border-left: 6px solid #1e8cdb;">&nbsp;<b> AC Adaptor</b></h5>
                            </div>
                            <div class="w3-fifth" style="padding: 4px 4px 4px 4px;">
                                <asp:Label CssClass="labelSytle" runat="server" Text="Model" Visible="False"></asp:Label><br>
                                <editable:EditableDropDownList Width="90%" runat="server" ID="ddlACDesc" AutoPostBack="true" OnSelectedIndexChanged="ddlACDesc_SelectedIndexChanged" CssClass="w3-input w3-border textboxStyle"></editable:EditableDropDownList>
                            </div>
                            <div class="w3-fifth" style="padding: 4px 4px 4px 4px;">
                                <asp:Label CssClass="labelSytle" runat="server" Text="PPE #" Visible="False"></asp:Label><br>
                                <editable:EditableDropDownList Width="90%" runat="server" ID="ddlACPPE" AutoPostBack="true" OnSelectedIndexChanged="ddlACPPE_SelectedIndexChanged" CssClass="w3-input w3-border textboxStyle"></editable:EditableDropDownList>
                            </div>
                            <div class="w3-fifth" style="padding: 4px 4px 4px 4px;">
                                <asp:Label CssClass="labelSytle" runat="server" Text="Asset #" Visible="False"></asp:Label><br>
                                <editable:EditableDropDownList Width="90%" runat="server" ID="ddlACAsset" AutoPostBack="true" OnSelectedIndexChanged="ddlACAsset_SelectedIndexChanged" CssClass="w3-input w3-border textboxStyle"></editable:EditableDropDownList>
                            </div>
                            <div class="w3-fifth" style="padding: 4px 4px 4px 4px;">
                                <asp:Label CssClass="labelSytle" runat="server" Text="Serial #" Visible="False"></asp:Label><br>
                                <editable:EditableDropDownList Width="90%" runat="server" ID="ddlACSerial" AutoPostBack="true" OnSelectedIndexChanged="ddlACSerial_SelectedIndexChanged" CssClass="w3-input w3-border textboxStyle"></editable:EditableDropDownList>
                            </div>
                        </div>
                        <%-- ---------------------------- --%>
                        <%--<div class="w3-col s2 w3-padding-left" style="border-bottom: 1px solid #EEEEEE">
                            <h5 style="border-left: 6px solid #1e8cdb;">&nbsp;<b> RAM</b></h5>
                        </div>--%>
                        <div class="w3-card-4 w3-round-small w3-white" style="margin-top: 4px; margin-bottom: 10px;">
                            <div class="w3-col s2 w3-padding-left" style="padding: 4px 4px 4px 4px;">
                                <h5 style="border-left: 6px solid #1e8cdb;">&nbsp;<b> RAM</b></h5>
                            </div>
                            <div class="w3-fifth" style="padding: 4px 4px 4px 4px;">
                                <asp:Label CssClass="labelSytle" runat="server" Text="Model" Visible="False"></asp:Label><br>
                                <editable:EditableDropDownList Width="90%" runat="server" ID="ddlRAMldesc" AutoPostBack="true" OnSelectedIndexChanged="ddlRAMldesc_SelectedIndexChanged" CssClass="w3-input w3-border textboxStyle"></editable:EditableDropDownList>
                            </div>
                            <div class="w3-fifth" style="padding: 4px 4px 4px 4px;">
                                <asp:Label CssClass="labelSytle" runat="server" Text="PPE #" Visible="False"></asp:Label><br>
                                <editable:EditableDropDownList Width="90%" runat="server" ID="ddlRAMlppe" AutoPostBack="true" OnSelectedIndexChanged="ddlRAMlppe_SelectedIndexChanged" CssClass="w3-input w3-border textboxStyle"></editable:EditableDropDownList>
                            </div>
                            <div class="w3-fifth" style="padding: 4px 4px 4px 4px;">
                                <asp:Label CssClass="labelSytle" runat="server" Text="Asset #" Visible="False"></asp:Label><br>
                                <editable:EditableDropDownList Width="90%" runat="server" ID="ddlRAMlasset" AutoPostBack="true" OnSelectedIndexChanged="ddlRAMlasset_SelectedIndexChanged" CssClass="w3-input w3-border textboxStyle"></editable:EditableDropDownList>
                            </div>
                            <div class="w3-fifth" style="padding: 4px 4px 4px 4px;">
                                <asp:Label CssClass="labelSytle" runat="server" Text="Serial #" Visible="False"></asp:Label><br>
                                <editable:EditableDropDownList Width="90%" runat="server" ID="ddlRAMlserial" AutoPostBack="true" OnSelectedIndexChanged="ddlRAMlserial_SelectedIndexChanged" CssClass="w3-input w3-border textboxStyle"></editable:EditableDropDownList>
                            </div>
                        </div>
                    </div>
                    <div class="w3-row" style="margin-top: 4px; margin-bottom: 5px;">
                        <div id="dvAdditional" runat="server" visible="false" style="margin-top: 10px;">
                            <asp:Button runat="server" ID="btnAdditional" Text="Additional Equipments" OnClick="btnAdditional_Click" CssClass="w3-button w3-border" Font-Size="12px" Font-Bold="true" BackColor="#1e8cdb" ForeColor="White" />
                        </div>
                    </div>
                </div>
            </div>
            <div class="w3-row" runat="server" id="dvGVadditional" visible="false" style="margin-bottom: 4px;">
                <div class="w3-row w3-padding">
                    <div class="w3-col s12 w3-padding-left" style="border-bottom: 1px solid #EEEEEE">
                        <h5 style="border-left: 6px solid #1e8cdb;">&nbsp;<b> Additional Equipments</b></h5>
                    </div>
                    <div class="w3-third" style="padding: 4px 4px 4px 4px;">
                        <asp:Label runat="server" Text="Equipment Type" CssClass="labelSytle"></asp:Label>
                        <editable:EditableDropDownList Width="90%" runat="server" ID="ddlType2" AutoPostBack="true" OnSelectedIndexChanged="ddlType2_SelectedIndexChanged" CssClass="w3-input w3-border textboxStyle">
                        </editable:EditableDropDownList>
                    </div>
                    <div class="w3-third" style="padding: 4px 4px 4px 4px;">
                        <asp:Label runat="server" Text="Model" CssClass="labelSytle"></asp:Label>
                        <editable:EditableDropDownList Width="90%" runat="server" ID="ddlDesc2" AutoPostBack="true" OnSelectedIndexChanged="ddlDesc2_SelectedIndexChanged" CssClass="w3-input w3-border textboxStyle">
                            <asp:ListItem Value="0">---</asp:ListItem>
                        </editable:EditableDropDownList>
                    </div>
                    <div class="w3-third" style="padding: 4px 4px 4px 4px;">
                        <asp:Label runat="server" Text="Search ID/ Model/ PPE/ Asset/ Serial" CssClass="labelSytle"></asp:Label>
                        <asp:TextBox ID="txtSearch" runat="server" CssClass="w3-input w3-border textboxStyle" Width="100%" Height="35px" AutoPostBack="true" OnTextChanged="txtSearch_TextChanged"></asp:TextBox>
                    </div>

                    <div class="w3-row w3-padding">
                        <div class="w3-row w3-padding-0 w3-padding-left w3-padding-right">
                            <asp:GridView ID="gvAssignEquip" runat="server" AutoGenerateColumns="false" BorderStyle="None" GridLines="both" OnPageIndexChanging="gvAssignEquip_PageIndexChanging"
                                OnSelectedIndexChanged="gvAssignEquip_SelectedIndexChanged" OnRowDataBound="gvAssignEquip_RowDataBound"
                                AllowPaging="true" PageSize="10" CellPadding="5" Font-Size="14px" Width="100%">
                                <RowStyle CssClass="RowStyle" />
                                <Columns>
                                    <asp:BoundField HeaderText="ID" DataField="Equipment_ID" />
                                    <asp:BoundField HeaderText="PPE" DataField="Equipment_PPE_Number" />
                                    <asp:BoundField HeaderText="Serial" DataField="Equipment_Serial" />
                                    <asp:BoundField HeaderText="Asset" DataField="Equipment_Asset_Number" />
                                </Columns>
                                <EmptyDataTemplate>
                                    No Record Available
                                </EmptyDataTemplate>
                            </asp:GridView>
                        </div>
                    </div>

                    <div class="w3-col s12 w3-padding-left" style="border-bottom: 1px solid #EEEEEE">
                        <h5 style="border-left: 6px solid #1e8cdb;">&nbsp;<b> Selected Equipments</b></h5>
                    </div>

                    <div class="w3-row w3-padding">
                        <div class="w3-row w3-padding-0 w3-padding-left w3-padding-right">
                            <asp:GridView ID="gvEqpList" runat="server" BorderStyle="None" GridLines="both"
                                OnSelectedIndexChanged="gvEqpList_SelectedIndexChanged" OnRowDataBound="gvEqpList_RowDataBound"
                                CellPadding="5" Font-Size="14px" Width="100%">
                                <RowStyle CssClass="RowStyle" />
                                <EmptyDataTemplate>
                                    No Record Available
                                </EmptyDataTemplate>
                            </asp:GridView>
                        </div>
                    </div>
                </div>
            </div>
            <div class="w3-row" style="padding-left: 10px; padding-right: 10px; padding-bottom: 10px;">
                <asp:Button runat="server" ID="btnSave" Text="Save" OnClick="btnSave_Click" CssClass="w3-button w3-border" Font-Size="12px" Font-Bold="true" BackColor="#1e8cdb" ForeColor="White" />
                <asp:Button runat="server" ID="btnCancel" Text="Cancel" OnClick="btnCancel_Click" CssClass="w3-button w3-border" Font-Size="12px" Font-Bold="true" BackColor="#1e8cdb" ForeColor="White" />
                <asp:Button runat="server" Visible="false" ID="Button1" Text="sa" OnClick="Button1_Click" CssClass="w3-button w3-border" Font-Size="12px" Font-Bold="true" BackColor="#1e8cdb" ForeColor="White" />
            </div>
        </div>
    </div>
    <%-- </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnSave" />
        </Triggers>
    </asp:UpdatePanel>--%>


    <div id="idMesBox" class="w3-modal w3-round" runat="server" visible="false" style="z-index: 1400; background-color: white; display: block;">
        <div class="w3-modal-content w3-animate-top w3-card-4 w3-round">
            <%--<asp:UpdatePanel ID="updPnl1" runat="server" UpdateMode="Conditional">
                <ContentTemplate>--%>
            <header class="w3-text-white w3-padding-left" style="background-color: #002050;">
                <h3>Print</h3>
            </header>
            <asp:Label runat="server" CssClass="labelSytle" Text="Do you want to print?"></asp:Label>
            <asp:Button runat="server" ID="btnYes" Text="Yes" OnClick="btnYes_Click" CssClass="w3-button w3-border" Font-Size="12px" Font-Bold="true" BackColor="#1e8cdb" ForeColor="White" />
            <asp:Button runat="server" ID="btnNo" Text="No" OnClick="btnNo_Click" CssClass="w3-button w3-border" Font-Size="12px" Font-Bold="true" BackColor="#1e8cdb" ForeColor="White" />

            <footer class="w3-container w3-light-gray">
                <p>&nbsp;</p>
            </footer>
            <%-- </ContentTemplate>
            </asp:UpdatePanel>--%>
        </div>
    </div>

    <div class="w3-container">
        <asp:Panel ID="Panel1" runat="server" Visible="false">
            <asp:GridView runat="server" ID="gvView" AutoGenerateColumns="true" BorderStyle="None" GridLines="Horizontal" Width="100%">
                <RowStyle CssClass="RowStyle" />
                <HeaderStyle BackColor="#1e8cdb" CssClass="l9" ForeColor="White" />
                <EmptyDataTemplate>
                    <div style="text-align: center;">
                        <label>...No Record found...</label>
                    </div>
                </EmptyDataTemplate>
            </asp:GridView>

            <%--<asp:LinkButton runat="server" OnClick="btnBack_Click" Text="Back" ID="btnBack"></asp:LinkButton>--%>
        </asp:Panel>
    </div>

    <%-- <script type="text/javascript">
        function openMsgBox() {
            document.getElementById('idMesBox').style.display = 'block';
        }

        function closeMsgBox() {
            document.getElementById('idMesBox').style.display = 'none';
        }
    </script>--%>
</asp:Content>


