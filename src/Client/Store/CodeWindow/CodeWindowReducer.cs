using AutoStep.Editor.Client.Store.App;
using Blazor.Fluxor;

namespace AutoStep.Editor.Client.Store.CodeWindow
{
    /// <summary>
    /// Reducer for Code Window state changes.
    /// </summary>
    internal class CodeWindowReducer : Reducer<CodeWindowState, ICodeWindowAction>
    {
        /// <inheritdoc/>
        public override CodeWindowState Reduce(CodeWindowState state, ICodeWindowAction action)
        {
            return action switch
            {
                ChangeFileAction load => new CodeWindowState(load.NewFile, true),
                LoadCodeCompleteAction loaded => new CodeWindowState(loaded.File, false),
                CodeChangeAction codeChange => new CodeWindowState(codeChange.File, state.IsLoading),
                ProjectCompiledAction _ => new CodeWindowState(state.DisplayedFile, state.IsLoading),

                // No change
                _ => state,
            };
        }
    }
}
