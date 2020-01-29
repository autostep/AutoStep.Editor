using AutoStep.Editor.Shared;
using Blazor.Fluxor;
using Microsoft.AspNetCore.Components;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace AutoStep.Editor.Client.Store.CodeWindow
{
    public class ChangeFileEffect : Effect<ChangeFileAction>
    {
        protected override async Task HandleAsync(ChangeFileAction action, IDispatcher dispatcher)
        {
            try
            {
                var codeBody = await action.NewFile.Source.GetContentAsync();

                action.NewFile.Source.UpdateLocalBody(codeBody);

                dispatcher.Dispatch(new LoadCodeCompleteAction(action.NewFile));
            }
            catch
            {
                // Do stuff eventually.
            }
        }
    }
}
