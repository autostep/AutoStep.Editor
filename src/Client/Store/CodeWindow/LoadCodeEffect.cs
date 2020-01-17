using AutoStep.Editor.Shared;
using Blazor.Fluxor;
using Microsoft.AspNetCore.Components;
using System.Net.Http;
using System.Threading.Tasks;

namespace AutoStep.Editor.Client.Store.CodeWindow
{
    public class LoadCodeEffect : Effect<LoadCodeAction>
    {
        public LoadCodeEffect(HttpClient client)
        {
            Client = client;
        }

        public HttpClient Client { get; }

        protected override async Task HandleAsync(LoadCodeAction action, IDispatcher dispatcher)
        {
            try
            {
                var codeBody = await Client.GetJsonAsync<CodeResource>($"api/resources/{action.SourceName}");

                dispatcher.Dispatch(new LoadCodeCompleteAction(codeBody.Body));
            }
            catch
            {
                // Do stuff eventually.
            }
        }
    }
}
