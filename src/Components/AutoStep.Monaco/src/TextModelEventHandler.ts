import { IBlazorInteropObject } from './IBlazorInteropObject';

/**
 * Text model event handler.
 */
export class TextModelEventHandler {
    eventSink: IBlazorInteropObject;

    constructor(eventSink: IBlazorInteropObject) {
        this.eventSink = eventSink;
    }

    public modelUpdated(newValue: string)
    {
        this.eventSink.invokeMethodAsync("ModelUpdated", newValue);
    }
}