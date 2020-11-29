// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
window.StartLoading = function () {
    StartLoding();
}
window.StopLoading = function () {
    StopLoding();
}
window.requied = (ele) => {
    alert(ele);
}
window.PrintElem = (elem) => {
    var mywindow = window.open('', 'PRINT', 'height=600,width=900');

    mywindow.document.write('<html><head><title>' + document.title + '</title>');
    mywindow.document.write('</head><body >');
    mywindow.document.write('<h1>' + document.title + '</h1>');
    mywindow.document.write(document.getElementById(elem).innerHTML);
    mywindow.document.write('</body></html>');

    mywindow.document.close(); // necessary for IE >= 10
    mywindow.focus(); // necessary for IE >= 10*/

    mywindow.print();
    mywindow.close();

    return true;
}

window.PrintElemWithBarcod = (elem) => {
    var mywindow = window.open('', 'PRINT', 'height=600,width=900');

    mywindow.document.write('<html><head><title>' + document.title + '</title><link media="screen, print" href="https://fonts.googleapis.com/css2?family=Libre+Barcode+39+Extended+Text&display=swap" rel="stylesheet">');
    mywindow.document.write('</head><body >');
    mywindow.document.write('<h1>' + document.title + '</h1>');
    mywindow.document.write(document.getElementById(elem).innerHTML);
    mywindow.document.write('</body></html>');

    mywindow.document.close(); // necessary for IE >= 10
    mywindow.focus(); // necessary for IE >= 10*/

    mywindow.print();
    mywindow.close();

    return true;
}
window.Setfocus = (elem) => {
    $('#' + elem + ' :input').focus();
}
window.FullScreen = () => {
    document.getElementById("barcode2").focus();
}
function StartLoding() {
    document.getElementById("loader").style.display = "block";
    document.getElementById("contentBody").style.display = "none";
}
function StopLoding() {
    document.getElementById("loader").style.display = "none";
    document.getElementById("contentBody").style.display = "block";
}
window.JsFunctions = {

    initializeSelect2: function (name, placeholder) {
        if (placeholder === null || placeholder === '') {
            $('#' + name).select2({
                allowClear: false
            });
        }
        else {
            $('#' + name).select2({
                placeholder: 'Seleccione una opción',
                allowClear: false
            });
        }
        return true;
    }

};

