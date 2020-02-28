<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Forms/pmInventory.Master" CodeBehind="Assign_Additional_Equip.aspx.vb" Inherits="InventorySystem.Assign_Additional_Equip" EnableViewState="true" %>

<%@ MasterType VirtualPath="~/Forms/pmInventory.Master" %>

<%@ Register Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit" tagPrefix="ajax" %> 

<asp:Content ContentPlaceHolderID="MainHead" runat="server">
    <link rel="stylesheet" href="../bootstrap-4.4.1-dist/css/bootstrap.min.css" />
</asp:Content>

<asp:Content ID="PageContentHead" ContentPlaceHolderID="head" runat="server">
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
        select{
            margin-right: 12px;
        }
        #formDiv {
            margin-bottom: 10px;
        }
    </style>

<%--    <script src="../jquery-ui/jquery-ui-1.12.1/jquery-ui.min.js" type="text/javascript"></script>
    <link rel="stylesheet" href="../jquery-ui/jquery-ui-1.12.1/jquery-ui.min.css" />
    <link rel="../jquery-ui/jquery-ui-1.12.1/jquery-ui.theme.min.css" />
    <link rel="stylesheet" href="../jquery-ui/jquery-ui-1.12.1/jquery-ui.structure.min.css" />--%>
</asp:Content>

<asp:Content ID="PageContentBody" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
<%--        <div id="formDiv" class="fieldGroup container mt-3 bg-white">
        <div class="input-group">
        <select id='selectType' name="selectType" class="initial-selectType form-control">
            <option value="0">-- SELECT TYPE --</option>
        </select>
        <select id='selectDesc' name="selectDesc" class="form-control">
            <option value="0">-- SELECT DESCRIPTION --</option>
            
        </select>
        <select id='selectPPE' name="selectPPE" class="form-control">
            <option value="0">-- SELECT PPE #--</option>
            
        </select>
        <select id='selectAsset' name="selectAsset" class="form-control">
            <option value="0">-- SELECT ASSET #--</option>
            
        </select>
        <select id='selectSerial' name="selectSerial" class="form-control">
            <option value="0">-- SELECT SERIAL #--</option>
            
        </select>
            <div class="input-group-addon"> 
                <a href="javascript:void(0)" class="btn btn-success addMore"><span class="glyphicon glyphicon glyphicon-plus" aria-hidden="true"></span> +</a>
            </div>
        </div>
    </div>--%>

    <div id="actions" class="actions container align-content-center">
        <input type="button" id="testing" value="TEST" />
        <asp:Button ID="Button1"  ClientIDMode="Static" CssClass="saveBtn btn btn-primary" runat="server" Text="Save" OnClientClick="" OnClick="Button1_Click"/>
        <asp:Button ID="Button2" runat="server" CssClass="btn btn-danger" Text="Cancel" OnClick="Button2_Click" />
        <asp:HiddenField ID="hiddenCounter" runat="server" ClientIDMode="Static" />
        <asp:TextBox ID="TextBox1" runat="server" ClientIDMode="Static"></asp:TextBox>
        <input type="button" id="testButton" value="test db"/>
    </div>
</asp:Content>

<asp:Content ContentPlaceHolderID="BodyScripts" runat="server">
       <!-- copy of input fields group -->
<%--    <div class="container mt-3 bg-white form-group fieldGroupCopy" style="display: none;">
        <div class="input-group">

        <select name="selectType" class="form-control">
            <option value="0">-- SELECT TYPE --</option>
        </select>

        <select name="selectDesc" class="form-control">
            <option value="0">-- SELECT DESCRIPTION --</option>
        </select>

        <select name="selectPPE" class="form-control">
            <option value="0">-- SELECT PPE #--</option>
        </select>

        <select name="selectAsset" class="form-control">
            <option value="0">-- SELECT ASSET #--</option>
        </select>

        <select name="selectSerial" class="form-control">
            <option value="0">-- SELECT SERIAL #--</option>
        </select>

            <div class="input-group-addon"> 
                <a href="javascript:void(0)" class="btn btn-danger remove"><span class="glyphicon glyphicon glyphicon-remove" aria-hidden="true"></span> -</a>
            </div>

        </div>
    </div>--%>

    <script src="../js/jquery-3.4.1.min.js" type="text/javascript"></script>
    <script src="../js/jquery_plugins/jquery-validation/dist/jquery.validate.min.js" type="text/javascript"></script>
    <script src="../bootstrap-4.4.1-dist/js/bootstrap.min.js" type="text/javascript"></script>
    <script type="text/javascript">
        var fieldLength = 5;
        var selectRowNumber = 1;
        var previousValues = [];
        var ctr = 0;

        function getList_OfTypes(counterVar) {
            var getTypes = <%= GetEquipType() %>;
            for (var z = 0; z < getTypes.length; z++) {
                $(document).find('#selectType' + counterVar).append("<option value='" + getTypes[z] +"'>" + getTypes[z] + "</option>");
            }
        }

        function getSelectedValue() {
            var x = document.getElementById(this);
            $('#TextBox1').val(x);
        }

        $(document).ready(function () {
            //group add limit
            var maxGroup = 10;
            //var ctr = 0;

            //$('.saveBtn').attr('disabled', 'disabled');

            var initial_ListOfTypes = <%= GetEquipType() %>;

            for (var z = 0; z < initial_ListOfTypes.length; z++) {
                $('#selectType').append("<option value='" + initial_ListOfTypes[z] + "'>" + initial_ListOfTypes[z] + "</option>");
            }

            //add more fields group
            $(".addMore").click(function () {
                ctr = ctr + 1;
                $('.saveBtn').attr('disabled', 'disabled');
                var divHTML = "<div style=\"margin-top: 20px; margin-bottom: 20px;\" class=\"input-group\">\r\n            <select name=\"selectType\" id=\"selectType" + ctr + "\" class=\"form-control\">\r\n            <option value=\"0\">-- SELECT TYPE --<\/option>\r\n        <\/select>\r\n        <select name=\"selectDesc\" id=\"selectDesc" + ctr + "\" class=\"form-control\">\r\n            <option value=\"0\">-- SELECT DESCRIPTION --<\/option>\r\n            <option value=\"1\">2<\/option>\r\n            <option value=\"2\">3<\/option>\r\n            <option value=\"3\">4<\/option>\r\n            <option value=\"4\">5<\/option>\r\n        <\/select>\r\n        <select name=\"selectPPE\" id=\"selectPPE" + ctr + "\" class=\"form-control\">\r\n            <option value=\"0\">-- SELECT PPE #--<\/option>\r\n            <option value=\"1\">2<\/option>\r\n            <option value=\"2\">3<\/option>\r\n            <option value=\"3\">4<\/option>\r\n            <option value=\"4\">5<\/option>\r\n        <\/select>\r\n        <select name=\"selectAsset\" id=\"selectAsset" + ctr + "\" class=\"form-control\">\r\n            <option value=\"0\">-- SELECT ASSET #--<\/option>\r\n            <option value=\"1\">2<\/option>\r\n            <option value=\"2\">3<\/option>\r\n            <option value=\"3\">4<\/option>\r\n            <option value=\"4\">5<\/option>\r\n        <\/select>\r\n        <select name=\"selectSerial\" id=\"selectSerial" + ctr + "\" class=\"form-control\">\r\n            <option value=\"0\">-- SELECT SERIAL #--<\/option>\r\n            <option value=\"1\">2<\/option>\r\n            <option value=\"2\">3<\/option>\r\n            <option value=\"3\">4<\/option>\r\n            <option value=\"4\">5<\/option>\r\n        <\/select>\r\n            <div class=\"input-group-addon\"> \r\n                <a href=\"javascript:void(0)\" class=\"btn btn-danger remove\"><span class=\"glyphicon glyphicon glyphicon-remove\" aria-hidden=\"true\"><\/span> -<\/a>\r\n            <\/div>\r\n        <\/div>";

                //if ($('body').find('#formDiv').length < maxGroup) {
                if (ctr < maxGroup) {
                    //var fieldHTML = '<div class="container mt-3 bg-white form-group fieldGroup">' + divHTML + '</div>';
                    //$('body').find('.fieldGroup:last').after(fieldHTML);
                    $('#formDiv').append(divHTML);
                    $('#hiddenCounter').val(ctr);
                    fieldLength = fieldLength + 5;
                    selectRowNumber = selectRowNumber + 1;
                    getList_OfTypes(ctr);
                }
                else {
                    alert('Maximum of 10 groups of fields are allowed.');
                }
            });

            //remove fields group
            $('#formDiv').on("click", ".remove", function () {
                $(this).parents(".input-group").remove();
                ctr = ctr - 1;
                $('#hiddenCounter').val(ctr);
                fieldLength = fieldLength - 5;

                console.log(previousValues.length);
            });

            //triggerDropDown_ChangeFunction();

            
        });

        //function triggerDropDown_ChangeFunction() {
        //    var selectCounter = 0;
        //    var valueIndexer;

        //    $(document).on('change', '.fieldGroup > .input-group > .form-control', function () {
        //         var n = previousValues.includes($(this).attr("id"));

        //         if ($(this).val() != "0" && selectCounter < fieldLength && n == false) {
        //            selectCounter = selectCounter + 1;
        //            previousValues.push($(this).attr("id"));
        //            alert($(this).attr("id") + " first if");
        //         }

        //        if ($(this).val() != "0" && selectCounter < fieldLength && n == true) {
        //            selectCounter = selectCounter;
        //            alert($(this).attr("id") + " second if");
        //        }

        //        if ($(this).val() != "0" && selectCounter >= fieldLength && n == true) {
        //            selectCounter = selectCounter;
        //            alert($(this).attr("id") + " third if");
        //        }

        //        if($(this).val() == 0 && selectCounter <= fieldLength && n == true) {
        //            selectCounter = selectCounter - 1;

        //            valueIndexer = previousValues.indexOf($(this).attr("id").toString());

        //            if (valueIndexer > -1) {
        //                previousValues.splice(valueIndexer, 1);
        //            }

        //            alert("last if");
        //        }

        //        if (selectCounter >= fieldLength) {
        //             $('.saveBtn').prop('disabled', false);
        //        }
        //        else {
        //            $('.saveBtn').prop('disabled', true);
        //        }

        //        $('#TextBox1').val($(this).val());

        //        console.log("Select Counter: " + selectCounter + " Field Length: " + fieldLength + " " + ctr);

        //    });
        //}
    </script>
</asp:Content>