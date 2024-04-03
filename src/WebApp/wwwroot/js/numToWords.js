function inWords(numIn) {
    var inWordsSub = function (num) {
        var a = ['', 'One ', 'Two ', 'Three ', 'Four ', 'Five ', 'Six ', 'Seven ', 'Eight ', 'Nine ', 'Ten ', 'Eleven ', 'Twelve ', 'Thirteen ', 'Fourteen ', 'Fifteen ', 'Sixteen ', 'Seventeen ', 'Eighteen ', 'Nineteen '];
        var b = ['', '', 'Twenty', 'Thirty', 'Forty', 'Fifty', 'Sixty', 'Seventy', 'Eighty', 'Ninety'];
        var n = ('0000000' + num).substr(-7).match(/^(\d{2})(\d{2})(\d{1})(\d{2})$/);
        if (!n) return;
        var str = '';
        str += (n[1] != 0) ? (a[Number(n[1])] || b[n[1][0]] + ' ' + a[n[1][1]]) + 'Lakh ' : '';
        str += (n[2] != 0) ? (a[Number(n[2])] || b[n[2][0]] + ' ' + a[n[2][1]]) + 'Thousand ' : '';
        str += (n[3] != 0) ? (a[Number(n[3])] || b[n[3][0]] + ' ' + a[n[3][1]]) + 'Hundred ' : '';
        str += (n[4] != 0) ? ((str != '') ? 'and ' : '') + (a[Number(n[4])] || b[n[4][0]] + ' ' + a[n[4][1]]) : '';
        return str;
    }
    var nTemp = numIn.toString().split(".");
    var num = nTemp[0].split(",").join("");
    if (num.length > 14) return 'overflow';
    num = ('00000000000000' + num).substr(-14);
    var words = inWordsSub(num.substr(-7))
    var croreWords = inWordsSub(num.substr(0, 7))
    if (croreWords.trim() != "") {
        words = croreWords + "Crores " + words;
    }
    if (words.trim() != "") {
        words += " Rupees only";
    }
    return words;
}