using AutoStep.Editor.Shared;
using Blazor.Fluxor;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace AutoStep.Editor.Client.Store.CodeWindow
{
    public class ChangeFileEffect : Effect<ChangeFileAction>
    {
        private readonly ILoggerFactory loggerFactory;

        public ChangeFileEffect(ILoggerFactory loggerFactory)
        {
            this.loggerFactory = loggerFactory;
        }

        protected override async Task HandleAsync(ChangeFileAction action, IDispatcher dispatcher)
        {
            try
            {
                var codeBody = await action.NewFile.Source.GetContentAsync();

                dispatcher.Dispatch(new LoadCodeCompleteAction(action.NewFile));

                // Initiate compilation.
                var projectCompiler = action.Project.Compiler;

                await projectCompiler.Compile(loggerFactory);

                projectCompiler.Link();

                dispatcher.Dispatch(new ProjectCompiledAction(action.Project));
            }
            catch
            {
                // Do stuff eventually.
            }
        }
    }
}
