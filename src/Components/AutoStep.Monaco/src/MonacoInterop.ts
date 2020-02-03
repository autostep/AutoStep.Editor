import { editor, languages, Uri } from 'monaco-editor/esm/vs/editor/editor.api';
import { IEditorContext } from './IEditorContext';
import { ITextModelContext } from './ITextModelContext';
import { EditorEventHandler } from './EditorEventHandler';
import { TextModelEventHandler } from './TextModelEventHandler';
import { IBlazorInteropObject } from './IBlazorInteropObject';
import { AutoStepTokenProvider } from './AutoStepTokenProvider';

// Initialise the Monaco Environment with the relative URL.
// @ts-ignore
self.MonacoEnvironment = {
    getWorkerUrl: function (moduleId, label) {
        return "./_content/Autostep.Monaco/editor.worker.bundle.js";
    }
};

/**
 * Monaco Interop TypeScript
 * */
class MonacoInterop {

    /**
     * Set of created editors
     */
    private editors: { [id: string]: IEditorContext } = {};
    /**
     * Set of created text models
     */
    private models: { [uri: string]: ITextModelContext } = {};
    
    constructor()
    {
        // Set up my custom there (I may move this later)
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

    /**
     * Create an editor control.
     * @param id The id of the control (to reference it later)
     * @param container The HTML Element container.
     * @param blazorCallback An object on which to invoke event methods.
     */
    createEditor(id: string, container: HTMLElement, blazorCallback: IBlazorInteropObject) {
        
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

    /**
     * Create a new text model.
     * @param uri The URI of the model.
     * @param value The content of the model.
     * @param blazorCallback An object on which to call event handling methods.
     * @param language The ID of the model's language.
     */
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

    /**
     * Register a language and associated tokeniser.
     * @param languageId The language ID.
     * @param extension The file extension.
     * @param tokenizer The tokenizer.
     */
    registerLanguageTokenizer(languageId: string, extension: string, tokenizer: IBlazorInteropObject)
    {
        languages.register({
            id: languageId,
            extensions: [extension]
        });

        var tokenProvider = new AutoStepTokenProvider(tokenizer);

        languages.setTokensProvider(languageId, tokenProvider);
    }

    /**
     * Set the model markers for a text model.
     * @param textModelUri The URI of the text model.
     * @param owner The owner of the markers.
     * @param markers The full set of new markers for the model.
     */
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

    /**
     * Sets the content for a model.
     * @param textModelUri The text model URI.
     * @param newContent The new content of the model.
     */
    setModelContent(textModelUri: string, newContent: string)
    {
        var modelCtxt = this.models[textModelUri];

        if (!modelCtxt) {
            throw "Specified model not created.";
        }

        modelCtxt.textModel.setValue(newContent);
    }

    /**
     * Change the model an editor is displaying.
     * @param editorId The ID of the editor.
     * @param textModelUri The URI of the text model.
     */
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
