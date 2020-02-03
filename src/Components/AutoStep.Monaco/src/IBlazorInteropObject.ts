/**
 * Interface that defines the useful methods on the .NET object reference passed by Blazor.
 */
export interface IBlazorInteropObject {
    invokeMethodAsync<T>(methodName: string, ...args: any[]): Promise<T>;
    invokeMethod<T>(methodName: string, ...args: any[]): T;

}