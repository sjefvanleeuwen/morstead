/// <reference path="ace.min.js" />

function createEditor() {
    ace.config.set('basePath', '/js/ace');
    var editor = ace.edit("editor");
    editor.setTheme("ace/theme/chaos");
    editor.session.setMode("ace/mode/yaml");
}