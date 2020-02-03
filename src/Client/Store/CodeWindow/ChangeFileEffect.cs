using System.Threading.Tasks;
using AutoStep.Editor.Client.Store.App;
using Blazor.Fluxor;
using Microsoft.Extensions.Logging;

namespace AutoStep.Editor.Client.Store.CodeWindow
{
    /// <summary>
    /// Occurs when a file is changed in the editor.
    /// </summary>
    internal class ChangeFileEffect : Effect<ChangeFileAction>
    {
        private readonly ILoggerFactory loggerFactory;

        /// <summary>
        /// Initializes a new instance of the <see cref="ChangeFileEffect"/> class.
        /// </summary>
        /// <param name="loggerFactory">The logging factory.</param>
        public ChangeFileEffect(ILoggerFactory loggerFactory)
        {
            this.loggerFactory = loggerFactory;
        }

        /// <inheritdoc/>
        protected override async Task HandleAsync(ChangeFileAction action, IDispatcher dispatcher)
        {
            // Refresh the latest content (this will probably do an HTTP request or file read).
            await action.NewFile.Source.GetContentAsync();

            // Dispatch the load complete action (file can now be displayed).
            dispatcher.Dispatch(new LoadCodeCompleteAction(action.NewFile));

            // Initiate compilation.
            var projectCompiler = action.Project.Compiler;
            await projectCompiler.Compile(loggerFactory);

            // Do a link operation.
            projectCompiler.Link();

            // Dispatch the project compiled action.
            dispatcher.Dispatch(new ProjectCompiledAction(action.Project));
        }
    }
}
