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
                ChangeFileAction load => new CodeWindowState(load.NewFile, true),
                LoadCodeCompleteAction loaded => new CodeWindowState(loaded.File, false),
                CodeChangeAction codeChange => new CodeWindowState(codeChange.File, state.IsLoading),
                ProjectCompiledAction compiledAction => new CodeWindowState(state.DisplayedFile, state.IsLoading),
                // No change
                _ => state 
            };
        }
    }
}
