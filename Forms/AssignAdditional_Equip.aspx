<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Forms/pmInventory.Master" CodeBehind="AssignAdditional_Equip.aspx.vb" Inherits="InventorySystem.AssignAdditional_Equip" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainHead" runat="server">
    <link rel="stylesheet" href="../bootstrap-4.4.1-dist/css/bootstrap.min.css" runat="server" />
</asp:Content>

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
        #form-table {
            margin-top: 10px;
        }
        .btnDiv{
            background-color: white;
            margin-top: 20px;
        }
    </style>
    
    <script src="../js/jquery-3.4.1.min.js" type="text/javascript"></script>
    <script src="../bootstrap-4.4.1-dist/js/bootstrap.min.js" type="text/javascript"></script>
</asp:Content>


<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <div id="form-table" class="table-responsive">
        <span id="error"></span>
        <table class="table table-bordered" id="equip_table">
            <tr>
                <td><p><b>Assign To:</b></p></td>
                <td colspan="5"><asp:TextBox ID="TextBox1" runat="server" ReadOnly="true" Text="Joel Victor M. Lorilla"></asp:TextBox></td>
            </tr>
            <tr>
                <th>EQUIPMENT TYPE</th>
                <th>EQUIPMENT DESCRIPTION</th>
                <th>EQUIPMENT PPE #</th>
                <th>EQUIPMENT ASSET #</th>
                <th>EQUIPMENT SERIAL #</th>
                <th><input type="button" name="add" class="btn btn-success btn-sm add" value="ADD" /></th>
            </tr>
        </table>
        <div class="btnDiv">
            <asp:Button ID="updateBtn" CssClass="btn btn-primary" runat="server" Text="UPDATE" ClientIDMode="Static" OnClick="updateBtn_Click"/>
            <asp:Button ID="cancelBtn" CssClass="btn btn-danger" runat="server" Text="CANCEL" />
            <asp:HiddenField ID="hiddenCounterField" runat="server" ClientIDMode="Static" />
        </div>
    </div>
</asp:Content>

<asp:Content ContentPlaceHolderID="BodyScripts" runat="server">

    <script type="text/javascript">
        $(document).ready(function () {
            var rowCount = 1;
            var oldCount = 1;
            var hiddenCount = 0;

            //Code to add fields/rows to the form.
            $(document).on('click', '.add', function () {

                var datahtml = '';
                datahtml += '<tr>';
                datahtml += '<td><select id="selectTypeRow' + rowCount + '" name="select_type" class="form-control select_type"></select></td>';
                datahtml += '<td><select id="selectDescRow' + rowCount + '" name="select_desc" class="form-control select_desc"><option value="0">--SELECT--</option></select></td>';
                datahtml += '<td><select id="selectPPERow' + rowCount + '" name="select_ppe" class="form-control select_ppe"><option value="0">--SELECT--</option></select></td>';
                datahtml += '<td><select id="selectAssetRow' + rowCount + '" name="select_asset" class="form-control select_asset"><option value="0">--SELECT--</option></select></td>';
                datahtml += '<td><select id="selectSerialRow' + rowCount + '" name="select_serial" class="form-control select_serial"><option value="0">--SELECT--</option></select></td>>';
                datahtml += '<td><input type="button" name="remove" class="btn btn-danger btn-sm remove" value="REMOVE" /></td></tr>';

                //$('#equip_table').append(datahtml).hide().fadeIn('slow');
                $(datahtml).hide().appendTo('#equip_table').fadeIn('normal');

                if (oldCount <= 1) {
                    rowCount = rowCount + 1;
                    oldCount = rowCount;
                }
                else {
                    rowCount = rowCount + 1;
                    oldCount = rowCount;
                }

                /*Selects every <select> element with a class of select_type, calls a function on CodeBehind
                 the function returns an array of serialized strings, append those strings as <option> elements on
                 <select> elements.*/
                $('.select_type').each(function() {
                    var getList_EquipType = <%= GetEquipType() %>;
                    var optionLength = getList_EquipType.length;
                    var dropDownLength = $(this)[0].length;

                    if (dropDownLength == 0) {
                         $(this).append('<option value="0">--SELECT--</option>');
                         for (var i = 0; i < optionLength; i++) {
                            $(this).append('<option value"' + getList_EquipType[i] + '">' + getList_EquipType[i] + '</option>');
                        }
                    }
                });

                hiddenCount = hiddenCount + 1;
                $('#hiddenCounterField').val(hiddenCount);
            });

            //Code to remove fields.
            $(document).on('click', '.remove', function () {
                $(this).closest('tr').fadeOut('normal', function () {
                    $(this).remove();
                });

                if (oldCount <= 1) {
                    rowCount = rowCount - 1;
                    oldCount = rowCount;
                }
                else {
                    rowCount = (oldCount + 1) - 1;
                } 

                hiddenCount = hiddenCount - 1;
                $('#hiddenCounterField').val(hiddenCount);
            });

            /*Code when UPDATE button is clicked. Returns false if there are fields missing. 
            Otherwise, if all fields have selected values this will return true and proceeds to the
            Code-Behind to insert/update to database.*/
            $('#updateBtn').click(function (event) {

                var error = '';

                $('.select_type').each(function () {
                    var count = 1;
                    if ($(this).val() == '0') {
                        error += "<p>SELECT AN EQUIPMENT TYPE AT " + count + " ROW</p>";
                        return false;
                    }
                    count = count + 1;
                });

                $('.select_desc').each(function () {
                    var count = 1;
                    if ($(this).val() == '0') {
                        error += "<p>SELECT EQUIPMENT DESCRIPTION AT " + count + " ROW</p>";
                        return false;
                    }
                    count = count + 1;
                });

                $('.select_ppe').each(function () {
                    var count = 1;
                    if ($(this).val() == '0') {
                        error += "<p>SELECT PPE # AT " + count + " ROW</p>";
                        return false;
                    }
                    count = count + 1;
                });

                $('.select_asset').each(function () {
                    var count = 1;
                    if ($(this).val() == '0') {
                        error += "<p>SELECT ASSET # AT " + count + " ROW</p>";
                        return false;
                    }
                    count = count + 1;
                });

                $('.select_serial').each(function () {
                    var count = 1;
                    if ($(this).val() == '0') {
                        error += "<p>SELECT SERIAL # AT " + count + " ROW</p>";
                        return false;
                    }
                    count = count + 1;
                });

                if (error == '') {
                    return true;
                }
                else {
                    $('#error').html('<div class="alert alert-danger">' + error + '</div>');
                    return false;
                }
            });

            /*Code that fires an event when each select value changes, 
              each IF statement calls one of the functions below.*/
            $(document).on('change', 'select', function () {
                var selectedRow = $(this).attr('id').toString();
                var selectedValue = $(this).val();
                var rowSubStr = '';

                if (selectedRow.startsWith('selectTypeRow')) {
                    rowSubStr = selectedRow.slice(10, 14);
                    GetEquipDesc(rowSubStr, selectedValue);
                    //method here
                    //console.log(rowSubStr);
                }

                if (selectedRow.startsWith('selectDescRow')) {
                    rowSubStr = selectedRow.slice(10, 14);
                    console.log(rowSubStr + " " + selectedValue);
                    GetEquipPPE(rowSubStr, selectedValue);
                }

                if (selectedRow.startsWith('selectPPERow')) {
                    rowSubStr = selectedRow.slice(9, 13);
                    console.log(rowSubStr + " " + selectedValue);
                    GetEquipAsset(rowSubStr, selectedValue);
                }

                if (selectedRow.startsWith('selectAssetRow')) {
                    rowSubStr = selectedRow.slice(11, 15);
                    console.log(rowSubStr + " " + selectedValue);
                    GetEquipSerial(rowSubStr, selectedValue);
                }

                if (selectedRow.startsWith('selectSerialRow')) {
                    rowSubStr = selectedRow.slice(12, 16);
                    console.log(rowSubStr);
                }
            });
        });

        /*Functions that calls a WebMethod on Code-Behind, each function passes some parameters to the WebMethod,
         the WebMethod then returns serialized strings, this code parses these strings and saves to an array. 
         Iterate through that array and append the values of the array to <option> tags on their respective <select> element.*/
        function GetEquipDesc(rowVar, valueVar) {
            $.ajax({  
                type: "POST",  
                url: "AssignAdditional_Equip.aspx/GetDropDownValues",  
                contentType: "application/json; charset=utf-8",  
                dataType: "json",  
                data: JSON.stringify({ 'DropDown_Param': 'Equipment_Description', 'EquipType_Param': valueVar, 'EquipName_Param': '', 'EquipPPE_Param': '' }),  
                success: function (data) {  
                    alert(data.d);
                    $('#selectDesc' + rowVar).find('option').remove().end().append('<option value="0">--SELECT--</option>');
                    var x = JSON.parse(data.d);
                    var xLength = x.length;

                    
                    for (var i = 0; i < xLength; i++) {
                        $('#selectDesc' + rowVar).append('<option value="' + x[i] + '">' + x[i] + '</option>');
                    }
                },
                error: function (response) {
                    alert(response);
                }
            });  
        }

        function GetEquipPPE(rowVar, valueVar) {
             $.ajax({  
                type: "POST",  
                url: "AssignAdditional_Equip.aspx/GetDropDownValues",  
                contentType: "application/json; charset=utf-8",  
                dataType: "json",  
                data: JSON.stringify({ 'DropDown_Param': 'Equipment_PPE_Number', 'EquipType_Param': valueVar, 'EquipName_Param': '', 'EquipPPE_Param': '' }),  
                success: function (data) {  
                    alert(data.d);
                    $('#selectPPE' + rowVar).find('option').remove().end().append('<option value="0">--SELECT--</option>');
                    var x = JSON.parse(data.d);
                    var xLength = x.length;

                    
                    for (var i = 0; i < xLength; i++) {
                        $('#selectPPE' + rowVar).append('<option value="' + x[i] + '">' + x[i] + '</option>');
                    }
                },
                error: function (response) {
                    alert(response);
                }
            });
        }

        function GetEquipAsset(rowVar, valueVar) {
            var selectedEquipDesc = $('#selectDesc' + rowVar).val();

            $.ajax({  
                type: "POST",  
                url: "AssignAdditional_Equip.aspx/GetDropDownValues",  
                contentType: "application/json; charset=utf-8",  
                dataType: "json",  
                data: JSON.stringify({ 'DropDown_Param': 'Equipment_Asset_Number', 'EquipType_Param': valueVar, 'EquipName_Param': selectedEquipDesc, 'EquipPPE_Param': '' }),  
                success: function (data) {  
                    alert(data.d);
                    $('#selectAsset' + rowVar).find('option').remove().end().append('<option value="0">--SELECT--</option>');
                    var x = JSON.parse(data.d);
                    var xLength = x.length;
                    console.log(selectedEquipDesc);

                    
                    for (var i = 0; i < xLength; i++) {
                        $('#selectAsset' + rowVar).append('<option value="' + x[i] + '">' + x[i] + '</option>');
                    }
                },
                error: function (response) {
                    alert(response);
                }
            });
        }

        function GetEquipSerial(rowVar, valueVar) {
            var selectedEquipDesc = $('#selectDesc' + rowVar).val();
            var selectedEquip_PPE = $('#selectPPE' + rowVar).val();

            $.ajax({  
                type: "POST",  
                url: "AssignAdditional_Equip.aspx/GetDropDownValues",  
                contentType: "application/json; charset=utf-8",  
                dataType: "json",  
                data: JSON.stringify({ 'DropDown_Param': 'Equipment_Serial', 'EquipType_Param': valueVar, 'EquipName_Param': selectedEquipDesc, 'EquipPPE_Param': selectedEquip_PPE }),  
                success: function (data) {  
                    alert(data.d);
                    $('#selectSerial' + rowVar).find('option').remove().end().append('<option value="0">--SELECT--</option>');
                    var x = JSON.parse(data.d);
                    var xLength = x.length;
                    console.log(selectedEquipDesc);

                    for (var i = 0; i < xLength; i++) {
                        $('#selectSerial' + rowVar).append('<option value="' + x[i] + '">' + x[i] + '</option>');
                    }
                },
                error: function (response) {
                    alert(response);
                }
            });
        }
    </script>

</asp:Content>
