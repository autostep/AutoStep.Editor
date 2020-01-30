using AutoStep.Editor.Client.Store.CodeWindow;
using Blazor.Fluxor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutoStep.Editor.Client.Store
{
    public class SwitchProjectEffect : Effect<SwitchProjectAction>
    {
        protected override Task HandleAsync(SwitchProjectAction action, IDispatcher dispatcher)
        {
            try
            {
                // Just dispatch a change file action.
                dispatcher.Dispatch(new ChangeFileAction(action.NewProject, action.DefaultFile));
            }
            catch
            {
                // Do stuff eventually.
            }

            return Task.CompletedTask;
        }
    }
}
