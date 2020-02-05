<%@ Page Title="User Accountability" Language="vb"
    AutoEventWireup="false"
    CodeBehind="UserAccountability.aspx.vb"
    Inherits="InventorySystem.UserAccountability"
    EnableEventValidation="false"
    EnableSessionState="True"
    MasterPageFile="~/Forms/pmInventory.Master" %>

<%@ MasterType VirtualPath="~/Forms/pmInventory.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>User Accountability</title>
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
    <%--<asp:ScriptManager runat="server" ID="SM1"></asp:ScriptManager>--%>
    <div class="w3-container" style="margin-left: 40px; margin-right: 40px; margin-top: 80px; overflow: auto">
        <div class="w3-card-4 w3-round-small w3-white" style="margin-top: 4px; margin-bottom: 10px;">
            <div class="w3-row w3-padding">
                <div class="w3-row" style="margin-top: 5px; margin-bottom: 5px;">
                    <asp:Button runat="server" ID="btnAddAcc" OnClick="btnAddAcc_Click" Text="Assign Equipment" CssClass="w3-button w3-border" Font-Size="12px" Font-Bold="true" BackColor="#1e8cdb" ForeColor="White" />
                    <asp:Button runat="server" ID="btnPrintAcc" OnClick="btnPrintAcc_Click" Text="Print Accountability" CssClass="w3-button w3-border" Font-Size="12px" Font-Bold="true" BackColor="#1e8cdb" ForeColor="White" />
                </div>
                <div class="w3-row" style="margin-top: 5px; margin-bottom: 5px;">
                    <div class="w3-third" style="padding: 4px 4px 4px 4px;">
                        <asp:Label runat="server" Text="Search Name" CssClass="labelSytle"></asp:Label>
                        <asp:TextBox runat="server" ID="txtSearch" AutoPostBack="true" CssClass="w3-input w3-border textboxStyle" OnTextChanged="txtSearch_TextChanged"></asp:TextBox>
                    </div>
                    <div class="w3-third" style="padding: 4px 4px 4px 4px;">
                        <asp:Label CssClass="labelSytle" runat="server" Text="Department : "></asp:Label>
                        <%--<asp:DropDownList runat="server" ID="ddlDepartment" AutoPostBack="true" CssClass="w3-input w3-border textboxStyle" OnSelectedIndexChanged="ddlDepartment_SelectedIndexChanged"></asp:DropDownList>--%>
                    <asp:TextBox runat="server" ID="txtDepartment" AutoPostBack="true" CssClass="w3-input w3-border textboxStyle" OnTextChanged="txtDepartment_TextChanged"></asp:TextBox>
                    </div>
                    <div class="w3-third" style="padding: 4px 4px 4px 4px;">
                        <asp:Label CssClass="labelSytle" runat="server" Text="Position : "></asp:Label>
                        <asp:DropDownList runat="server" ID="ddlPosition" AutoPostBack="true" CssClass="w3-input w3-border textboxStyle" OnSelectedIndexChanged="ddlPosition_SelectedIndexChanged"></asp:DropDownList>
                    </div>
                </div>

                <div class="w3-row w3-padding">
                    <div class="w3-row w3-padding-0 w3-padding-left w3-padding-right">
                        <asp:GridView ID="gvGetAccList" runat="server" AllowPaging="true" PageSize="10" OnSelectedIndexChanged="gvGetAccList_SelectedIndexChanged"
                            OnRowDataBound="gvGetAccList_RowDataBound" BorderStyle="None" GridLines="Horizontal" OnPageIndexChanging="gvGetAccList_PageIndexChanging"
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

    <div id="dvPrintAcc" class="w3-modal w3-round" runat="server" visible="false" style="z-index: 1400; background-color: white; display: block;">
        <div class="w3-modal-content w3-card-4 w3-round">
            <%--<asp:UpdatePanel ID="updPnl1" runat="server" UpdateMode="Conditional">
                <ContentTemplate>--%>
                    <header class="w3-text-white w3-padding-left" style="background-color: #002050;">
                        <h3>&nbsp&nbsp Print Accountability</h3>
                    </header>
                    <div class="w3-row">
                        <div class="w3-half w3-padding" style="border-right: 2px solid black">
                            <asp:Label runat="server" CssClass="labelSytle" Text="Select people : "></asp:Label>
                            <asp:GridView ID="gvNameList" runat="server" AutoGenerateColumns="false" BorderStyle="None" GridLines="both"
                                OnPageIndexChanging="gvNameList_PageIndexChanging"
                                OnSelectedIndexChanged="gvNameList_SelectedIndexChanged"
                                OnRowDataBound="gvNameList_RowDataBound" 
                                AllowPaging="true" PageSize="10" CellPadding="5" Font-Size="14px" Width="100%">
                                <RowStyle CssClass="RowStyle" />
                                <Columns>
                                    <asp:BoundField DataField="id" />
                                    <asp:BoundField DataField="Assigned_To" HeaderText="Name" />
                                </Columns>
                                <EmptyDataTemplate>
                                    No Record Available
                                </EmptyDataTemplate>
                            </asp:GridView>
                        </div>
                        <div class="w3-half w3-padding" style="border-left: 2px solid black">
                            <asp:Label runat="server" CssClass="labelSytle" Text="Selected people : "></asp:Label>
                            <asp:GridView ID="gvSelectedList" runat="server" BorderStyle="None" GridLines="both"
                                OnSelectedIndexChanged="gvSelectedList_SelectedIndexChanged" 
                                OnRowDataBound="gvSelectedList_RowDataBound"
                                CellPadding="5" Font-Size="14px" Width="100%">
                                <RowStyle CssClass="RowStyle" />
                                <EmptyDataTemplate>
                                    No Record Available
                                </EmptyDataTemplate>
                            </asp:GridView>
                        </div>
                    </div>
                    <div class="w3-row w3-padding">
                        <asp:Button runat="server" ID="btnPrintAll" Text="Print All" OnClick="btnPrintAll_Click" CssClass="w3-button w3-border" Font-Size="12px" Font-Bold="true" BackColor="#1e8cdb" ForeColor="White" />
                        <asp:Button runat="server" ID="btnPrintSelected" Text="Print Selected" OnClick="btnPrintSelected_Click" CssClass="w3-button w3-border" Font-Size="12px" Font-Bold="true" BackColor="#1e8cdb" ForeColor="White" />
                        <asp:Button runat="server" ID="btnCancel" Text="Cancel" OnClick="btnCancel_Click" CssClass="w3-button w3-border" Font-Size="12px" Font-Bold="true" BackColor="#1e8cdb" ForeColor="White" />
                    </div>
                    <footer class="w3-container w3-light-gray">
                        <p>&nbsp;</p>
                    </footer>
                <%--</ContentTemplate>
            </asp:UpdatePanel>--%>
        </div>
    </div>

    <div id="dvNotif" class="w3-modal w3-round" runat="server" visible="false" style="z-index: 1400; background-color: white; display: block;">
        <div class="w3-modal-content w3-card-4 w3-round">
            <%--<asp:UpdatePanel ID="updPnl1" runat="server" UpdateMode="Conditional">
                <ContentTemplate>--%>
                    <header class="w3-text-white w3-padding-left" style="background-color: #002050;">
                        <h3>&nbsp&nbsp Success!</h3>
                    </header>
                    <div class="w3-row">
                        <p>File Saved! Please Click <asp:LinkButton runat="server" Text="here" ID="lnkbtn"></asp:LinkButton> to go to the file!</p>
                        <a runat="server" Text="Here" ID="lnkPath" >Here</a>
                        <br />
                        <asp:Label runat="server" ID="test"></asp:Label>
                        <br />
                        <asp:Button runat="server" ID="btnClose" Text="Close" OnClick="btnClose_Click" />
                    </div>                  
                    <footer class="w3-container w3-light-gray">
                        <p>&nbsp;</p>
                    </footer>
                <%--</ContentTemplate>
            </asp:UpdatePanel>--%>
        </div>
    </div>  
</asp:Content>
