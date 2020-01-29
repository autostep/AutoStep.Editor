using Blazor.Fluxor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutoStep.Editor.Client.Store.CodeWindow
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
