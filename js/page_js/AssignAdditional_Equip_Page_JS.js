        $(document).ready(function () {
            var rowCount = 1;
            var oldCount = 1;
            var hiddenCount = 0;

            $(document).on('click', '.add', function () {

                var datahtml = '';
                datahtml += '<tr>';
                datahtml += '<td><select id="selectTypeRow' + rowCount + '" name="select_type" class="form-control select_type"></select></td>';
                datahtml += '<td><select id="selectDescRow' + rowCount + '" name="select_desc" class="form-control select_desc"><option value="0">--SELECT--</option></select></td>';
                datahtml += '<td><select id="selectPPERow' + rowCount + '" name="select_ppe" class="form-control select_ppe"><option value="0">--SELECT--</option></select></td>';
                datahtml += '<td><select id="selectAssetRow' + rowCount + '" name="select_asset" class="form-control select_asset"><option value="0">--SELECT--</option></select></td>';
                datahtml += '<td><select id="selectSerialRow' + rowCount + '" name="select_serial" class="form-control select_serial"><option value="0">--SELECT--</option></select></td>>';
                datahtml += '<td><input type="button" name="remove" class="btn btn-danger btn-sm remove" value="REMOVE" /></td></tr>';

                $('#equip_table').append(datahtml);

                if (oldCount <= 1) {
                    rowCount = rowCount + 1;
                    oldCount = rowCount;
                }
                else {
                    rowCount = rowCount + 1;
                    oldCount = rowCount;
                }

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

            $(document).on('click', '.remove', function () {
                $(this).closest('tr').remove();

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

        function GetEquipDesc(rowVar, valueVar) {
            //alert(rowVar + ' ' + valueVar);
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