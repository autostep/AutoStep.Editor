export interface IBlazorInteropObject {
    invokeMethodAsync<T>(methodName: string, ...args: any[]): Promise<T>;
}

export class EventHandler {
    eventSink: IBlazorInteropObject;

    constructor(eventSink: IBlazorInteropObject) {
        this.eventSink = eventSink;
    }

    editorContentChanged(newContent: string): boolean {
        this.eventSink.invokeMethodAsync<void>('EditorContentChanged', newContent);

        return false;
    }
}