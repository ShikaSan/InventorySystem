<%@ Page Language="VB" 
    AutoEventWireup="false" 
    CodeBehind="Print.aspx.vb" 
    Inherits="InventorySystem.Print"
    EnableEventValidation="false"
    EnableSessionState="True"
    MasterPageFile="~/Forms/pmInventory.Master"
    MaintainScrollPositionOnPostback="true" %>

<%@ MasterType VirtualPath="~/Forms/pmInventory.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Print</title>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <div>
        
    </div>
</asp:Content>
