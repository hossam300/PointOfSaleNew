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
window.FullScreen = () => {
   // toggleFullScreen();
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


function toggleFullScreen() {
    if (!document.fullscreenElement) {
        document.documentElement.requestFullscreen();
    } else {
        if (document.exitFullscreen) {
            document.exitFullscreen();
        }
    }
}
//function toggleFullScreen() {
//    var videoElement = document.getElementById("contentBody");

//    if (!document.mozFullScreen && !document.webkitFullScreen) {
//        if (videoElement.mozRequestFullScreen) {
//            videoElement.mozRequestFullScreen();
//        } else {
//            videoElement.webkitRequestFullScreen(Element.ALLOW_KEYBOARD_INPUT);
//        }
//    } else {
//        if (document.mozCancelFullScreen) {
//            document.mozCancelFullScreen();
//        } else {
//            document.webkitCancelFullScreen();
//        }
//    }
//}

document.addEventListener("keydown", function (e) {
    if (e.keyCode == 13) {
        toggleFullScreen();
    }
}, false);

function goFullscreen() {

    var body = document.body;

    function toggleFullScreen() {
        if (!document.mozFullScreen && !document.webkitFullScreen) {
            if (body.mozRequestFullScreen) {
                body.mozRequestFullScreen();
            } else {
                body.webkitRequestFullScreen(Element.ALLOW_KEYBOARD_INPUT);
            }
        } else {
            if (document.mozCancelFullScreen) {
                document.mozCancelFullScreen();
            } else {
                document.webkitCancelFullScreen();
            }
        }
    }

    document.addEventListener("keydown", function (e) {
        if (e.keyCode == 13) {
            toggleFullScreen();
        }
    }, false);
}
function fullscreenChanged() {
    if (document.webkitFullscreenElement == null) {
        mf = document.body;
        mf.style.display = "none";
    }
}
//document.onwebkitfullscreenchange = fullscreenChanged;
//document.documentElement.onclick = goFullscreen;
//document.onkeydown = goFullscreen;

function requestFullScreen() {
    var el = document.documentElement
        , rfs = // for newer Webkit and Firefox
            el.requestFullScreen
            || el.webkitRequestFullScreen
            || el.mozRequestFullScreen
            || el.msRequestFullScreen
        ;
    if (typeof rfs != "undefined" && rfs) {
        rfs.call(el);
    } else if (typeof window.ActiveXObject != "undefined") {
        // for Internet Explorer
        var wscript = new ActiveXObject("WScript.Shell");
        if (wscript != null) {
            wscript.SendKeys("{F11}");
        }
    }
}