

function removeCharAtAwNumeric(str, keychar, istart, iend) {
    var nstr = '';
    var ilength = iend;
    var bStr = false;

    for (var i = 0; i < str.length; i++) {
        if (i >= istart && i < ilength) {
            if (bStr == false) {
                nstr += keychar;
                bStr = true;
            }
        }
        else {
            nstr += str.charAt(i);
        }
    }
    return nstr;
}



function setNegativeAwNumeric(str) {
    var regNegative = /-/g;
    if (str.indexOf('-') != -1) {
        str = str.replace(regNegative, '');
        str = '-' + str;
    }
    return str;
}

//Setting Cursor Position in a Textbox
function setCaretPositionAwNumeric(obj, pos) {
    if (obj.createTextRange) {
        var range = obj.createTextRange();
        range.move("character", pos);
        range.select();
    } else if (obj.selectionStart) {
        obj.focus();
        obj.setSelectionRange(pos, pos);
    }
    else {
        obj.focus();
        obj.setSelectionRange(pos, pos);
    }
}

function addCommasAwNumeric(nStr) {
    nStr.value += '';
    x = nStr.value.split('.');
    x1 = x[0];
    x2 = x.length > 1 ? '.' + x[1] : '';
    var rgx = /(\d+)(\d{3})/;
    while (rgx.test(x1)) {
        x1 = x1.replace(rgx, '$1' + ',' + '$2');
    }
    nStr.value = x1 + x2;
}



function RemoveCommasAwNumeric(nStr) {
    var x;
    if (nStr.value.indexOf(",") != -1) {
        nStr.value += '';
        x = nStr.value;
        x = x.replace(/,/g, "");
        nStr.value = x;
    }
}

function getSelectedTextAwNumeric(textbox) {
    if (document.selection) {
        return document.selection.createRange().text;
    }
    else {
        return textbox.value.substring(textbox.selectionStart, textbox.selectionEnd);
    }
}

function IsNumericAwNumeric(val) {
    return (parseFloat(val, 10) == (val * 1));
}

function InsertCharAwNumeric(string, chr, pos) {
    var output = '';
    var c;

    if (pos == 0) {
        output = chr + string;
    }
    else {
        for (var i = 0; i <= string.length; i++) {
            c = string.charAt(i);
            if (i == pos)
                output = output + chr + c;
            else
                output += c;
        }
    }
    return output;
}

function GetKeyPositoinAwNumeric(obj) {
    var sel;
    var rng;
    var r2;
    var i = -1;

    if (typeof obj.selectionStart == "number") {
        i = obj.selectionStart;
    }
    else if (document.selection && obj.createTextRange) {
        sel = document.selection;
        if (sel) {
            r2 = sel.createRange();
            rng = obj.createTextRange();
            rng.setEndPoint("EndToStart", r2);
            i = rng.text.length;
        }
    }
    else {
        obj.onkeyup = null;
        obj.onclick = null;
    }
    return i;

}

function onKeyupEventHandlerAwNumeric(obj, e, decimalPlaces, allowNegative) {
    var temp = obj.value;
    temp = setNegativeAwNumeric(temp);
    if (obj.value != temp) {
        obj.value = temp;
    }
}
function onKeypressEventHandlerAwNumeric(obj, e, decimalPlaces, allowNegative, min, max) {

    var browser = navigator.appName;
    var b_version = navigator.appVersion;
    var version = parseFloat(b_version);
    var key;
    var isCtrl = false;
    var keychar;
    var iPos;
    var newVal;
    var startPos = obj.selectionStart;
    var endPos = obj.selectionEnd;
    var indexDot;
    var allowDecimal = false;

    if (decimalPlaces > 0)   // use decimal point.
        allowDecimal = true;

//    obj.value.substring(startPos, endPos);

    if (window.event) {
        key = e.keyCode;
        isCtrl = window.event.ctrlKey
    }
    else if (e.which) {
        key = e.which;
        isCtrl = e.ctrlKey;
    }

    if (isNaN(key) || key == 0) {
        return true;
    }


    keychar = String.fromCharCode(key);

    // check for backspace or delete, or if Ctrl was pressed
    if (key == 8 || isCtrl)
        return true;

    if (browser.toLowerCase().indexOf("microsoft") != -1) {
        startPos = GetSelectionStartAt(obj);
        endPos = GetSelectionEndAt(obj);
    }
    

    if (startPos == endPos) {
        iPos = GetKeyPositoinAwNumeric(obj);
        newVal = InsertCharAwNumeric(obj.value, keychar, iPos);
    }
    else {
        newVal = removeCharAtAwNumeric(obj.value, keychar, startPos, endPos);
    }

    if (keychar == '-') {
        if (allowNegative == false) {
            return false;
        }
        else {
            newVal = setNegativeAwNumeric(newVal);
        }
    }

    if (keychar == '+') {   // replace negative to empty.
        var regNegative = /-/g;
        newVal = newVal.replace(regNegative, '');
    }


    if (keychar == '.') {
        if (allowDecimal == false)
            return false;
        indexDot = obj.value.indexOf('.');
        if (indexDot != -1) {
            indexDot = indexDot + 1;
            setCaretPositionAwNumeric(obj, indexDot);
        }
    }

    var isFirstN = allowNegative ? keychar == '-' && obj.value.indexOf('-') == -1 : false;
    var isFirstD = allowDecimal ? keychar == '.' && obj.value.indexOf('.') == -1 : false;


    if (isFirstD || isFirstN || newVal == '-.')
        return true;

    if (IsNumericAwNumeric(newVal) == false) {
        return false;
    }

    if (parseInt(min) == 0 && parseInt(max) == 0)
        return true;

    if (parseInt(max) != 0) {
        if (parseFloat(newVal) > parseFloat(max)) {
            return false;
        }
    }

    if (parseInt(min) != 0) {
        if (parseFloat(newVal) < parseFloat(min)) {
            return false;
        }
    }

}

function onKeyDownHandlerAwNumeric(obj, e) {

    var strSel;
    var key;
    var keychar;

    if (window.event) {
        key = e.keyCode;
    }
    else if (e.which) {
        key = e.which;
    }

    iPos = GetKeyPositoinAwNumeric(obj);
    //Backspace
    if (key == 8) {
        var strDot = obj.value.charAt(iPos - 1)
        if (obj.value.length == 2 && (obj.value.indexOf('.') == 0||obj.value.indexOf('.')==1)) {
            obj.value = '';
            return true;
        }
        if (strDot == '.') {
            setCaretPositionAwNumeric(obj, iPos - 1);
            return false;
        }
    }
    
    //Delete
    if (key == 46) {
        var strDot = obj.value.charAt(iPos)
        if (obj.value.length == 2 && (obj.value.indexOf('.') == 0||obj.value.indexOf('.') == 1)) {
            obj.value = '';
            return true;
        }
        if (strDot == '.') {
            setCaretPositionAwNumeric(obj, iPos + 1);
            return false;
        }
    }

    if ((key >= 37 && key <= 40) || // Left, Up, Right and Down
        key == 8 || // backspaceASKII
        key == 9 || // tabASKII
        key == 16 || // shift
        key == 17 || // control
        key == 35 || // End
        key == 36 || // Home
        key == 46) // deleteASKII
        return true;
}

function onFocusHandlerAwNumeric(obj) {
    RemoveCommasAwNumeric(obj);
}

function onblurEventHandlerAwNumeric(obj, allowComma, decimalPlaces, leadZero, min, max) {

    var intText;

    if (IsNumericAwNumeric(obj.value)) {
        obj.value = (parseFloat(obj.value)).toFixed(decimalPlaces);  // Format - Decimal Precision
        if (allowComma) {
            addCommasAwNumeric(obj);
        }
    }
    else {
        if (leadZero == 'Hide') {
            obj.value = '';
        }
        else {
            var zero = 0;
            obj.value = zero.toFixed(decimalPlaces);
        }
    }


    intText = RemoveCommasStringAwNumeric(obj.value);
    if (parseInt(max) != 0) {
        if (parseFloat(intText) > parseFloat(max)) {
            if (leadZero == 'Hide') {
                obj.value = '';
            }
            else {
                var zero = 0;
                obj.value = zero.toFixed(decimalPlaces);
            }
        }
    }

    if (parseInt(min) != 0) {
        if (parseFloat(intText) < parseFloat(min)) {
            if (leadZero == 'Hide') {
                obj.value = '';
            }
            else {
                var zero = 0;
                obj.value = zero.toFixed(decimalPlaces);
            }
        }
    }

}

function GetCursorPosition(inputElement) {
    var i = inputElement.value.length;
    if (inputElement.createTextRange) {
        var range = document.selection.createRange().duplicate();
        while (range.parentElement() == inputElement && range.move("character", 1) == 1)
            --i;
        return i;
    }
    else {
        return -1;
    }
}

function GetSelectionEndAt(inputElement) {
    var range = document.selection.createRange().duplicate();
    return GetCursorPosition(inputElement) + range.text.length;
}

function GetSelectionStartAt(inputElement) {
    return GetCursorPosition(inputElement);
}


function RemoveCommasStringAwNumeric(str) {
    var x;
    if (str.indexOf(",") != -1) {
        str += '';
        x = str;
        x = x.replace(/,/g, "");
        str = x;
    }
    return str;
}
