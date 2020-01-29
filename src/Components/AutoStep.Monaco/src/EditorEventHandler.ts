import { IBlazorInteropObject } from './IBlazorInteropObject';

export class EditorEventHandler {
    eventSink: IBlazorInteropObject;

    constructor(eventSink: IBlazorInteropObject) {
        this.eventSink = eventSink;
    }
}