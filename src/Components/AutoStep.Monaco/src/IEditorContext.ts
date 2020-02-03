import * as monaco from 'monaco-editor/esm/vs/editor/editor.api';
import { EditorEventHandler } from './EditorEventHandler';
import { IBlazorInteropObject } from './IBlazorInteropObject';

/**
 * Context structure for a code editor.
 */
export interface IEditorContext 
{
    id: string;
    codeEditor: monaco.editor.IStandaloneCodeEditor;
    updating: boolean;
    eventHandler?: EditorEventHandler;
    eventSink?: IBlazorInteropObject;
}
