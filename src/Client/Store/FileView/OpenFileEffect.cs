using System.Threading.Tasks;
using AutoStep.Editor.Client.Store.App;
using AutoStep.Editor.Client.Store.CodeWindow;
using AutoStep.Monaco.Interop;
using AutoStep.Projects;
using Blazor.Fluxor;
using Microsoft.Extensions.Logging;

namespace AutoStep.Editor.Client.Store.FileView
{
    /// <summary>
    /// Occurs when a file is changed in the editor.
    /// </summary>
    internal class OpenFileEffect : Effect<OpenFileAction>
    {
        private readonly ILogger<OpenFileEffect> logger;
        private readonly IState<AppState> state;
        private readonly MonacoInterop monaco;

        /// <summary>
        /// Initializes a new instance of the <see cref="OpenFileEffect"/> class.
        /// </summary>
        /// <param name="loggerFactory">The logging factory.</param>
        public OpenFileEffect(ILoggerFactory loggerFactory, IState<AppState> state, MonacoInterop monaco)
        {
            this.logger = loggerFactory.CreateLogger<OpenFileEffect>();
            this.state = state;
            this.monaco = monaco;
        }

        /// <inheritdoc/>
        protected override async Task HandleAsync(OpenFileAction action, IDispatcher dispatcher)
        {
            var fileId = action.FileId;

            var file = state.Value.Files[fileId];

            // Refresh the latest content (this will probably do an HTTP request or file read).
            var content = await file.Source.GetContentAsync();

            var languageId = file.File is ProjectInteractionFile ? "autostep-interaction" : "autostep";

            var textModel = await monaco.CreateTextModel(file.FileUri, content, languageId);

            textModel.OnModelChanged += (object sender, string args) =>
            {
                dispatcher.Dispatch(new CodeChangeAction(action.Project, fileId, args));
            };

            logger.LogTrace("Dispatching Open File Complete");

            // Dispatch the load complete action (file can now be displayed).
            dispatcher.Dispatch(new OpenFileCompleteAction(fileId, textModel));
        }
    }
}
