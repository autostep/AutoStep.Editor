import * as monaco from 'monaco-editor/esm/vs/editor/editor.api';
import { IBlazorInteropObject, EventHandler } from './EventHandler';

export interface IEditorContext 
{
    id: string;
    codeEditor: monaco.editor.IStandaloneCodeEditor;
    updating: boolean;
    eventHandler?: EventHandler
    eventSink?: IBlazorInteropObject;
}
