using System.Linq;
using System.Threading.Tasks;
using AutoStep.Editor.Client.Store.FileView;
using Blazor.Fluxor;

namespace AutoStep.Editor.Client.Store.App
{
    /// <summary>
    /// Occurs when the active project changes.
    /// </summary>
    internal class SwitchProjectEffect : Effect<SwitchProjectAction>
    {
        protected override Task HandleAsync(SwitchProjectAction action, IDispatcher dispatcher)
        {
            // Open all the files.
            foreach (var item in action.NewProject.AllFiles)
            {
                dispatcher.Dispatch(new OpenFileAction(action.NewProject, item.Key));
            }

            return Task.CompletedTask;
        }
    }
}
