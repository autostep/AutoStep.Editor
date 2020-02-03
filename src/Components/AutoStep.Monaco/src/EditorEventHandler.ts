import { IBlazorInteropObject } from './IBlazorInteropObject';

/**
 * Editor Event Handler.
 */
export class EditorEventHandler {
    eventSink: IBlazorInteropObject;

    constructor(eventSink: IBlazorInteropObject) {
        this.eventSink = eventSink;
    }
}