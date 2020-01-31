using AutoStep.Editor.Client.Store.CodeWindow;
using Blazor.Fluxor;
using System;

namespace AutoStep.Editor.Client.Store.App
{
    public class AppReducer : Reducer<AppState, IAutoStepAction>
    {
        private readonly CodeWindowReducer codeWindowReducer;

        public AppReducer(CodeWindowReducer codeWindowReducer)
        {
            this.codeWindowReducer = codeWindowReducer;
        }

        public override AppState Reduce(AppState state, IAutoStepAction action)
        {
            Console.WriteLine("Reducer:" + action.GetType().ToString());

            return action switch
            {
                SwitchProjectAction changeProjectAction => new AppState(changeProjectAction.NewProject, state.CodeWindow),
                ProjectCompiledAction compiled => new AppState(state.Project, codeWindowReducer.Reduce(state.CodeWindow, compiled)),
                ICodeWindowAction codeWindowAction => new AppState(state.Project, codeWindowReducer.Reduce(state.CodeWindow, codeWindowAction)),
                _ => new AppState(state.Project, state.CodeWindow)
            };
        }
    }
}
