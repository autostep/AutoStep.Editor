using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using AutoStep.Editor.Client.Language;
using AutoStep.Editor.Client.Store.CodeWindow;
using AutoStep.Editor.Client.Store.FileView;
using AutoStep.Projects;
using Blazor.Fluxor;
using Microsoft.Extensions.Logging;

namespace AutoStep.Editor.Client.Store.App
{
    /// <summary>
    /// Reducer for the top-level application state. Does composite model reduction by referencing other reducers.
    /// </summary>
    internal class AppReducer : Reducer<AppState, IAutoStepAction>
    {
        private readonly ILogger<AppReducer> logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="AppReducer"/> class.
        /// </summary>
        /// <param name="codeWindowReducer">The code window reducer.</param>
        /// <param name="logger">A logger.</param>
        public AppReducer(ILogger<AppReducer> logger)
        {
            this.logger = logger;
        }

        /// <inheritdoc/>
        public override AppState Reduce(AppState state, IAutoStepAction action)
        {
            logger.LogTrace(LogMessages.AppReducer_Reduce, action.GetType().ToString());

            return action switch
            {
                OpenFileAction openFileAction => OpenFile(state, openFileAction),
                SwitchProjectAction changeProjectAction =>
                    new AppState(
                        changeProjectAction.NewProject,
                        ImmutableDictionary<string, ProjectFileState>.Empty,
                        ImmutableDictionary<Guid, FileViewState>.Empty),
                //ProjectCompiledAction compiled => state,
                //ICodeWindowAction codeWindowAction => new AppState(state.Project, codeWindowReducer.Reduce(state.CodeWindow, codeWindowAction)),
                _ => state
            };
        }

        private AppState OpenFile(AppState state, OpenFileAction openFileAction)
        {
            var fileViewState = new FileViewState(Guid.NewGuid(), openFileAction.FileId);

            var fileState = new ProjectFileState(
                                new Uri($"file://{openFileAction.FileId}"),
                                state.Project.AllFiles[openFileAction.FileId],
                                state.Project.AllFiles[openFileAction.FileId].ContentSource as ProjectFileSource,
                                false);

            return new AppState(
                        state.Project,
                        state.Files.Add(openFileAction.FileId, fileState),
                        state.FileViews.Add(fileViewState.Id, fileViewState));
        }
    }
}
