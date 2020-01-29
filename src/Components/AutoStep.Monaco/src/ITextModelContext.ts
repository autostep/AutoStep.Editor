import { editor } from 'monaco-editor/esm/vs/editor/editor.api';
import { IBlazorInteropObject } from './IBlazorInteropObject';
import { TextModelEventHandler } from './TextModelEventHandler';

export interface ITextModelContext {
    textModel: editor.ITextModel;
    updating: boolean;
    changeTimer: any;
    eventHandler?: TextModelEventHandler
    eventSink?: IBlazorInteropObject;
}
