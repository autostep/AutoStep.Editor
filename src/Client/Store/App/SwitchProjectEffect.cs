using System.Threading.Tasks;
using AutoStep.Editor.Client.Store.CodeWindow;
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
            // Just dispatch a change file action to load the default file.
            dispatcher.Dispatch(new ChangeFileAction(action.NewProject, action.DefaultFile));

            return Task.CompletedTask;
        }
    }
}
