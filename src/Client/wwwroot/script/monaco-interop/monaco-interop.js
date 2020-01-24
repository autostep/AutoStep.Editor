window.monacoInterop = (function (my) {

    var editors = {};

    var editorModifyFlags = {};

    my.initialiseEditor = function (id, container, monacoEventHandler) {
        console.log("initialiseEditor");
        return new Promise((resolve, reject) => {
            require(['vs/editor/editor.main'], function () {
                console.log("Require Complete");
                editors[id] = monaco.editor.create(container);
                console.log("Editor Created");
                editors[id].onDidChangeModelContent(content => {
                    if (!editorModifyFlags[id]) {
                        var editorBody = editors[id].getValue();

                        monacoEventHandler.invokeMethodAsync('EditorContentChanged', editorBody);
                    }
                });
                resolve();
            });
        });
    };

    my.setContent = function (id, value) {
        console.log("Set Value");
        editorModifyFlags[id] = true;
        editors[id].setValue(value);
        editorModifyFlags[id] = false;
    };

    return my;

})({});