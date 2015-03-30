
/* Find and Checked/Unchecked in same column */

function SelectAllCheckboxes(obj, mode) {
    var oel = document.body.getElementsByTagName("input");
    for (var i = oel.length - 1; i >= 0; i--) {
        if (oel[i].type == "checkbox") {
            var strCtrlName = oel[i].name;
            var strCtrlArr = strCtrlName.split("$");
            switch (strCtrlArr[4]) {
                case mode:
                    oel[i].checked = obj.checked;
                    break;
            }
        }
    }
}


// Find and Checked/Unchecked in Gridview
function GridViewSelectAllCheckbox(strGridViewName, obj, col) {
    var grid = document.getElementById(strGridViewName);
    var chkApprove;
    var cell;

    if (grid.rows.length > 0) {
        //loop starts from 1. rows[0] points to the header.
        for (i = 1; i < grid.rows.length; i++) {
            //get the reference of first column
            cell = grid.rows[i].cells[col]; // การอนุมัติ
            // รายการ
            for (j = 0; j < cell.childNodes.length; j++) {
                if (cell.childNodes[j].type == 'checkbox') {
                    chkApprove = cell.childNodes[j];
                    chkApprove.checked = obj.checked;
                }
            }
        }
    }
}

function chkBoxValidate(chk,cb) 
{
    for (j = 1; j <= 5; j++) 
    {
        var obj = cb.substr(0, (cb.length - 1)) + j.toString();
        if (obj != cb) {
            document.getElementById(obj).checked = false;
        }
        else {
            //alert('xx');
            obj.checked = chk.checked;
        }
    }
    //document.getElementById(cb).checked = chk.checked;
    return false;
}