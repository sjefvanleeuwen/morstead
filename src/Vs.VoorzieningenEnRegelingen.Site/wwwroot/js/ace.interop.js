/// <reference path="ace.min.js" />
var editor;
var instance;
var selection;

function createEditor(caller) {
    instance = caller;
    ace.config.set('basePath', '/js/ace');
    editor = ace.edit("editor");
    editor.setTheme("ace/theme/chaos");
    editor.session.setMode("ace/mode/yaml");

    editor.selection.on("changeCursor", function () {
        instance.invokeMethodAsync('OnChange', editor.getCursorPosition()).then(result => {
            console.log(result);
        });
    });
    editor.session.on("change", function (delta) {
        instance.invokeMethodAsync('OnChange', delta);
    });
}

