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
            return action switch
            {
                ICodeWindowAction codeWindowAction =>  new AppState(codeWindowReducer.Reduce(state.CodeWindow, codeWindowAction)),
                _ => state
            };
        }
    }
}
