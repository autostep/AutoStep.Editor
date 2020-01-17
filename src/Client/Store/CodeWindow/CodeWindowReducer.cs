using Blazor.Fluxor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutoStep.Editor.Client.Store.CodeWindow
{
    public class CodeWindowReducer : Reducer<CodeWindowState, ICodeWindowAction>
    {
        public override CodeWindowState Reduce(CodeWindowState state, ICodeWindowAction action)
        {
            return action switch
            {
                LoadCodeAction load => new CodeWindowState(state.CodeBody, load.SourceName, true),
                LoadCodeCompleteAction loaded => new CodeWindowState(loaded.Body, state.SourceName, false),

                _ => throw new InvalidOperationException("Unexpected reducer")
            };
        }
    }
}
