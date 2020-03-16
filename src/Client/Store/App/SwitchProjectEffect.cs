using System.Linq;
using System.Threading.Tasks;
using AutoStep.Editor.Client.Language;
using AutoStep.Editor.Client.Store.FileView;
using Blazor.Fluxor;
using Microsoft.Extensions.Logging;

namespace AutoStep.Editor.Client.Store.App
{
    /// <summary>
    /// Occurs when the active project changes.
    /// </summary>
    internal class SwitchProjectEffect : Effect<SwitchProjectAction>
    {
        private readonly CompilationService compilation;

        public SwitchProjectEffect(CompilationService compilation)
        {
            this.compilation = compilation;
        }

        protected override async Task HandleAsync(SwitchProjectAction action, IDispatcher dispatcher)
        {
            // Open all the files.
            foreach (var item in action.NewProject.AllFiles)
            {
                dispatcher.Dispatch(new OpenFileAction(action.NewProject, item.Key));
            }

            await compilation.CompileAndLink(action.NewProject);

            dispatcher.Dispatch(new ProjectCompiledAction(action.NewProject));
        }
    }
}
