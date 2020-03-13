using System.Threading.Tasks;
using AutoStep.Editor.Client.Store.App;
using AutoStep.Editor.Client.Store.CodeWindow;
using Blazor.Fluxor;
using Microsoft.Extensions.Logging;

namespace AutoStep.Editor.Client.Store.FileView
{
    /// <summary>
    /// Occurs when a file is changed in the editor.
    /// </summary>
    internal class OpenFileEffect : Effect<OpenFileAction>
    {
        private readonly ILoggerFactory loggerFactory;
        private readonly IState<AppState> state;

        /// <summary>
        /// Initializes a new instance of the <see cref="OpenFileEffect"/> class.
        /// </summary>
        /// <param name="loggerFactory">The logging factory.</param>
        public OpenFileEffect(ILoggerFactory loggerFactory, IState<AppState> state)
        {
            this.loggerFactory = loggerFactory;
            this.state = state;
        }

        /// <inheritdoc/>
        protected override async Task HandleAsync(OpenFileAction action, IDispatcher dispatcher)
        {
            var fileId = action.FileId;

            var file = state.Value.Files[fileId];

            // Refresh the latest content (this will probably do an HTTP request or file read).
            await file.Source.GetContentAsync();

            // Dispatch the load complete action (file can now be displayed).
            dispatcher.Dispatch(new OpenFileCompleteAction(file));
        }
    }
}
