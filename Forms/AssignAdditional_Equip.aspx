<%@ Page Title="" Language="vb" AutoEventWireup="false" EnableSessionState="True" MasterPageFile="~/Forms/pmInventory.Master" CodeBehind="AssignAdditional_Equip.aspx.vb" Inherits="InventorySystem.AssignAdditional_Equip" %>

<%@ MasterType VirtualPath="~/Forms/pmInventory.Master" %>

<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" href="../CSS/w3.css" runat="server" />
    <style>
        #headingDiv{
             background-color: #1e8cdb;
        }

        .labelSytle {
            font-size: 20px;
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

        .imageStyle {
            padding-top: 5px;
        }
        .btnDiv{
            background-color: white;
            margin-top: 20px;
        }
        .select2-container--default .select2-results__option--highlighted[aria-selected] {
          background-color: lightseagreen !important;
          color:white !important;
        }
    </style>
    <script src="../js/jquery-3.4.1.min.js" type="text/javascript"></script>
    <script src="../js/select2-4.0.13/dist/js/select2.min.js" type="text/javascript"></script>
    <link rel="stylesheet" href="../js/select2-4.0.13/dist/css/select2.min.css" />
    <script src="../bootstrap-4.4.1-dist/js/bootstrap.min.js" type="text/javascript"></script>
    
<<<<<<< Updated upstream
    <!-- This JS file handles most of the functionality of this webform. -->
=======
    <!--Code for this page's interactivity and codebehind webmethod calls is here. -->
>>>>>>> Stashed changes
    <script src="../js/page_js/AssignAdditional_Equip.js" type="text/javascript"></script>
</asp:Content>


<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <div id="form-table" class="table w3-container w3-section">
        <span id="error"></span>
        <table class="w3-table w3-border w3-striped" id="equip_table">
            <tr>
                <td><p><b>Assign To:</b></p></td>
                <td><asp:TextBox ID="TextBox1" runat="server" CssClass="w3-input" ReadOnly="true" ClientIDMode="Static"></asp:TextBox></td>
                <td><p><b>Location:</b></p></td>
                <td colspan="3">
                    <asp:DropDownList ID="LocationDropDownList" CssClass="w3-select form-control" runat="server" ClientIDMode="Static">
                                <asp:ListItem>--SELECT--</asp:ListItem>
                                <asp:ListItem>ELVDI</asp:ListItem>
                                <asp:ListItem>ITSDI</asp:ListItem>
                                <asp:ListItem>ITSG</asp:ListItem>
                                <asp:ListItem>WSI MAKATI</asp:ListItem>
                                <asp:ListItem>WSI CEBU</asp:ListItem>
                                <asp:ListItem>WSI DAVAO</asp:ListItem>
                                <asp:ListItem>WAREHOUSE</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <th>EQUIPMENT TYPE</th>
                <th>EQUIPMENT DESCRIPTION</th>
                <th>EQUIPMENT PPE #</th>
                <th>EQUIPMENT ASSET #</th>
                <th>EQUIPMENT SERIAL #</th>
                <th><input type="button" name="add" class="w3-button w3-circle w3-green add" value="+" /></th>
            </tr>
        </table>
        <div class="btnDiv">
            <asp:Button ID="updateBtn" CssClass="w3-button w3-round w3-blue" runat="server" Text="UPDATE" ClientIDMode="Static" OnClick="updateBtn_Click"/>
            <asp:Button ID="cancelBtn" CssClass="w3-button w3-round w3-red" runat="server" Text="CANCEL" OnClick="cancelBtn_Click" />
            <asp:HiddenField ID="hiddenCounterField" runat="server" ClientIDMode="Static" />
            <asp:HiddenField ID="hiddenUserId" runat="server" ClientIDMode="Static" />
        </div>
    </div>
</asp:Content>
