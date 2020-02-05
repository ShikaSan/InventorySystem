<%@ Page Title="Equipment - Add" Language="vb"
    AutoEventWireup="false"
    CodeBehind="AddEquipment.aspx.vb"
    Inherits="InventorySystem.AddEquipment"
    EnableEventValidation="false"
    EnableSessionState="True"
    MasterPageFile="~/Forms/pmInventory.Master" %>
<%@ MasterType VirtualPath="~/Forms/pmInventory.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Equipment - Add</title>
    <link rel="stylesheet" href="/CSS/w3.css" />
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
    <div class="w3-container" style="margin-left: 40px; margin-right: 40px; margin-top: 80px; overflow: auto">
        <div class="w3-card-4 w3-round-small w3-white" style="margin-top: 4px; margin-bottom: 10px;">
            <div class="w3-row w3-padding">
                <div class="w3-col s12 w3-padding-left" style="border-bottom: 1px solid #EEEEEE">
                    <h5 style="border-left: 10px solid #1e8cdb;">&nbsp;Add Unit</h5>
                </div>
                <div class="w3-row" style="border-bottom: 1px solid #EEEEEE; margin-top: 5px; margin-bottom: 5px;">
                    <div class="w3-third" style="padding: 4px 4px 4px 4px;">
                        <div>
                            <asp:Label runat="server" Text="Type" CssClass="labelSytle"></asp:Label><br />
                            <asp:DropDownList runat="server" ID="ddlType" CssClass="w3-input w3-border textboxStyle">
                            </asp:DropDownList>
                        </div>
                    </div>
                <%--    <table  border="0" style="padding:4px 4px 4px 4px;margin:0px 0px 0px 0px;"></table>--%>
                    <div class="w3-third" style="padding: 4px 4px 4px 4px;">
                        <div>
                            <asp:Label runat="server" Text="Description" CssClass="labelSytle"></asp:Label><br />
                            <asp:TextBox runat="server" ID="txtItemDesc" CssClass="w3-input w3-border textboxStyle"></asp:TextBox>
                        </div>
                    </div>
                    <div class="w3-third" style="padding: 4px 4px 4px 4px;">
                        <div>
                            <asp:Label runat="server" Text="Date Acquired" CssClass="labelSytle"></asp:Label><br />
                            <asp:TextBox runat="server" ID="txtDateAcquired" CssClass="w3-input w3-border textboxStyle" TextMode="Date"></asp:TextBox>                            
                        </div>
                    </div>
                </div>
                <%-- ----------------------------------------------------- --%>
                <div class="w3-row" style="border-bottom: 1px solid #EEEEEE; margin-top: 5px; margin-bottom: 5px;">
                    <div class="w3-third" style="padding: 4px 4px 4px 4px;">
                        <div>
                            <asp:Label runat="server" Text="PPE #" CssClass="labelSytle"></asp:Label><br />
                            <asp:TextBox runat="server" ID="txtItemPPE" CssClass="w3-input w3-border textboxStyle"></asp:TextBox>
                        </div>
                    </div>
                    <div class="w3-third" style="padding: 4px 4px 4px 4px;">
                        <div>
                            <asp:Label runat="server" Text="Asset #" CssClass="labelSytle"></asp:Label><br />
                            <asp:TextBox runat="server" ID="txtItemAsset" CssClass="w3-input w3-border textboxStyle"></asp:TextBox>
                        </div>
                    </div>
                    <div class="w3-third" style="padding: 4px 4px 4px 4px;">
                        <div>
                            <asp:Label runat="server" Text="Serial #" CssClass="labelSytle"></asp:Label><br />
                            <asp:TextBox runat="server" ID="txtItemSerial" CssClass="w3-input w3-border textboxStyle"></asp:TextBox>
                        </div>
                    </div>
                </div>
                <%-- ----------------------------------------------------- --%>
                <div class="w3-row" style="border-bottom: 1px solid #EEEEEE; margin-top: 5px; margin-bottom: 5px;">
                    <%--<div class="w3-third" style="padding: 4px 4px 4px 4px;">
                        <div>
                            <asp:Label runat="server" Text="RFT #" CssClass="labelSytle"></asp:Label><br />
                            <asp:TextBox runat="server" ID="txtRFTnum" CssClass="w3-input w3-border textboxStyle"></asp:TextBox>
                        </div>
                    </div>
                    <div class="w3-third" style="padding: 4px 4px 4px 4px;">
                        <div>
                            <asp:Label runat="server" Text="RFT Date" CssClass="labelSytle"></asp:Label><br />
                            <asp:TextBox runat="server" ID="txtRFTdate" CssClass="w3-input w3-border textboxStyle"></asp:TextBox>
                        </div>
                    </div>--%>
                     <div class="w3-third" style="padding: 4px 4px 4px 4px;">
                        <div>
                            <asp:Label runat="server" Text="Upload Picture" CssClass="labelSytle"></asp:Label><br />
                            <asp:FileUpload runat="server" ID="fUpload" />
                        </div>
                    </div>
                </div>
                <%-- ----------------------------------------------------- --%>
                <%--<div class="w3-row" style="border-bottom: 1px solid #EEEEEE; margin-top: 5px; margin-bottom: 5px;">
                    <div class="w3-third" style="padding: 4px 4px 4px 4px;">
                        <div>
                            <asp:Label runat="server" Text="Status" CssClass="labelSytle"></asp:Label><br />
                            <asp:TextBox runat="server" ID="txtStatus" CssClass="w3-input w3-border textboxStyle" Text="On Stock"></asp:TextBox>
                        </div>
                    </div>
                    <div class="w3-third" style="padding: 4px 4px 4px 4px;">
                        <div>
                            <asp:Label runat="server" Text="Assigned To" CssClass="labelSytle"></asp:Label><br />
                            <asp:TextBox runat="server" ID="txtAssignedTo" CssClass="w3-input w3-border textboxStyle" Text="ISG"></asp:TextBox>
                        </div>
                    </div>
                    <div class="w3-third" style="padding: 4px 4px 4px 4px;">
                        <div>
                        </div>
                    </div>
                </div>--%>
                <div class="w3-row" style="border-bottom: 1px solid #EEEEEE; margin-top: 5px; margin-bottom: 5px; width: 100%">
                    <asp:Label runat="server" Text="Remarks : " CssClass="labelSytle"></asp:Label><br />
                    <asp:TextBox runat="server" ID="txtRemarks" TextMode="MultiLine" Width="100%" CssClass="w3-input w3-border textboxStyle"></asp:TextBox>
                </div>
            </div>
            <table>
                <tr>
                    <td>
                        <asp:Button runat="server" Text="Save" ID="btnSave" OnClick="btnSave_Click" CssClass="w3-button w3-border" Font-Size="12px" Font-Bold="true" BackColor="#1e8cdb" ForeColor="White" /><br />
                    </td>
                    <td>
                        <asp:Button runat="server" Text="Cancel" ID="btnCancel" OnClick="btnCancel_Click" CssClass="w3-button w3-border" Font-Size="12px" Font-Bold="true" BackColor="#1e8cdb" ForeColor="White" />
                    </td>
                </tr>
            </table>
        </div>
    </div>
</asp:Content>
