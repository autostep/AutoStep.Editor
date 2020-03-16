using AutoStep.Editor.Client.Store.CodeWindow;
using AutoStep.Language;
using AutoStep.Monaco.Interop;
using AutoStep.Projects;
using Blazor.Fluxor;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.Extensions.Logging;

namespace AutoStep.Editor.Client.Store.App
{
    /// <summary>
    /// Action occurs when a project has been compiled.
    /// </summary>
    internal class ProjectCompiledEffect : Effect<ProjectCompiledAction>
    {
        private readonly IState<AppState> state;
        private readonly ILogger<ProjectCompiledEffect> logger;

        public ProjectCompiledEffect(IState<AppState> state, ILogger<ProjectCompiledEffect> logger)
        {
            this.state = state;
            this.logger = logger;
        }

        protected override async Task HandleAsync(ProjectCompiledAction action, IDispatcher dispatcher)
        {
            // Go through the files.
            foreach (var file in state.Value.Files)
            {
                if (await UpdateMarkers(file.Value))
                {
                    // Dispatch an update to the file state.
                    dispatcher.Dispatch(new FileModelUpdateAction(file.Key, DateTime.UtcNow));
                }
            }
        }

        private async ValueTask<bool> UpdateMarkers(ProjectFileState fileState)
        {
            var lastUpdateTime = fileState.LastModelUpdate;

            var file = fileState.File;

            LanguageOperationResult primary = null;
            LanguageOperationResult secondary = null;
            DateTime latestUpdateTime = DateTime.MinValue;

            if (file is AutoStep.Projects.ProjectInteractionFile interactionFile)
            {
                primary = interactionFile.LastCompileResult;
                latestUpdateTime = interactionFile.LastCompileTime;

                secondary = interactionFile.LastSetBuildResult;

                if (interactionFile.LastSetBuildTime > latestUpdateTime)
                {
                    latestUpdateTime = interactionFile.LastSetBuildTime;
                }
            }
            else if (file is AutoStep.Projects.ProjectTestFile testFile)
            {
                primary = testFile.LastCompileResult;
                latestUpdateTime = testFile.LastCompileTime;

                secondary = testFile.LastLinkResult;

                if (testFile.LastLinkTime.GetValueOrDefault() > latestUpdateTime)
                {
                    latestUpdateTime = testFile.LastLinkTime.Value;
                }
            }

            if (latestUpdateTime > fileState.LastModelUpdate)
            {
                logger.LogTrace("Updating Markers for {0}", fileState.FileUri);

                IEnumerable<LanguageOperationMessage> messages;

                if (primary is null)
                {
                    messages = Enumerable.Empty<LanguageOperationMessage>();
                }
                else
                {
                    messages = primary.Messages;

                    if (secondary is object)
                    {
                        messages = messages.Concat(secondary.Messages);
                    }
                }

                logger.LogTrace("Found {0} Markers for {1}", messages.Count(), fileState.FileUri);

                var markers = messages.Select(m => GetMarkerDataFromMessage(m));

                await fileState.TextModel.SetMarkers("autostep", markers);

                return true;
            }
            else
            {
                logger.LogTrace("Not Updating Markers; file not updated", fileState.FileUri);
            }

            return false;
        }

        private static MarkerData GetMarkerDataFromMessage(AutoStep.Language.LanguageOperationMessage msg)
        {
            var severity = msg.Level switch
            {
                CompilerMessageLevel.Error => MarkerSeverity.Error,
                CompilerMessageLevel.Warning => MarkerSeverity.Warning,
                _ => MarkerSeverity.Info
            };

            var endPosition = msg.EndColumn;

            if (endPosition is null)
            {
                endPosition = msg.StartColumn;
            }
            else
            {
                endPosition++;
            }

            // Expand message end to the location after the token
            return new MarkerData($"ASC{(int)msg.Code:D5}", msg.Message, severity, msg.StartColumn, msg.StartLineNo, endPosition.Value, msg.EndLineNo ?? msg.StartLineNo);
        }
    }
}
