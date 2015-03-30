
function tree(obj, tblname) {
    /*var el = document.getElementById(tblname);
		
		var src  = obj.src;
    if(src.indexOf("menu_e_n") != -1) {
    obj.src = "../images/menu_e_p.gif";
    el.style.display = "none";
    }
    else {
    obj.src = "../images/menu_e_n.gif";
    el.style.display = "block";
    }*/
    var el = document.getElementsByName(tblname);
    if (el.length > 1) {
        for (var i = 0; i < el.length; i += 2) {
            var src = obj.src;
            if (src.indexOf("menu_e_n") != -1) {
                obj.src = "../../images/menu_e_p.gif";
                el[i].style.display = "none";
                el[i + 1].style.display = "none";
            }
            else {
                obj.src = "../../images/menu_e_n.gif";
                el[i].style.display = "block";
                el[i + 1].style.display = "block";
            }
        }
    }
    else if (el.length == 1) {
        el = document.getElementById(tblname);
        var src = obj.src;
        if (src.indexOf("menu_e_n") != -1) {
            obj.src = "../../images/menu_e_p.gif";
            el.style.display = "none";
        }
        else {
            obj.src = "../../images/menu_e_n.gif";
            el.style.display = "block";
        }
    }

}
function isnumeric(strNumber) {
    var regexNum = new RegExp("^[0-9]");
    for (var i = 0; i < strNumber.length; i++) {
        if (!regexNum.test(strNumber.charAt(i))) {
            return false;
        }
    }
    return true;
}
function ismaxlength(obj) {
    var mlength = obj.getAttribute ? parseInt(obj.getAttribute("maxlength")) : "";
    if (obj.getAttribute && obj.value.length > mlength);
    obj.value = obj.value.substring(0, mlength);
}

function windowOpen(url, target, width, height) {
    var leftVal = (screen.width - width) / 2;
    var topVal = (screen.height - height) / 2;
    var sFeature = "top=" + topVal + ",left=" + leftVal + ",width=" + width + ",height=" + height + ",toolbar=no,resizable=no,scrollbars=yes,status=yes"
    window.open(url, target, sFeature);
    return false;
}

function windowOpenReturn(url, target, width, height) {
    //Created by kalue: 29/09/2007
    //What difference fn windowOpen: Popup Page will be return to opener. So opener page is status IsPostBack.
    var leftVal = (screen.width - width) / 2;
    var topVal = (screen.height - height) / 2;
    var sFeature = "top=" + topVal + ",left=" + leftVal + ",width=" + width + ",height=" + height + ",toolbar=no,resizable=no,scrollbars=yes,status=yes"
    return window.open(url, target, sFeature);
}

function windowMinimize() {
    window.innerWidth = 100;
    window.innerHeight = 100;
    window.screenX = screen.width;
    window.screenY = screen.height;
    alwaysLowered = true;
}

function windowOpenMaximize(url, target) {
    var nWidth = screen.width - 10;
    var nHeight = screen.height - 80;
    var sFeature = "top=0,left=0,width=" + nWidth + ",height=" + nHeight + ",toolbar=no,resizable=no,scrollbars=yes,status=yes"
    window.open(url, target, sFeature);
    return false;
}

function windowOpenMaximizeReturn(url, target) {
    //Created by kalue: 29/09/2007
    //What difference fn windowOpenMaximize: Popup Page will be return to opener. So opener page is status IsPostBack.
    var nWidth = screen.width - 10;
    var nHeight = screen.height - 80;
    var sFeature = "top=0,left=0,width=" + nWidth + ",height=" + nHeight + ",toolbar=no,resizable=no,scrollbars=yes,status=yes"
    return window.open(url, target, sFeature);
}

function padLeft(str, pad, count) {
    while (str.length < count)
        str = pad + str;
    return str;
}
function padRight(str, pad, count) {
    while (str.length < count)
        str = str + pad;
    return str;
}
function checkKeyCode(e) {
    if (e.keyCode == 13) { return false; }
}

function checkPage(intMaxPage, strPage) {
    var arrPage = strPage.split('|||');
    if (((document.forms[0].elements[arrPage[1]].value * 1) > intMaxPage) || ((document.forms[0].elements[arrPage[1]].value * 1) <= 0)) {
        if (strPage != "") {
            alertvb(arrPage[0], 48);
            document.forms[0].elements[arrPage[1]].focus();
            document.forms[0].elements[arrPage[1]].select();
            return false;
        }
    }
    return true;
}

function checkInt(field, maxvalue) {
    var str = field.value;
    var strpic = "";
    for (var ind = 0; ind < str.length; ind++) {
        var ch = str.substring(ind, ind + 1)
        if (ch >= "0" && ch <= "9") {
            if (parseInt(((strpic + ch) / 1)) > parseInt(maxvalue)) {
                alertvb("Last Page is : " + maxvalue + ".", 48);
                ind = str.length + 2;
                field.value = maxvalue;
            }
            else {
                strpic = strpic + ch;
            }
        }
        else {
            ind = str.length + 2;
            alertvb('Please Input Number Only!', 48);
            field.value = strpic;
        }
    }
}


function chkDecimal(fld, dec, sep) { // fixed decimal fields 
    if (!fld.value.length || fld.disabled) return true; // blank fields are the domain of requireValue 
    var val = fld.value;
    if (typeof (sep) != 'undefined') val = val.replace(new RegExp(sep, 'g'), '');

    val = parseFloat(val);
    if (isNaN(val)) { // parse error 
        var dV = 0;
        dV = dV.toFixed(dec);
        fld.value = dV;
        return false;
    }
    fld.value = val.toFixed(dec);
    return true;
}

function chkTime30(fld) { // fixed decimal fields 
    if (!fld.value.length || fld.disabled) {
        fld.value = "00:00";
        return true;
    } // blank fields are the domain of requireValue 
    var val = fld.value;
    var re = new RegExp("^([0-1][0-9]|[2][0-3]):([0][0]|[3][0])$");
    if (!fld.value.match(re)) {
        alert('please input data hh:mm (hh : 00 - 23 and mm : 00,30)');
        fld.value = "00:00"
        fld.focus();
        return false;
    }
    return true;
}

function chkTime15(fld) { // fixed decimal fields 
    if (!fld.value.length || fld.disabled) {
        fld.value = "00:00";
        return true;
    } // blank fields are the domain of requireValue 
    var val = fld.value;
    var re = new RegExp("^([0-1][0-9]|[2][0-3]):([0][0]|[0][5]|[1][0]|[1][5]|[2][0]|[2][5]|[3][0]|[3][5]|[4][0]|[4][5]|[5][0]|[5][5])$");
    if (!fld.value.match(re)) {
        alert('please input data hh:mm (hh : 00 - 23 and mm : 00,05,10,15,20,25,30,35,40,45,50,55)');
        fld.value = "00:00"
        fld.focus();
        return true;
    }
    return true;
}

function checkNaN(n) {
    return !(n >= 0 || n < 0);
}

function calTime(fld, ele, target) {
    var obj = document.getElementById(ele);
    var obj2 = document.getElementById(target);
    var re = new RegExp("^([0-9]|[0-9][0-9]).([0][0]|[5][0]|[0]|[5])$");
    var re2 = new RegExp("^([0-9]|[0-9][0-9])$");
    if (!fld.value.match(re)) {
        if (!fld.value.match(re2)) {
            fld.value = "0.00";
        }
    }
    var str1 = obj.value.split(":");
    if (str1[0].indexOf("0") == 0) {

        str1[0] = str1[0].slice(1, 2);
    }
    var hour1 = parseInt(str1[0]);
    var min1 = parseInt(str1[1]);
    var str2 = fld.value.split(".");
    var hour2 = parseInt(str2[0]);
    var min2 = parseInt(str2[1]);
    if (min1 == 30) {
        if (min2 == 50) {
            hour1 = hour1 + hour2 + 1;
            min1 = 0;
        } else {
            hour1 = hour1 + hour2;
        }
    } else {
        if (min2 == 50) {
            hour1 = hour1 + hour2;
            min1 = min1 + 30;
        } else {
            hour1 = hour1 + hour2;
        }
    }
    alert(hour1);
    if (hour1 > 23) {
        fld.value = "0.00";
        alert("More Than 1 day");
        obj2.value = obj.value;
        return true;
    }
    var hour3 = "00";
    var min3 = "00";
    if (hour1 > 9) {
        hour3 = hour1;
    } else {
        hour3 = "0" + hour1;
    }
    if (min1 == 0) {
        min3 = "00";
    } else {
        min3 = "30";
    }
    obj2.value = hour3 + ":" + min3;
    return true;
}

function calTimeName(fld1, ele, target) {
    var fld = document.getElementById(fld1)// Hour
    var obj = document.getElementById(ele); //Start Time
    var obj2 = document.getElementById(target); // Finish Time
    var re = new RegExp("^([0-9]|[0-9][0-9]).([0]|[5]|[0][0]|[5][0])$");
    var re2 = new RegExp("^([0-9]|[0-9][0-9])$");
    if (fld.value != "") {
        var str1 = obj.value.split(":");
        if (str1[0].indexOf("0") == 0) {

            str1[0] = str1[0].slice(1, 2);
        }
        var hour1 = parseInt(str1[0]);

        var min1 = parseInt(str1[1]);

        var str2 = fld.value.split(".");

        var hour2 = parseInt(str2[0]);

        //alert('str2[1] ' + str2[1]);
        if (str2[1] == null) {
            str2[1] = "0";
        }
        var min2 = parseInt(str2[1]);
        //alert('min1 ' + min1);
        //alert('min2 ' + min2);
        var minx = min1 + (min2 * 60) / 100;
        //alert(minx);
        if (minx >= 60) {
            hour1 = hour1 + hour2 + 1;
            min1 = (minx - 60);
        } else {
            hour1 = hour1 + hour2;
            min1 = minx;
        }
        //alert(min1);

        /*	 if (min1 >= 30){
        if (min2 == 50){
        hour1 = hour1 + hour2 + 1;
        min1 =  0 ;
        }else{
        hour1 = hour1 + hour2;
        }
        }else{
        if (min2 == 50){
        hour1 = hour1 + hour2;
        min1 = min1 + 30;
        }else{
        hour1 = hour1 + hour2;
        }
        }*/
        if (hour1 > 23) {
            alert("More Than 1 day");
            obj2.value = obj.value;
            return true;
        }
        var hour3 = "00";
        var min3 = "00";
        if (hour1 > 9) {
            hour3 = hour1;
        } else {
            hour3 = "0" + hour1;
        }
        //alert(min1);
        if (min1 < 10) {
            min3 = "0" + min1;
        } else {
            min3 = min1;
        }
        obj2.value = hour3 + ":" + min3;
    }
    else {
        obj2.value = "";
    }
    return true;
}

function chkPositive(fld, dec, sep) {
    chkDecimal(fld, dec, sep);

    if (fld.value < 0) {
        alertvb('Please fill number more than zero.', 48);
        fld.select();
    }
}

// ----------------------------------------------------------------------------
// frmRequest_KeyDown
//
// Description: event handler for request form key down event
//    translates returns on option buttons to a tab
//    this works only for IE, the keypress event is used for other browsers
//
// Arguments : 
//    e - the event object
//
// Dependencies : none
//
// History :
// 2006.07.13 - WSR : adapted to this project
//
function frmRequest_KeyDown(e) {

    var numCharCode;
    var elTarget;
    var strType;

    // get event if not passed
    if (!e) var e = window.event;

    // get character code of key pressed
    if (e.keyCode) numCharCode = e.keyCode;
    else if (e.which) numCharCode = e.which;

    // get target
    if (e.target) elTarget = e.target;
    else if (e.srcElement) elTarget = e.srcElement;

    if (elTarget.tagName.toLowerCase() != 'textarea') {
        // if this is a return - change to tab

        if (numCharCode == 13) {
            if (e.keyCode) e.keyCode = 9;
            else if (e.which) e.which = 9;
        }
    }


    // process default action
    return true;

}
//
// frmRequest_KeyDown
// ----------------------------------------------------------------------------


// ----------------------------------------------------------------------------
// frmRequest_KeyPress
//
// Description: event handler for request form key press event
//    cancels returns on form elements that would prematurely submit the form
//
// Arguments : 
//    e - the event object
//
// Dependencies : none
//
// History :
// 2006.07.13 - WSR : adapted to this project
//
function frmRequest_KeyPress(e) {

    var numCharCode;
    var elTarget;
    var strType;

    // get event if not passed
    if (!e) var e = window.event;

    // get character code of key pressed
    if (e.keyCode) numCharCode = e.keyCode;
    else if (e.which) numCharCode = e.which;

    // get target
    if (e.target) elTarget = e.target;
    else if (e.srcElement) elTarget = e.srcElement;

    if (elTarget.tagName.toLowerCase() != 'textarea') {
        // if this is a return
        if (numCharCode == 13) {
            if (e.keyCode) e.keyCode = 9;
            else if (e.which) e.which = 9;
        }

    }


    // process default action
    return true;

}
//
// frmRequest_KeyPress
// ----------------------------------------------------------------------------

function on_load_list() {
    var frmRequest = document.forms[0];
    if (frmRequest) {
        if (window.event)
            frmRequest.onkeydown = frmRequest_KeyDown;
        else
            frmRequest.onkeypress = frmRequest_KeyPress;
    }
}

function on_load() {
    var frmRequest = document.forms[0];
    if (frmRequest) {
        if (window.event)
            frmRequest.onkeydown = frmRequest_KeyDown;
        else
            frmRequest.onkeypress = frmRequest_KeyPress;
    }
    var obj1 = window.parent;
    obj1.focus();

}

function selectParent(parentObj, selectValue) {
    for (i = 0; i < parentObj.length; i++) {
        //alert(parentObj.options[i].value + ' - ' + selectValue);
        if (parentObj.options[i].value == selectValue) {
            parentObj.options[i].selected = true;
            break;
        }
    }
}

function windowOpenMax(url, target) {
    var leftVal = 0;
    var topVal = 0;
    var sFeature = "top=" + topVal + ",left=" + leftVal + ",width=" + screen.width + ",height=" + screen.height + ",toolbar=no,resizable=no,scrollbars=yes,status=yes"
    window.open(url, target, sFeature);
    return false;
}

function doClick(url, target, width, height, e) {
    var key;
    var leftVal = (screen.width - width) / 2;
    var topVal = (screen.height - height) / 2;
    var sFeature = "top=" + topVal + ",left=" + leftVal + ",width=" + width + ",height=" + height + ",toolbar=no,resizable=no,scrollbars=yes,status=yes"

    if (window.event) {
        key = window.event.keyCode; // IE	
    }
    else {
        key = e.which; // firefox
    }

    if (key == 13) {
        window.open(url, target, sFeature);
        event.keyCode = 0
        return false;
    }
}

function CommaFormatted(objAmount) {  //****** Thousands separate by comma ************
    var amount = objAmount.value;
    var delimiter = ","; // replace comma if desired
    var a = amount.split('.', 2)
    var d = a[1];
    var i = a[0];
    i = i.replace(/,/g, "");
    i = parseInt(i);

    if (isNaN(i)) { return ''; }
    var minus = '';
    if (i < 0) { minus = '-'; }
    i = Math.abs(i);
    var n = new String(i);
    var a = [];
    while (n.length > 3) {
        var nn = n.substr(n.length - 3);
        a.unshift(nn);
        n = n.substr(0, n.length - 3);
    }
    if (n.length > 0) { a.unshift(n); }
    n = a.join(delimiter);
    if (typeof (d) != 'undefined') {
        if (d.length < 1) { amount = n; }
        else { amount = n + '.' + d; }
    }
    else {
        amount = n;
    }
    amount = minus + amount;
    objAmount.value = amount;
}

function CommaFormattedInnerHTML(objAmount) {
    //****** Thousands separate by comma for INNER HTML************
    var amount = objAmount.innerText;

    var delimiter = ","; // replace comma if desired
    var a = amount.split('.', 2)
    var d = a[1];
    var i = a[0];
    i = i.replace(/,/g, "");
    i = parseInt(i);

    if (isNaN(i)) { return ''; }
    var minus = '';
    if (i < 0) { minus = '-'; }
    i = Math.abs(i);
    var n = new String(i);
    var a = [];
    while (n.length > 3) {
        var nn = n.substr(n.length - 3);
        a.unshift(nn);
        n = n.substr(0, n.length - 3);
    }
    if (n.length > 0) { a.unshift(n); }
    n = a.join(delimiter);
    if (typeof (d) != 'undefined') {
        if (d.length < 1) { amount = n; }
        else { amount = n + '.' + d; }
    }
    else {
        amount = n;
    }
    amount = minus + amount;
    objAmount.innerText = amount;
}

function chkDecimalInnerHTML(fld, dec, sep) {
    // fixed decimal fields 
    if (!fld.innerText.length || fld.disabled) return true; // blank fields are the domain of requireValue 
    var val = fld.innerText;
    if (typeof (sep) != 'undefined') val = val.replace(new RegExp(sep, 'g'), '');

    val = parseFloat(val);
    if (isNaN(val)) { // parse error 
        var dV = 0;
        dV = dV.toFixed(dec);
        fld.value = dV;
        return false;
    }
    fld.innerText = val.toFixed(dec);
    return true;
}

function keytime(obj) {
    evt = window.event;
    var key = evt.keyCode;
    if (!((key >= 48 && key <= 57) || key == 58)) {
        window.event.keyCode = 0;
    }
    else {
        if (key == 58) {
            if ((obj.value.length == 2)) {
                obj.value += ":";
            }
            window.event.keyCode = 0;
        }
        else if ((obj.value.length == 2) && key != 58) {
            obj.value += ":";
        }
    }
}

function alertjava(msgs) {
    alert(msgs);
    return false;
}

function chktextbox(e, text) {
    var val = e.value;
    var val_text = text.value;
    if (!val) {
        alert('กรุณาป้อนข้อมูล' + val);
        e.focus();
        return false;
    }
    return true;
}

function chkcombo(e, text) {
    var val = e.value;
    var val_text = text.value;
    if (!val) {
        alert('กรุณาเลือกข้อมูล' + val);
        e.focus();
        return false;
    }
    return true;
}

function gWH() {
    var e = window, a = 'inner';
    if (!('innerWidth' in window)) {
        a = 'client';
        e = document.documentElement || document.body;
    }
    return { width: e[a + 'Width'], height: e[a + 'Height'] }
}


function OpenPopUp(pwidth, pheight, iframeheight, headname, url, level) {
    var panel = 'ctl00_panelShow' + level;
    var divdes = 'divdes' + level;
    var iframe = 'iframeShow' + level;
    var modal = 'show' + level + '_ModalPopupExtender';
    window.parent.document.getElementById(panel).style.width = pwidth;
    window.parent.document.getElementById(panel).style.height = pheight;
    window.parent.document.getElementById(divdes).innerHTML = headname;
    window.parent.document.getElementById(iframe).style.height = iframeheight;
    window.parent.document.getElementById(iframe).style.width = "100%";
    window.parent.document.getElementById(iframe).src = url;
    window.parent.$find(modal).show();
    return false;
}

function OpenPopUpRun(pwidth, pheight, iframeheight, headname, url, level) {
    var panel = 'ctl00_panelShow' ;
    var divdes = 'divdes' ;
    var iframe = 'iframeShow' ;
    var modal = 'show_ModalPopupExtender';
    console.log();
    $("#" + panel).width = pwidth;
    $("#" + divdes).html(headname);
    $("#" + iframe).height = iframeheight;
    $("#" + iframe).width = "100%";
    $("#" + iframe).src = url;
    $find(modal).show();

}

function ClosePopUp(level) {
    var iframe = 'iframeShow' + level;
    var modal = 'show' + level + '_ModalPopupExtender';
    window.parent.document.getElementById(iframe).src = 'about:blank';
    window.parent.document.getElementById(iframe).style.width = '0px';
    window.parent.document.getElementById(iframe).style.height = '0px';
    window.parent.$find(modal).hide();
    return false;
}

function ClosePopUpListPost(page, level) {
    var iframe = 'iframeShow' + level;
    var modal = 'show' + level + '_ModalPopupExtender';
    window.parent.document.forms[0].ctl00$ASPxRoundPanel1$ContentPlaceHolder2$txthpage.value = page;
    window.parent.__doPostBack('ctl00$ASPxRoundPanel1$ContentPlaceHolder2$GridView1$ctl01$cboPerPage', '');
    //window.parent.__doPostBack('BindGridView',page) ;
    window.parent.document.getElementById(iframe).src = 'about:blank';
    window.parent.document.getElementById(iframe).style.width = '0px';
    window.parent.document.getElementById(iframe).style.height = '0px';
    window.parent.$find(modal).hide();
    return false;
}

function PopUpListPost(page, level) {
    var iframe = 'iframeShow' + level;
    var modal = 'show' + level + '_ModalPopupExtender';
    window.parent.document.forms[0].ctl00$ASPxRoundPanel1$ContentPlaceHolder2$txthpage.value = page;
    window.parent.__doPostBack('ctl00$ASPxRoundPanel1$ContentPlaceHolder2$GridView1$ctl01$cboPerPage', '');
    return false;
}


function RefreshMain(page) {
    window.parent.document.forms[0].ctl00$ASPxRoundPanel1$ContentPlaceHolder2$txthpage.value = page;
    window.parent.__doPostBack('ctl00$ASPxRoundPanel1$ContentPlaceHolder2$GridView1$ctl01$cboPerPage', '');
    return false;
}


var monthDay = new Array(31, -1, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31);


function DateDiff(pfromDate, ptoDate) {

    var increment = 0;
    var day = 0, month = 0, year = 0;

    var AfromDate = pfromDate.split("/", 3);
    var AfromDate2 = ptoDate.split("/", 3);
    var fromDate = new Date(AfromDate[2] - 543, AfromDate[1] - 1, AfromDate[0]);
    var toDate = new Date(AfromDate2[2] - 543, AfromDate2[1] - 1, parseInt(AfromDate2[0]) + 1);


    if (fromDate.getDate() > toDate.getDate()) {
        increment = monthDay[fromDate.getMonth()];
    }
    if (increment == -1) {
        if (IsLeapYear(fromDate)) {
            increment = 29;
        }
        else {
            increment = 28;
        }
    }


    if (increment != 0) {
        day = (toDate.getDate() + increment) - fromDate.getDate();
        increment = 1;
    }
    else {
        day = toDate.getDate() - fromDate.getDate();
    }
    if ((fromDate.getMonth() + increment) > toDate.getMonth()) {
        month = (toDate.getMonth() + 12) - (fromDate.getMonth() + increment);
        increment = 1;
    }
    else {
        month = (toDate.getMonth()) - (fromDate.getMonth() + increment);
        increment = 0;
    }
    year = toDate.getFullYear() - (fromDate.getFullYear() + increment);


    var diff = new Object();
    diff.Years = year;
    diff.Months = month;
    diff.Days = day;
    return diff;
};
function IsLeapYear(utc) {
    var y = utc.getFullYear();
    return !(y % 4) && (y % 100) || !(y % 400) ? true : false;
};

function show_diff_date(fromDate, toDate) {
    var diff = DateDiff(fromDate, toDate)
    var strreturn = '';
    if (diff.Years > 0) strreturn = strreturn + diff.Years + ' ปี ';
    if (diff.Months > 0) strreturn = strreturn + diff.Months + ' เดือน ';
    if (diff.Days > 0) strreturn = strreturn + diff.Days + ' วัน ';
    return strreturn;
};

function show_diff_day(fromDate, toDate) {
    var diff = DateDiff(fromDate, toDate)
    var strreturn = '0';
    if (diff.Days > 0) strreturn = diff.Days;
    return strreturn;
};

function show_diff_month(fromDate, toDate) {
    var diff = DateDiff(fromDate, toDate)
    var strreturn = '0';
    if (diff.Months > 0) strreturn = diff.Months;
    return strreturn;
};

function show_diff_year(fromDate, toDate) {
    var diff = DateDiff(fromDate, toDate)
    var strreturn = '0';
    if (diff.Years > 0) strreturn = diff.Years;
    return strreturn;
};





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

function chkBoxValidate(chk, cb) {
    for (j = 1; j <= 5; j++) {
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


function createDate(ctr_id,today) {
    $(function() {
        var dateBefore = null;
        $("#" + ctr_id).datepicker({
            dateFormat: 'dd/mm/yy',
            showOn: 'button',
            buttonImage: '../../images/Calendar_scheduleHS.png',
            buttonText: 'คลิกเพื่อแสดงวันที่',
            buttonImageOnly: true,
            dayNamesMin: ['อา', 'จ', 'อ', 'พ', 'พฤ', 'ศ', 'ส'],
            monthNamesShort: ['มกราคม', 'กุมภาพันธ์', 'มีนาคม', 'เมษายน', 'พฤษภาคม', 'มิถุนายน', 'กรกฎาคม', 'สิงหาคม', 'กันยายน', 'ตุลาคม', 'พฤศจิกายน', 'ธันวาคม'],
            changeMonth: true,
            changeYear: true,
            showButtonPanel: true,
            currentDay : today ,
            currentText: "วันนี้",
            closeText:"ปิด",
            beforeShow: function() {
                if ($(this).val() != "") {
                    var arrayDate = $(this).val().split("/");
                    arrayDate[2] = parseInt(arrayDate[2]) - 543;
                    $(this).val(arrayDate[0] + "/" + arrayDate[1] + "/" + arrayDate[2]);
                }
                setTimeout(function() {
                    $.each($(".ui-datepicker-year option"), function(j, k) {
                        var textYear = parseInt($(".ui-datepicker-year option").eq(j).val()) + 543;
                        $(".ui-datepicker-year option").eq(j).text(textYear);
                    });
                }, 50);
                setTimeout(function() {
                    if ($("#" + ctr_id).val() != "") {
                        var arrayDate = $("#" + ctr_id).val().split("/");
                        arrayDate[2] = parseInt(arrayDate[2]) + 543;
                        $("#" + ctr_id).val(arrayDate[0] + "/" + arrayDate[1] + "/" + arrayDate[2]);
                    }
                }, 50);

            },
            onChangeMonthYear: function() {
                setTimeout(function() {
                    $.each($(".ui-datepicker-year option"), function(j, k) {
                        var textYear = parseInt($(".ui-datepicker-year option").eq(j).val()) + 543;
                        $(".ui-datepicker-year option").eq(j).text(textYear);
                    });
                }, 50);
            },
            onClose: function() {
                if ($(this).val() != "" && $(this).val() == dateBefore) {
                    var arrayDate = dateBefore.split("/");
                    arrayDate[2] = parseInt(arrayDate[2]) + 543;
                    $(this).val(arrayDate[0] + "/" + arrayDate[1] + "/" + arrayDate[2]);
                }
            },
            onSelect: function(dateText, inst) {
                dateBefore = $(this).val();
                var arrayDate = dateText.split("/");
                arrayDate[2] = parseInt(arrayDate[2]) + 543;
                $(this).val(arrayDate[0] + "/" + arrayDate[1] + "/" + arrayDate[2]);
            }

        });

    });
}





