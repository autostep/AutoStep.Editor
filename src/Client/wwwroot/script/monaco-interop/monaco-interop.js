window.monacoInterop = (function (my) {

    var editors = {};

    my.initialiseEditor = function (id, container) {
        console.log("initialiseEditor");
        return new Promise((resolve, reject) => {
            require(['vs/editor/editor.main'], function () {
                console.log("Require Complete");
                editors[id] = monaco.editor.create(container);
                console.log("Editor Created");
                resolve();
            });
        });
    };

    my.setContent = function (id, value) {
        console.log("Set Value");
        editors[id].setValue(value);
    };

    return my;

})({});