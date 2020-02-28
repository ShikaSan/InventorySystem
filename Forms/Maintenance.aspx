<%@ Page Title="Maintenance" Language="vb"
    AutoEventWireup="false"
    MasterPageFile="~/Forms/pmInventory.Master"
    CodeBehind="Maintenance.aspx.vb"
    Inherits="InventorySystem.Maintenance"
    EnableEventValidation="False"
    EnableSessionState="True"
    MaintainScrollPositionOnPostback="true" %>

<%@ MasterType VirtualPath="~/Forms/pmInventory.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Maintenance</title>
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
        <div class="w3-row ">
            <div class="w3-col w3-half w3-card-4 w3-round-small w3-white w3-padding" style="margin-top: 4px; margin-bottom: 4px; width: 49.65%; margin-right: 4px">
                <div class="w3-row ">
                    <div class="w3-col s12 w3-padding-left" style="border-bottom: 1px solid #EEEEEE">
                        <h5 style="border-left: 6px solid #1e8cdb;">&nbsp;<b> Departments</b></h5>
                    </div>
                    <asp:GridView runat="server" ID="gvDept" AutoGenerateColumns="true" OnRowDataBound="gvDept_RowDataBound" AllowPaging="true" OnPageIndexChanging="gvDept_PageIndexChanging"
                        OnSelectedIndexChanged="gvDept_SelectedIndexChanged" BorderStyle="None" GridLines="Horizontal" Width="100%" CellPadding="5" Font-Size="14px">
                        <RowStyle CssClass="RowStyle" />
                        <HeaderStyle BackColor="#1e8cdb" CssClass="l9" ForeColor="White" />
                        <EmptyDataTemplate>
                            <div style="text-align: center;">
                                <label>...No Record found...</label>
                            </div>
                        </EmptyDataTemplate>
                    </asp:GridView>
                </div>
                <div class="w3-row w3-margin-top">
                    <asp:Label runat="server" Text="Department Name" CssClass="labelSytle"></asp:Label>
                </div>
                <div class="w3-row">
                    <asp:TextBox runat="server" ID="txtDeptName" CssClass="textboxStyle w3-input w3-border"></asp:TextBox>
                    <br />
                    <asp:Button runat="server" ID="btnDSave" OnClick="btnDSave_Click" Text="Save" CssClass="w3-button w3-border" Font-Size="12px" Font-Bold="true" BackColor="#1e8cdb" ForeColor="White" />
                    <asp:Button runat="server" ID="btnDCancel" OnClick="btnDCancel_Click" Text="Cancel" CssClass="w3-button w3-border" Font-Size="12px" Font-Bold="true" BackColor="#1e8cdb" ForeColor="White" />
                </div>
            </div>
            <div class="w3-col w3-half w3-card-4 w3-round-small w3-white w3-padding" style="margin-top: 4px; margin-bottom: 4px; width: 49.65%; margin-left: 4px">
                <div class="w3-row ">
                    <div class="w3-col s12 w3-padding-left" style="border-bottom: 1px solid #EEEEEE">
                        <h5 style="border-left: 6px solid #1e8cdb;">&nbsp;<b> Equipment Types</b></h5>
                    </div>
                    <asp:GridView runat="server" ID="gvTypes" AutoGenerateColumns="true" OnRowDataBound="gvTypes_RowDataBound" AllowPaging="true" OnPageIndexChanging="gvTypes_PageIndexChanging"
                        OnSelectedIndexChanged="gvTypes_SelectedIndexChanged" BorderStyle="None" GridLines="Horizontal" Width="100%" CellPadding="5" Font-Size="14px">
                        <RowStyle CssClass="RowStyle" HorizontalAlign="Center" />
                        <HeaderStyle BackColor="#1e8cdb" CssClass="l9" ForeColor="White" />
                        <EmptyDataTemplate>
                            <div style="text-align: center;">
                                <label>...No Record found...</label>
                            </div>
                        </EmptyDataTemplate>
                    </asp:GridView>
                </div>
                <div class="w3-row w3-margin-top ">
                    <asp:Label runat="server" Text="Equipment Type" CssClass="labelSytle"></asp:Label>
                </div>
                <div class="w3-row">
                    <asp:TextBox runat="server" ID="txtEqType" CssClass="textboxStyle w3-input w3-border"></asp:TextBox>
                    <br />
                    <asp:Button runat="server" ID="btnEqSave" OnClick="btnEqSave_Click" Text="Save" CssClass="w3-button w3-border" Font-Size="12px" Font-Bold="true" BackColor="#1e8cdb" ForeColor="White" />
                    <asp:Button runat="server" ID="btnEqCancel" OnClick="btnEqCancel_Click" Text="Cancel" CssClass="w3-button w3-border" Font-Size="12px" Font-Bold="true" BackColor="#1e8cdb" ForeColor="White" />
                </div>
            </div>
        </div>
        <div class="w3-row ">
            <div class="w3-card-4 w3-round-small w3-white w3-padding" style="margin-top: 4px; margin-bottom: 8px;">
                <div class="w3-row ">
                    <div class="w3-col s12 w3-padding-left" style="border-bottom: 1px solid #EEEEEE">
                        <h5 style="border-left: 6px solid #1e8cdb;">&nbsp;<b> User Access</b></h5>
                    </div>
                    <asp:GridView runat="server" ID="gvUserAccess" AutoGenerateColumns="true" OnRowDataBound="gvUserAccess_RowDataBound" AllowPaging="true" OnPageIndexChanging="gvUserAccess_PageIndexChanging"
                        OnSelectedIndexChanged="gvUserAccess_SelectedIndexChanged" BorderStyle="None" GridLines="Horizontal" Width="100%" CellPadding="5" Font-Size="14px">
                        <RowStyle CssClass="RowStyle" />
                        <HeaderStyle BackColor="#1e8cdb" CssClass="l9" ForeColor="White" />
                        <EmptyDataTemplate>
                            <div style="text-align: center;">
                                <label>...No Record found...</label>
                            </div>
                        </EmptyDataTemplate>
                    </asp:GridView>
                </div>
                <div class="w3-row w3-margin-top ">
                    <table style="width:100%">
                        <tr>
                            <td class="w3-padding">
                                <asp:Label runat="server" Text="Name" CssClass="labelSytle"></asp:Label>
                                <asp:TextBox runat="server" ID="txtName" CssClass="textboxStyle w3-input w3-border"></asp:TextBox>
                            </td>
                            <td class="w3-padding">
                                <asp:Label runat="server" Text="Domain" CssClass="labelSytle"></asp:Label>
                                <asp:TextBox runat="server" ID="txtDomain" CssClass="textboxStyle w3-input w3-border"></asp:TextBox>
                            </td>
                            <td class="w3-padding">
                                <asp:Label runat="server" Text="Access Type" CssClass="labelSytle"></asp:Label>
                                <asp:DropDownList runat="server" CssClass="textboxStyle w3-input w3-border" ID="ddlUserLvl">
                                    <asp:ListItem Value="0">---</asp:ListItem>
                                    <asp:ListItem Value="RW">Read Write</asp:ListItem>
                                    <asp:ListItem Value="RO">Read Only</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                    </table>
                    <%--<div class="w3-col w3-third w3-padding" style="position:relative;margin-top: 4px; margin-bottom: 4px; ">
                        
                    </div>
                    <div class="w3-col w3-third w3-padding" style="position:relative;margin-top: 4px; margin-bottom: 4px; ">
                        
                    </div>
                    <div class="w3-col w3-third w3-padding" style="position:relative;margin-top: 4px; margin-bottom: 4px; ">
                        <
                    </div>--%>
                    <%-- testtestestse --%>
                </div>
                <div class="w3-row w3-margin-top ">
                    <asp:Button runat="server" ID="btnULSave" Text="Save" OnClick="btnULSave_Click" CssClass="w3-button w3-border" Font-Size="12px" Font-Bold="true" BackColor="White" ForeColor="Black" />
                    <asp:Button runat="server" ID="btnULCancel" Text="Cancel" OnClick="btnULCancel_Click" CssClass="w3-button w3-border" Font-Size="12px" Font-Bold="true" BackColor="#1e8cdb" ForeColor="White" />
                </div>
            </div>
        </div>
        <div class="w3-row ">
            <div class="w3-card-4 w3-round-small w3-white w3-padding" style="margin-top: 4px; margin-bottom: 8px;">
                <div class="w3-col s12 w3-padding-left" style="border-bottom: 1px solid #EEEEEE">
                    <h5 style="border-left: 6px solid #1e8cdb;">&nbsp;<b> Upload Excel File</b></h5>
                </div>
                <asp:FileUpload ID="FileUpload2" runat="server" CssClass="w3-button w3-border" Font-Size="12px" Font-Bold="true" BackColor="White" ForeColor="Black" />
                <asp:Button runat="server" ID="btnUploadEqOnly" Text="Upload Equipments Only" OnClick="btnUploadEqOnly_Click1" CssClass="w3-button w3-border" Font-Size="12px" Font-Bold="true" BackColor="#1e8cdb" ForeColor="White" />
                <%--<asp:GridView runat="server" AutoGenerateColumns="true" ID="sampleonly"></asp:GridView>--%>
            </div>
        </div>
    </div>
</asp:Content>
