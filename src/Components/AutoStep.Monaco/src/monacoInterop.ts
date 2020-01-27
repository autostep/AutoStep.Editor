import * as monaco from 'monaco-editor/esm/vs/editor/editor.api';
import { IEditorContext } from './IEditorContext';
import { IBlazorInteropObject, EventHandler } from './EventHandler';

// @ts-ignore
self.MonacoEnvironment = {
    getWorkerUrl: function(moduleId, label) {
      return "./editor.worker.bundle.js";
    }
};

class MonacoInterop 
{
  editors: { [id: string]: IEditorContext } = {};

  initialiseEditor(id: string, container: HTMLElement, blazorCallback: IBlazorInteropObject)
  {
    console.log("initialiseEditor");

    var editor = monaco.editor.create(container);

    var editorContext : IEditorContext = {
      id: id,
      codeEditor: editor,
      updating: false,
      eventHandler: new EventHandler(blazorCallback),
      eventSink: blazorCallback
    };

    this.editors[id] = editorContext;

    editor.onDidChangeModelContent(context => {
      if (!editorContext.updating)
      {
        var editorBody = editorContext.codeEditor.getValue();

        editorContext.eventHandler.editorContentChanged(editorBody);
      }
    })
  }

  setContent(editorId: string, value: string)
  {
    var ctxt = this.editors[editorId];

    ctxt.updating = true;
    ctxt.codeEditor.setValue(value);
    ctxt.updating = false;
  }
}

// This is what we'll export to the 'window' for monaco
// interop work.
window['monacoInterop'] = new MonacoInterop();