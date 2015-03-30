

function onKeypressEventHandlerAwTextBox(obj, e, filterchar) {

    var key;
    var keychar;

    if (filterchar != '') {
        if (window.event) {
            key = e.keyCode;
        }
        else if (e.which) {
            key = e.which;
        }
        if (isNaN(key) || key == 0) {
            return true;
        }
        keychar = String.fromCharCode(key);
        if (filterchar.indexOf(keychar) != -1) {
            return false;
        }
    }

}

function onKeyUpEventHandlerAwTextBox(obj, e) {

    var key;

    if (window.event) {
        key = e.keyCode;
    }
    else if (e.which) {
        key = e.which;
    }
    if (isNaN(key) || key == 0) {
        return true;
    }


    switch (key) {
        case 37: // Arrow Left
            return true;
            break;
        case 38: // Arrow Up
            return true;
            break;
        case 39: // Arrow Right
            return true;
            break;
        case 40: // Arrow Down
            return true;
            break;
    }

    var mlength = obj.getAttribute ? parseInt(obj.getAttribute("maxlength")) : "";
    if (obj.getAttribute && obj.value.length > mlength);
    obj.value = obj.value.substring(0, mlength);
}


function onBlurEventHandlerAwTextBox(obj, e, filterchar) {

    var strFilterchar;

    if (filterchar != '') {
        for (i = 0; i < filterchar.length; i++) {
            strFilterchar = filterchar.charAt(i);
            if (obj.value.indexOf(strFilterchar) != -1) {
                obj.value = '';
            }
        }
    }

}


function validateMaxlength(obj, maxlength) {
    if (obj.value.length > maxlength) {
        obj.value = obj.value.substring(0, maxlength);
    }
}

