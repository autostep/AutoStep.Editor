using System.Threading.Tasks;
using AutoStep.Editor.Client.Language;
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
        private readonly IState<AppState> state;
        private readonly CompilationService compilation;

        /// <summary>
        /// Initializes a new instance of the <see cref="CodeChangeEffect"/> class.
        /// </summary>
        /// <param name="logFactory">The logger facotry.</param>
        public CodeChangeEffect(IState<AppState> state, CompilationService compilation)
        {
            this.state = state;
            this.compilation = compilation;
        }

        /// <inheritdoc/>
        protected override async Task HandleAsync(CodeChangeAction action, IDispatcher dispatcher)
        {
            var file = state.Value.Files[action.FileId];

            // Update the local version of the 'body'.
            file.Source.UpdateLocalBody(action.Body);

            // Initiate compilation and linking.
            await compilation.CompileAndLink(action.Project);

            dispatcher.Dispatch(new ProjectCompiledAction(action.Project));
        }
    }
}
