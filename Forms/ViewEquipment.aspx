<%@ Page Title="Equipment - View" Language="vb"
    AutoEventWireup="false"
    CodeBehind="ViewEquipment.aspx.vb"
    Inherits="InventorySystem.ViewEqiupment"
    EnableEventValidation="false"
    EnableSessionState="True"
    MasterPageFile="~/Forms/pmInventory.Master" %>

<%@ MasterType VirtualPath="~/Forms/pmInventory.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Equipment - View</title>
    <link rel="stylesheet" href="/CSS/w3.css" />
    <style>
        .labelSytle {
            font-size: 12px;
            font-weight: 700;
        }

        .imageStyle {
            max-width: 100%;
            height: auto;
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
    <asp:Label runat="server" ID="lblID" Visible="false" CssClass="labelSytle"></asp:Label>
    <div class="w3-container" style="margin-left: 40px; margin-right: 40px; margin-top: 80px; overflow: auto">
        <div class="w3-card-4 w3-round-small w3-white" style="margin-top: 4px; padding: 10px 10px 10px 10px; border-bottom: 1px solid #EEEEEE; margin-bottom: 5px;">
            <asp:Button runat="server" ID="btnBack" Text="Back" OnClick="btnBack_Click" CssClass="w3-button w3-border" Font-Size="12px" Font-Bold="true" BackColor="#1e8cdb" ForeColor="White" />
            <asp:Button runat="server" ID="btnEdit" Text="Edit" OnClick="btnEdit_Click" CssClass="w3-button w3-border" Font-Size="12px" Font-Bold="true" BackColor="#1e8cdb" ForeColor="White" />
            <asp:Button runat="server" ID="btnSave" Text="Save" OnClick="btnSave_Click" Visible="false" CssClass="w3-button w3-border" Font-Size="12px" Font-Bold="true" BackColor="#1e8cdb" ForeColor="White" />
            <asp:Button runat="server" ID="btnCancel" Text="Cancel" OnClick="btnCancel_Click" Visible="false" CssClass="w3-button w3-border" Font-Size="12px" Font-Bold="true" BackColor="#1e8cdb" ForeColor="White" />
            <div class="w3-col s12 w3-padding-left" style="border-bottom: 1px solid #EEEEEE">
                <h5 style="border-left: 6px solid #1e8cdb;">&nbsp;<b> Equipment Details</b></h5>
            </div>
            <div class="w3-row w3-padding" style="margin: 10px 10px 10px 10px;">

                <div class="w3-col w3-third" style="margin-top: 5px; margin-bottom: 5px; padding: 4px 4px 4px 4px;">
                    <%--<div class="w3-third" style="padding: 4px 4px 4px 4px;">--%>
                    <div>
                        <asp:Label runat="server" Text="Assigned To" CssClass="labelSytle"></asp:Label><br />
                        <asp:TextBox runat="server" ID="txtAssignedTo" CssClass="w3-input w3-border textboxStyle"></asp:TextBox>
                    </div>
                    <%--</div>
                    <div class="w3-third" style="padding: 4px 4px 4px 4px;">--%>

                    <%--</div>
                    <div class="w3-third" style="padding: 4px 4px 4px 4px;">--%>
                    <div>
                        <asp:Label runat="server" Text="Date Acquired" CssClass="labelSytle"></asp:Label><br />
                        <asp:TextBox runat="server" ID="txtDateAcquired" CssClass="w3-input w3-border textboxStyle"></asp:TextBox>
                    </div>
                    <div>
                        <asp:Label runat="server" ID="lblBatteryAge" Text="Battery Age" CssClass="labelSytle"></asp:Label><br />
                        <asp:TextBox runat="server" ID="txtBatteryAge" CssClass="w3-input w3-border textboxStyle"></asp:TextBox>
                    </div>
                    <div>
                        <asp:Label runat="server" Text="Asset #" CssClass="labelSytle"></asp:Label><br />
                        <asp:TextBox runat="server" ID="txtItemAsset" CssClass="w3-input w3-border textboxStyle"></asp:TextBox>
                    </div>
                    <div>
                        <asp:Label runat="server" Text="PPE #" CssClass="labelSytle"></asp:Label><br />
                        <asp:TextBox runat="server" ID="txtItemPPE" CssClass="w3-input w3-border textboxStyle"></asp:TextBox>
                    </div>
                    <div>
                        <asp:Label runat="server" Text="Serial #" CssClass="labelSytle"></asp:Label><br />
                        <asp:TextBox runat="server" ID="txtItemSerial" CssClass="w3-input w3-border textboxStyle"></asp:TextBox>
                    </div>
                     <div>
                        <asp:Label runat="server" Text="OS " CssClass="labelSytle"></asp:Label><br />
                        <asp:TextBox runat="server" ID="txtOS" CssClass="w3-input w3-border textboxStyle"></asp:TextBox>
                    </div>

                    <%--</div>--%>
                </div>
                <%-- ----------------------------------------------------- --%>
                <div class="w3-col w3-third" style="margin-top: 5px; margin-bottom: 5px; padding: 4px 4px 4px 4px;">
                    <%--<div class="w3-third" style="padding: 4px 4px 4px 4px;">--%>


                    <%--</div>
                    <div class="w3-third" style="padding: 4px 4px 4px 4px;">--%>
                    <div>
                        <asp:Label runat="server" Text="Type" CssClass="labelSytle"></asp:Label><br />
                        <asp:DropDownList runat="server" ID="ddlType" CssClass="w3-input w3-border textboxStyle">
                        </asp:DropDownList>
                    </div>
                    <div>
                        <asp:Label runat="server" Text="Status" CssClass="labelSytle"></asp:Label><br />
                        <asp:DropDownList runat="server" ID="ddlStatus" AutoPostBack="true" CssClass="w3-input w3-border textboxStyle">
                            <asp:ListItem></asp:ListItem>
                            <asp:ListItem>On Stock</asp:ListItem>
                            <asp:ListItem>Assigned</asp:ListItem>
                            <asp:ListItem>Defective</asp:ListItem>
                            <asp:ListItem>For Disposal</asp:ListItem>
                            <asp:ListItem>For Repair</asp:ListItem>
                        </asp:DropDownList>
                    </div>

                    <!--<div>
                        <asp:Label runat="server" Text="Replacement" ID="lblReplacement" AutoPostBack="true" CssClass="labelSytle"></asp:Label><br />
                        <asp:DropDownList runat="server" ID="ddlReplacement" CssClass="w3-input w3-border textboxStyle"></asp:DropDownList>
                    </div>-->
                    
                    <div>
                        <asp:Label CssClass="labelSytle" runat="server" Text="Location"></asp:Label>
                        <asp:DropDownList runat="server" ID="ddlLocation" CssClass="w3-input w3-border textboxStyle" >
                            <asp:ListItem>---</asp:ListItem>
                            <asp:ListItem>ELVDI</asp:ListItem>
                            <asp:ListItem>ITSDI</asp:ListItem>
                            <asp:ListItem>ITSG</asp:ListItem>
                            <asp:ListItem>WSI MAKATI</asp:ListItem>
                            <asp:ListItem>WSI CEBU</asp:ListItem>
                            <asp:ListItem>WSI DAVAO</asp:ListItem>
                            <asp:ListItem>WAREHOUSE</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <div>
                        <asp:Label runat="server" Text="RFT #" CssClass="labelSytle"></asp:Label><br />
                        <asp:TextBox runat="server" ID="txtRFTnum" CssClass="w3-input w3-border textboxStyle"></asp:TextBox>
                    </div>
                    <div>
                        <asp:Label runat="server" Text="RFT Date" CssClass="labelSytle"></asp:Label><br />
                        <asp:TextBox runat="server" ID="txtRFTdate" CssClass="w3-input w3-border textboxStyle"></asp:TextBox>
                    </div>
                     <div>
                        <asp:Label runat="server" Text="Host Name" CssClass="labelSytle"></asp:Label><br />
                        <asp:TextBox runat="server" ID="txtHostName" CssClass="w3-input w3-border textboxStyle"></asp:TextBox>
                    </div>

                    <%--</div>
                    <div class="w3-third" style="padding: 4px 4px 4px 4px;">--%>

                    <%--</div>--%>
                </div>
                <%-- ----------------------------------------------------- --%>
                <%--<div class="w3-col w3-third" style="margin-top: 5px; margin-bottom: 5px;">--%>
                <%--<div class="w3-third" style="padding: 4px 4px 4px 4px;">--%>

                <%--</div>
                    <div class="w3-third" style="padding: 4px 4px 4px 4px;">--%>

                <%--</div>
                    <div class="w3-third" style="padding: 4px 4px 4px 4px;">--%>

                <%-- </div>
                </div>--%>
                <%-- ----------------------------------------------------- --%>
                <div class="w3-col w3-third" style="margin-top: 5px; margin-bottom: 5px; padding: 4px 4px 4px 4px;">
                    <%--<div class="w3-third" style="padding: 4px 4px 4px 4px;">--%>

                    <%--</div>
                    <div class="w3-third" style="padding: 4px 4px 4px 4px;">--%>

                    <%--</div>
                    <div class="w3-third" style="padding: 4px 4px 4px 4px;">--%>
                    <div>
                        <asp:Label runat="server" Text="Description" CssClass="labelSytle"></asp:Label><br />
                        <asp:TextBox runat="server" ID="txtItemDesc" CssClass="w3-input w3-border textboxStyle"></asp:TextBox>
                    </div>
                    <div runat="server" id="dvHasPic" visible="false">
                        <asp:Label runat="server" Text="Equipment Picture" CssClass="labelSytle"></asp:Label><br />
                        <asp:Image runat="server" ID="imgEqp" ImageAlign="Middle" CssClass="imageStyle" />
                    </div>
                    <div runat="server" id="dvNoPic" visible="false">
                        <asp:Label runat="server" Text="Upload Picture" CssClass="labelSytle"></asp:Label><br />
                        <asp:FileUpload runat="server" ID="fUpload" />
                    </div>
                    <%--</div>--%>
                </div>
                <div class="w3-row" style="border-bottom: 1px solid #EEEEEE; margin-top: 5px; margin-bottom: 5px; width: 100%">
                    <asp:Label runat="server" Text="Remarks : " CssClass="labelSytle"></asp:Label><br />
                    <asp:TextBox runat="server" ID="txtRemarks" TextMode="MultiLine" Width="100%" CssClass="w3-input w3-border textboxStyle"></asp:TextBox>
                </div>
            </div>

            <div class="w3-col s12 w3-padding-left" style="border-bottom: 1px solid #EEEEEE">
                <h5 style="border-left: 6px solid #1e8cdb;">&nbsp;<b> Equipment History</b></h5>
            </div>
            <div class="w3-row" style="margin: 10px 10px 10px 10px;">
                <div class="w3-row w3-padding">
                    <asp:GridView runat="server" ID="gvHistory" OnRowDataBound="gvHistory_RowDataBound" BorderStyle="None" BorderColor="White" Width="100%">
                        <RowStyle CssClass="RowStyle" />
                        <HeaderStyle BackColor="#1e8cdb" CssClass="l9" ForeColor="White" />
                        <EmptyDataTemplate>
                            <div style="text-align: center;">
                                <label>...No Record found...</label>
                            </div>
                        </EmptyDataTemplate>
                    </asp:GridView>
                </div>
            </div>
        </div>
    </div>

</asp:Content>
