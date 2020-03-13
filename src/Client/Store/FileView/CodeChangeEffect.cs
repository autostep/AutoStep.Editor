using System.Threading.Tasks;
using AutoStep.Editor.Client.Store.App;
using Blazor.Fluxor;
using Microsoft.Extensions.Logging;

namespace AutoStep.Editor.Client.Store.CodeWindow
{
    /// <summary>
    /// Occurs when the code in a local text model changes.
    /// </summary>
    internal class CodeChangeEffect : Effect<CodeChangeAction>
    {
        private readonly ILoggerFactory logFactory;
        private readonly IState<AppState> state;

        /// <summary>
        /// Initializes a new instance of the <see cref="CodeChangeEffect"/> class.
        /// </summary>
        /// <param name="logFactory">The logger facotry.</param>
        public CodeChangeEffect(ILoggerFactory logFactory, IState<AppState> state)
        {
            this.logFactory = logFactory;
            this.state = state;
        }

        /// <inheritdoc/>
        protected override async Task HandleAsync(CodeChangeAction action, IDispatcher dispatcher)
        {
            var file = state.Value.Files[action.FileId];

            // Update the local version of the 'body'.
            file.Source.UpdateLocalBody(action.Body);

            // Initiate compilation and linking.
            var projectCompiler = action.Project.Compiler;

            await projectCompiler.CompileAsync(logFactory);

            projectCompiler.Link();

            dispatcher.Dispatch(new ProjectCompiledAction(action.Project));
        }
    }
}
