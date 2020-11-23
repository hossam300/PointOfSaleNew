window.loadStyle = function (StylePath) {

    // check list - if already loaded we can ignore
    if (loaded[StylePath]) {
        console.log(StylePath + " already loaded");
        // return 'empty' promise
        return new this.Promise(function (resolve, reject) {
            resolve();
        });
    }

    return new Promise(function (resolve, reject) {
        // create JS library Style element
        var Style = document.createElement("link");
        Style.href = StylePath;
        Style.type = "text/css";
        Style.rel = "stylesheet";
        console.log(StylePath + " created");

        // flag as loading/loaded
        loaded[StylePath] = true;

        // if the Style returns okay, return resolve
        Style.onload = function () {
            console.log(StylePath + " loaded ok");
            resolve(StylePath);
        };

        // if it fails, return reject
        Style.onerror = function () {
            console.log(StylePath + " load failed");
            reject(StylePath);
        }

        // Styles will load at end of body
        document["head"].appendChild(Style);
    });
}
// store list of what Styles we've loaded
loaded = [];
window.ChangeToPosTemplate = function () {
    StartLoading();
    removeStyle();

    var addedstyle = [
        '/css/POSStyle.css',
        '/font-awesome/css/font-awesome.min.css'];

    for (var j = 0; j < addedstyle.length; j++) {
        loadStyle(addedstyle[j]);
    }
    setTimeout(function () { StopLoading(); }, 3000);
}
window.ChangeToMainTemplate = function () {
    StartLoading();
    removeStyle();
    var addedstyle = [
        
        '/assets/plugins/custom/datatables/datatables.bundle.css',
        '/assets/plugins/global/plugins.bundle.css',
        '/assets/plugins/custom/prismjs/prismjs.bundle.css',
        '/assets/css/style.bundle.css'];
    for (var j = 0; j < addedstyle.length; j++) {
        loadStyle(addedstyle[j]);
    }
    setTimeout(function () { StopLoading(); }, 3000);
}
window.removeStyle = function () {
    var sheets = document.getElementsByTagName('link');
    var host = location.protocol + '//' + location.host + "/";
    for (var i = 0; i < sheets.length; i++) {
        if (sheets[i].href == host + "css/site.css" || sheets[i].href == host + "_content/Radzen.Blazor/css/default.css") {
            continue;
        }
        else
            sheets[i].remove();
    }
}

window.RemoveStyle = function (StylePath) {
    if (StylePath) {
        var sheets = document.getElementsByTagName('link');
        for (var i = 0; i < sheets.length; i++) {
            if (sheets[i].href == StylePath) {
                sheets[i].remove();
            }
        }
    }

}