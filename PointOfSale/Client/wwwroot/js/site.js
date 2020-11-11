// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
window.StartLoading = function () {
    StartLoding();
}
window.StopLoading = function () {
    StopLoding();
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