function RegisScript() {
    $('.btnCalc').click(function() {
        var result = confirm('ต้องการคำนวณเลขที่ใบเบิกหรือไม่ ?');
        if (result) {
            var i = 0;
            var strValue = 0;
            $("input[type='text'][id*=txtitem_acc_deka]").each(function() {
                if (i == 0) {
                    strValue = $(this).val();
                    if (strValue!= '' && !isNaN(strValue)) {
                        strValue = parseInt($(this).val()) - 1;
                    } else {
                        strValue = 0;
                    }
                }
                $(this).val(++strValue);
                i++;
            });
        }
    });

    $('.btnCalcPPV').click(function() {
        var result = confirm('ต้องการคำนวณเลขที่ PPV หรือไม่ ?');
        if (result) {
            var i = 0;
            var strValue = '';
            $("input[type='text'][id*=txtppv_no]").each(function() {
                if (i == 0) {
                    strValue = $(this).val();
                }
                $(this).val(strValue);
                i++;
            });
        }
    });



}
