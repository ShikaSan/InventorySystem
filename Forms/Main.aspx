<%@ Page Title="ITSG Inventory Home" Language="vb"
    AutoEventWireup="false"
    CodeBehind="Main.aspx.vb"
    Inherits="InventorySystem.Main"
    EnableEventValidation="false"
    EnableSessionState="True"
    MasterPageFile="~/Forms/pmInventory.Master" %>

<%@ MasterType VirtualPath="~/Forms/pmInventory.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>ITSG Inventory Home</title>
    <link rel="stylesheet" href="../CSS/w3.css" />
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

            <%--<div class="w3-card-4 w3-round-small w3-white" style="margin-top: 4px; margin-bottom: 10px;">--%>
            <div class="w3-row w3-padding">
                <div class="w3-row" style="margin-top: 5px; margin-bottom: 5px;">
                    <asp:Button runat="server" Text="Add Equipment" ID="btnAdd" OnClick="btnAdd_Click" CssClass="w3-button w3-border" Font-Size="12px" Font-Bold="true" BackColor="#1e8cdb" ForeColor="White" />
                </div>
                <div class="w3-row" style="border-bottom: 1px solid #EEEEEE; margin-top: 5px; margin-bottom: 5px;">
                    <div class="w3-third" style="padding: 4px 4px 4px 4px;">
                        <asp:Table runat="server" Width="100%">
                            <asp:TableRow Width="100%">
                                <asp:TableCell Width="100%">
                            <asp:Label runat="server" Text="Status" CssClass="labelSytle"></asp:Label>
                                </asp:TableCell>
                            </asp:TableRow>
                            <asp:TableRow Width="100%">
                                <asp:TableCell Width="100%">
                                    <asp:DropDownList runat="server" ID="ddlStatus" AutoPostBack="true" OnSelectedIndexChanged="ddlStatus_SelectedIndexChanged" CssClass="w3-input w3-border textboxStyle">
                                    </asp:DropDownList>
                                </asp:TableCell>
                            </asp:TableRow>
                        </asp:Table>
                    </div>
                    <div class="w3-third" style="padding: 4px 4px 4px 4px;">
                        <asp:Table runat="server" Width="100%">
                            <asp:TableRow Width="100%">
                                <asp:TableCell Width="100%">
                                        <asp:Label runat="server" Text="Equipment Type" CssClass="labelSytle"></asp:Label>
                                </asp:TableCell>
                            </asp:TableRow>
                            <asp:TableRow Width="100%">
                                <asp:TableCell Width="100%">
                                    <asp:DropDownList runat="server" ID="ddlType" AutoPostBack="true" OnSelectedIndexChanged="ddlType_SelectedIndexChanged" CssClass="w3-input w3-border textboxStyle">
                                    </asp:DropDownList>
                                </asp:TableCell>
                            </asp:TableRow>
                        </asp:Table>
                    </div>
                    <div class="w3-third" style="padding: 4px 4px 4px 4px;">

                        <asp:Table runat="server">
                            <asp:TableRow>
                                <asp:TableCell Width="100%">
                                        <asp:Label runat="server" Text="Search ID/ Description/ PPE/ Asset/ Serial" CssClass="labelSytle" ></asp:Label>
                                </asp:TableCell>
                            </asp:TableRow>
                            <asp:TableRow>
                                <asp:TableCell Width="75%">
                                    <asp:TextBox ID="txtSearch" runat="server" CssClass="w3-input w3-border textboxStyle" Width="100%" Height="35px"></asp:TextBox>
                                </asp:TableCell>
                                <asp:TableCell Width="25%">
                                    <asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="btnSearch_Click" CssClass="w3-button w3-border" Height="35px" Font-Size="12px" Font-Bold="true" BackColor="#1e8cdb" ForeColor="White" />
                                </asp:TableCell>
                            </asp:TableRow>
                        </asp:Table>
                    </div>
                </div>
                <%--</div>--%>

                <%--<div class="w3-card-4 w3-round-small w3-white" style="margin-top: 4px; margin-bottom: 10px;">--%>
                <asp:GridView runat="server" ID="gvView" OnSelectedIndexChanged="gvView_SelectedIndexChanged" OnPageIndexChanging="gvView_PageIndexChanging"
                    OnRowDataBound="gvView_RowDataBound" AllowPaging="true" PageSize="10" AutoGenerateColumns="true" BorderStyle="None" GridLines="Horizontal"
                    CellPadding="5" Font-Size="14px" Width="100%" AllowSorting="true" OnSorting="gvView_Sorting">
                    <RowStyle CssClass="RowStyle" />
                    <%--<HeaderStyle BackColor="#1e8cdb" CssClass="l9" ForeColor="White" />--%>
                    <EmptyDataTemplate>
                        <div style="text-align: center;">
                            <label>...No Record found...</label>
                        </div>
                    </EmptyDataTemplate>
                </asp:GridView>
            </div>
            <%--</div>--%>
        </div>
    </div>
</asp:Content>
