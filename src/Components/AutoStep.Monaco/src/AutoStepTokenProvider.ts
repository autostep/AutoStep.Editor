import { IBlazorInteropObject } from './IBlazorInteropObject';
import { languages } from 'monaco-editor/esm/vs/editor/editor.api';

/**
 * AutoStep token state consists solely of a number.
 */
class AutoStepTokenState implements languages.IState {
    tokenState: number;

    /**
     * Create a new token state.
     * @param state State value.
     */
    constructor(state: number) {
        this.tokenState = state;
    }

    clone(): languages.IState {
        return new AutoStepTokenState(this.tokenState);
    }

    equals(other: languages.IState): boolean {
        if (other instanceof AutoStepTokenState) {
            return other.tokenState == this.tokenState;
        }

        return false;
    }
}

/**
 *  Implementation of the Monaco Token Provider.
 */
export class AutoStepTokenProvider implements languages.TokensProvider {
    private callback: IBlazorInteropObject;

    constructor(blazorCallback: IBlazorInteropObject) {
        this.callback = blazorCallback;
    }

    getInitialState(): languages.IState {
        return new AutoStepTokenState(this.callback.invokeMethod<number>("GetInitialState"));
    }

    tokenize(line: string, state: languages.IState): languages.ILineTokens {

        if (state instanceof AutoStepTokenState)
        {
            // Invoke the .NET method.
            var result: any = this.callback.invokeMethod("Tokenize", line, state.tokenState);

            return { tokens: result.tokens, endState: new AutoStepTokenState(result.endState) };
        }

        throw "Invalid start state";
    }
}
