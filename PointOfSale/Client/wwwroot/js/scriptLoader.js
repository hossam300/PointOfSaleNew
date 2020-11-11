window.loadScript = function (scriptPath) {
    // check list - if already loaded we can ignore
    if (loaded[scriptPath]) {
        console.log(scriptPath + " already loaded");
        // return 'empty' promise
        return new this.Promise(function (resolve, reject) {
            resolve();
        });
    }

    return new Promise(function (resolve, reject) {
        // create JS library script element
        var script = document.createElement("script");
        script.src = scriptPath;
        script.type = "text/javascript";
        console.log(scriptPath + " created");

        // flag as loading/loaded
        loaded[scriptPath] = true;

        // if the script returns okay, return resolve
        script.onload = function () {
            console.log(scriptPath + " loaded ok");
            resolve(scriptPath);
        };

        // if it fails, return reject
        script.onerror = function () {
            console.log(scriptPath + " load failed");
            reject(scriptPath);
        }

        // scripts will load at end of body
        document["body"].appendChild(script);
    });
}
// store list of what scripts we've loaded
loaded = [];
window.removeScript = function () {
    var d = document.getElementById("kt_scrolltop");
    var d2 = document.getElementById("kt_header_mobile");
    d.parentNode.removeChild(d); d2.parentNode.removeChild(d2);
    //var footer = document.getElementById('kt_footer');
    //footer.parentNode.removeChild(footer);
    //$("div > .d-flex align-items-center").html('');
    //var x = document.getElementsByTagName('script');
    //var host = location.protocol + '//' + location.host + "/";
    //for (var i = 0; i < x.length; i++) {
    //    if (x[i].src == undefined || x[i].src == "" || x[i].src == host + "_content/Microsoft.AspNetCore.Components.WebAssembly.Authentication/AuthenticationService.js" || x[i].src == host + "_framework/blazor.webassembly.js" || x[i].src == host + "js/scriptLoader.js" || x[i].src == host + "js/StyleLoader.js" || x[i].src == host + "js/site.js") {
    //        continue;
    //    }
    //    else
    //        x[i].parentNode.removeChild(x[i]);
    //}

}
window.RemoveScript = function (ScriptPath) {
    if (ScriptPath) {
        var sheets = document.getElementsByTagName('script');
        for (var i = 0; i < sheets.length; i++) {
            if (sheets[i].href == ScriptPath) {
                document["body"].removeChild(sheets[i]);
            }
        }
    }
   
}
