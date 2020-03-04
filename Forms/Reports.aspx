<%@ Page Language="vb"
    AutoEventWireup="false"
    CodeBehind="Reports.aspx.vb"
    Inherits="InventorySystem.Reports"
    EnableEventValidation="false"
    EnableSessionState="True"
    MaintainScrollPositionOnPostback="true"
    MasterPageFile="~/Forms/pmInventory.Master" %>
<%@ MasterType VirtualPath="~/Forms/pmInventory.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title></title>
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
                <div class="w3-row" style="border-bottom: 1px solid #EEEEEE; margin-top: 5px; margin-bottom: 5px;">
                </div>
            </div>
        </div>
    </div>
</asp:Content>
