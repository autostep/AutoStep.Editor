using System;
using AutoStep.Editor.Client.Store.CodeWindow;
using Blazor.Fluxor;
using Microsoft.Extensions.Logging;

namespace AutoStep.Editor.Client.Store.App
{
    /// <summary>
    /// Reducer for the top-level application state. Does composite model reduction by referencing other reducers.
    /// </summary>
    internal class AppReducer : Reducer<AppState, IAutoStepAction>
    {
        private readonly CodeWindowReducer codeWindowReducer;
        private readonly ILogger<AppReducer> logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="AppReducer"/> class.
        /// </summary>
        /// <param name="codeWindowReducer">The code window reducer.</param>
        /// <param name="logger">A logger.</param>
        public AppReducer(CodeWindowReducer codeWindowReducer, ILogger<AppReducer> logger)
        {
            this.codeWindowReducer = codeWindowReducer;
            this.logger = logger;
        }

        /// <inheritdoc/>
        public override AppState Reduce(AppState state, IAutoStepAction action)
        {
            logger.LogTrace(LogMessages.AppReducer_Reduce, action.GetType().ToString());

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
