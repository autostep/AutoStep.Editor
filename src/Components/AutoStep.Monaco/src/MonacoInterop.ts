import { editor, languages, Uri } from 'monaco-editor/esm/vs/editor/editor.api';
import { IEditorContext } from './IEditorContext';
import { ITextModelContext } from './ITextModelContext';
import { EditorEventHandler } from './EditorEventHandler';
import { TextModelEventHandler } from './TextModelEventHandler';
import { IBlazorInteropObject } from './IBlazorInteropObject';
import { AutoStepTokenProvider } from './AutoStepTokenProvider';

// @ts-ignore
self.MonacoEnvironment = {
    getWorkerUrl: function (moduleId, label) {
        return "./_content/Autostep.Monaco/editor.worker.bundle.js";
    }
};

class MonacoInterop {

    private editors: { [id: string]: IEditorContext } = {};
    private models: { [uri: string]: ITextModelContext } = {};
    
    constructor()
    {
        editor.defineTheme('autostep', {
            base: 'vs',
            inherit: true,
            rules: [
                { token: "markup.italic", fontStyle: 'italic' },
                { token: "string.variable", fontStyle: 'italic' },
                { token: "variable", fontStyle: 'italic' },
                { token: "entity.step.text.unbound", foreground: '#969696' },
                { token: "entity.annotation.opt", foreground: '#fbad38' },
                { token: "entity.annotation.tag", foreground: '#fbad38' }
            ],
            colors: {} 
        });

        editor.setTheme('autostep');
    }

    createEditor(id: string, container: HTMLElement, themeId: string, blazorCallback: IBlazorInteropObject) {
        console.log("createEditor");

        var newEditor = editor.create(container);

        var editorContext: IEditorContext = {
            id: id,
            codeEditor: newEditor,
            updating: false,
            eventHandler: new EditorEventHandler(blazorCallback),
            eventSink: blazorCallback
        };

        this.editors[id] = editorContext;
    }

    createTextModel(uri: string, value: string, blazorCallback: IBlazorInteropObject, language?: string)
    {
        var monacoUri = Uri.parse(uri);

        var model = editor.createModel(value, language, monacoUri);

        var modelContext: ITextModelContext = {
            textModel: model,
            changeTimer: 0,
            updating: false,
            eventHandler: new TextModelEventHandler(blazorCallback),
            eventSink: blazorCallback
        };

        // Attach events to the model.
        model.onDidChangeContent(ev => {

            if (modelContext.changeTimer)
            {
                clearTimeout(modelContext.changeTimer);
                modelContext.changeTimer = 0;
            }

            modelContext.changeTimer = setTimeout(() => {

                modelContext.changeTimer = 0;
                // Wait 1 second for someone to finish typing,
                // then raise the event.
                modelContext.eventHandler.modelUpdated(model.getValue());

            }, 1000);
        });

        this.models[uri] = modelContext;
    }

    registerLanguage(languageId: string, extension: string, blazorCallback: IBlazorInteropObject)
    {
        languages.register({
            id: languageId,
            extensions: [extension]
        });

        var tokenProvider = new AutoStepTokenProvider(blazorCallback);

        languages.setTokensProvider(languageId, tokenProvider);
    }

    registerTokenTheme(themeId: string, rulesJson: string)
    {
        var rules : any[] = JSON.parse(rulesJson);
    }

    setModelMarkers(textModelUri: string, owner: string, markers: editor.IMarkerData[])
    {
        var modelCtxt = this.models[textModelUri];

        if (!modelCtxt) {
            throw "Specified model not created.";
        }

        editor.setModelMarkers(modelCtxt.textModel, owner, markers);

        // Force a background re-tokenise when we get the model markers through, because compilation changes may have caused
        // everything to change.
        var unsafeModel: any = modelCtxt.textModel;
        unsafeModel._tokenization._resetTokenizationState();
    }

    setModelContent(textModelUri: string, newContent: string)
    {
        var modelCtxt = this.models[textModelUri];

        if (!modelCtxt) {
            throw "Specified model not created.";
        }

        modelCtxt.textModel.setValue(newContent);
    }

    setEditorModel(editorId: string, textModelUri: string)
    {
        var editorCtxt = this.editors[editorId];
        var modelCtxt = this.models[textModelUri];

        if (!editorCtxt)
        {
            throw "Specified editor not created.";
        }

        if (!modelCtxt)
        {
            throw "Specified model not created.";
        }

        editorCtxt.codeEditor.setModel(modelCtxt.textModel);
    }
}

// This is what we'll export to the 'window' for monaco
// interop work.
window['monacoInterop'] = new MonacoInterop();
