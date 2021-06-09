class Globals {
    constructor() {
        this.ServerURL = "http://localhost:24443/";
        
        this.SerializeFormToJSON = function(formName) {
            var array = $('#' + formName).serialize().split('&');
            var object = {};
            for (var i = 0; i < array.length; i++) {
                var sObj = array[i].split('=');
                object[sObj[0]] = decodeURIComponent(sObj[1]);
            }
            var json = JSON.stringify(object);
            return json;
        };
        this.SerializeFormToJSONObject = function(formName) {
            var array = $('#' + formName).serialize().split('&');
            var object = {};
            for (var i = 0; i < array.length; i++) {
                var sObj = array[i].split('=');
                object[sObj[0]] = decodeURIComponent(sObj[1]);
            }
            var json = JSON.stringify(object);
            return JSON.parse(json);
        };
        this.FormToJSON = function(formData) {
            var object = {};
            formData.forEach((value, key) => { object[key] = value; });
            var json = JSON.stringify(object);
            return json;
        };
        this.FormToJSONObject = function(formData) {
            var object = {};
            formData.forEach((value, key) => { object[key] = value; });
            var json = JSON.stringify(object);
            return JSON.parse(json);
        };
        this.GetURLParameter = function(param) {
            var vars = [];
            var hash = [];
            var hashes = window.location.href.slice(window.location.href.indexOf('?') + 1).split('&');
            for (var i = 0; i < hashes.length; i++) {
                hash = hashes[i].split('=');
                vars.push(hash[0]);
                vars[hash[0]] = hash[1];
            }

            var val = vars[param];

            return val;
        }
    }
}