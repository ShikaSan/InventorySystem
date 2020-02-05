<%@ Page Title="History" Language="vb"
    AutoEventWireup="false"
    CodeBehind="History.aspx.vb"
    Inherits="InventorySystem.History"
    EnableEventValidation="false"
    EnableSessionState="True"
    MasterPageFile="~/Forms/pmInventory.Master" %>

<%@ MasterType VirtualPath="~/Forms/pmInventory.Master" %>

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

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <div class="w3-container" style="margin-left: 40px; margin-right: 40px; margin-top: 80px; overflow: auto">
        <div class="w3-card-4 w3-round-small w3-white" style="margin-top: 4px; margin-bottom: 10px;">
            <div class="w3-row w3-padding">
                <div class="w3-row w3-padding">
                    <div class="w3-left">
                        <asp:Table runat="server">
                            <asp:TableRow>
                                <asp:TableCell Width="70%">
                                    <asp:TextBox ID="txtSearch" runat="server" CssClass="w3-input w3-border textboxStyle"></asp:TextBox>
                                </asp:TableCell>
                                <asp:TableCell>
                                    <asp:Button ID="btnSearch" runat="server" Text="Search Name" OnClick="btnSearch_Click" CssClass="w3-button w3-border" Font-Size="12px" Font-Bold="true" BackColor="#1e8cdb" ForeColor="White" />
                                </asp:TableCell>
                            </asp:TableRow>
                        </asp:Table>
                    </div>
                    <div class="w3-right" style="z-index: 5;">
                        <asp:Button runat="server" ID="btnEquipHistory" Text="Equipment History" OnClick="btnEquipHistory_Click" CssClass="w3-button w3-border" Font-Size="12px" Font-Bold="true" BackColor="#1e8cdb" ForeColor="White"  />
                        <asp:Button runat="server" ID="btnUserHistory" Text="User History" OnClick="btnUserHistory_Click" CssClass="w3-button w3-border" Font-Size="12px" Font-Bold="true" BackColor="#1e8cdb" ForeColor="White"  />
                    </div>
                </div>

                <div>
                    <div class="w3-row w3-padding">
                        <asp:GridView runat="server" ID="gvHistory" OnSelectedIndexChanged="gvHistory_SelectedIndexChanged" OnPageIndexChanging="gvHistory_PageIndexChanging"
                            OnRowDataBound="gvHistory_RowDataBound" AllowPaging="true" PageSize="10" BorderStyle="None" GridLines="Horizontal"
                            CellPadding="5" Font-Size="14px" Width="100%">
                            <RowStyle CssClass="RowStyle" />
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
    </div>
</asp:Content>
