export interface IBlazorInteropObject {
    invokeMethodAsync<T>(methodName: string, ...args: any[]): Promise<T>;
    invokeMethod<T>(methodName: string, ...args: any[]): T;

}