<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Forms/pmInventory.Master" Inherits="InventorySystem.ForRepair" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
        <title>History</title>
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
<asp:Content ID="wew" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <div class="w3-container" style="margin-left: 40px; margin-right: 40px; margin-top: 80px; overflow: auto">
        <div class="w3-card-4 w3-round-small w3-white" style="margin-top: 4px; margin-bottom: 10px;">
            <div class="w3-row w3-padding">
                <div class="w3-row w3-padding">
                    <div class="w3-left">
                        <asp:Table runat="server">
                            <asp:TableRow>
                                <asp:TableCell Width="70%">
                                    <asp:TextBox ID="txtBoxSearchRepair" class="" runat="server" CssClass="w3-input w3-border textboxStyle"></asp:TextBox>
                                </asp:TableCell>
                                <asp:TableCell>
                                    <asp:Button ID="btnSearchRepair" runat="server" Text="Search Name" CssClass="w3-button w3-border" Font-Size="12px" Font-Bold="true" BackColor="#1e8cdb" ForeColor="White" />
                                </asp:TableCell>
                            </asp:TableRow>
                        </asp:Table>
                    </div>
                    <div class="w3-right" style="z-index: 5;">
                    </div>
                </div>

                <div>
                    <div class="w3-row w3-padding">
                       
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ForRepairForm" runat="server">
</asp:Content>
