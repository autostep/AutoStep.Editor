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
                OpenFileCompleteAction openFileCompleteAction => OpenFileComplete(state, openFileCompleteAction),
                FileModelUpdateAction fileModelUpdateAction => FileModelUpdate(state, fileModelUpdateAction),
                SwitchProjectAction changeProjectAction =>
                    new AppState(
                        changeProjectAction.NewProject,
                        ImmutableDictionary<string, ProjectFileState>.Empty,
                        ImmutableDictionary<Guid, FileViewState>.Empty),
                ProjectCompiledAction _ => new AppState(state.Project, state.Files, state.FileViews),
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
                                true,
                                null,
                                DateTime.MinValue);

            return new AppState(
                        state.Project,
                        state.Files.Add(openFileAction.FileId, fileState),
                        state.FileViews.Add(fileViewState.Id, fileViewState));
        }

        private AppState OpenFileComplete(AppState state, OpenFileCompleteAction action)
        {
            var fileState = state.Files[action.FileId];

            var newFileState = new ProjectFileState(
                                fileState.FileUri,
                                fileState.File,
                                fileState.Source,
                                false,
                                action.TextModel,
                                DateTime.MinValue);

            var files = state.Files.SetItem(action.FileId, newFileState);

            return new AppState(
                state.Project,
                files,
                state.FileViews
            );
        }

        private AppState FileModelUpdate(AppState state, FileModelUpdateAction action)
        {
            var fileState = state.Files[action.FileId];

            var newFileState = new ProjectFileState(
                                fileState.FileUri,
                                fileState.File,
                                fileState.Source,
                                false,
                                fileState.TextModel,
                                action.UpdatedTime
                                );

            var files = state.Files.SetItem(action.FileId, newFileState);

            return new AppState(
                state.Project,
                files,
                state.FileViews
            );
        }
    }
}
