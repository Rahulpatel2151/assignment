$.validator.unobtrusive.adapters.addSingleVal("gradval");
$.validator.addMethod("gradval", function (value, element, exclude) {
    var got = Date(value);
    var thresold = new Date(2020, 1, 1, 0, 0, 0, 0);
    console.log(value);
    return got > thresold;
}); 