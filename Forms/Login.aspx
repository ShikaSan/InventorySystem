<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Login.aspx.vb" Inherits="InventorySystem.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>ITSG Inventory System</title>

    <%--<link rel="stylesheet" href="css/bootstrap.min.css" />--%>
    <link rel="stylesheet" href="/CSS/w3.css" />
    <link rel="stylesheet" href="/CSS/font-awesome.min.css" />

    <style>
        html, body, h1, h2, h3, h4, h5 {
            font-family: "Segoe UI", sans-serif;
        }
        /* latin-ext */
        @font-face {
            font-family: 'Segoe UI';
            font-style: normal;
            font-weight: 400;
            src: local('Segoe UI'), local('Segoe UI-SemiBold'), url(https://fonts.gstatic.com/s/raleway/v11/yQiAaD56cjx1AooMTSghGfY6323mHUZFJMgTvxaG2iE.woff2) format('woff2');
            unicode-range: U+0100-024F, U+1E00-1EFF, U+20A0-20AB, U+20AD-20CF, U+2C60-2C7F, U+A720-A7FF;
        }
        /* latin */
        @font-face {
            font-family: 'Segoe UI';
            font-style: normal;
            font-weight: 400;
            src: local('Segoe UI'), local('Segoe UI-SemiBold'), url(https://fonts.gstatic.com/s/raleway/v11/0dTEPzkLWceF7z0koJaX1A.woff2) format('woff2');
            unicode-range: U+0000-00FF, U+0131, U+0152-0153, U+02C6, U+02DA, U+02DC, U+2000-206F, U+2074, U+20AC, U+2212, U+2215;
        }

        .CustomColor {
            background-color: #002050;
        }       
    </style>
    <script type="text/javascript">
        function preventBack() { window.history.forward(); }
        setTimeout("preventBack()", 0);
        window.onunload = function () { null };
    </script>
</head>
<body class="w3-light-grey w3-main">
    <form id="form1" runat="server">
        <div class="w3-row " style="margin-top: 100px;">
            <div class="w3-col s4">
                &nbsp;
            </div>
            <div class="w3-col s4 w3-card-4 w3-round-small w3-white text-center ">
                <div class="w3-col w3-text-white w3-center  w3-round-small " style="height: 63px; background-color: #1e8cdb;">
                    <h3 style="font-family: 'Segoe UI'">ITSG Inventory Sytem</h3>
                </div>
                <%--class="w3-animate-zoom"--%>
                <div style="margin-bottom:10px">
                    <asp:Panel ID="pnlWarningMessage" runat="server" CssClass="w3-red w3-center" Visible="false">
                        <asp:Label ID="lblWarningMessage" runat="server">Account is invalid</asp:Label>
                    </asp:Panel>

                    <div class="w3-center" style="font-size: small;">
                        <label>&nbsp;</label>
                    </div>
                    <hr style="margin-top: 0px;" />
                    <div class="w3-padding">
                        <label>User Name</label>
                        <asp:TextBox ID="txtUserName" runat="server" CssClass="w3-input w3-border w3-round-small w3-center"></asp:TextBox>
                    </div>

                    <div class="w3-padding">
                        <label>Password</label>
                        <asp:TextBox ID="txtPassword" TextMode="Password" runat="server" CssClass="w3-input w3-border w3-round-small w3-center"></asp:TextBox>
                    </div>

                    <div class="w3-padding w3-margin-top">
                        <asp:Button ID="btnLogin" runat="server" CssClass="w3-btn w3-light-gray w3-border" Text="Login" Style="width: 100%;" OnClick="btnSignIn_Click" />
                    </div>

                     <hr style="margin-top: 0px; margin-bottom: 0px;" />

                    <div style="margin-top: 0px;" class="w3-center">
                        <label class="w3-text-gray" style="font-size: 11px; margin-top: 0px; margin-bottom: 0px;">© Copyright 2018</label><br />
                        <label class="w3-text-gray" style="font-size: 11px; margin-top: 0px; margin-bottom: 5px;">Wordtext Systems, Inc.</label>
                    </div>
                </div>
            </div>
            <div class="w3-col s4">
                &nbsp;
            </div>
        </div>
    </form>
</body>
</html>

