using AutoStep.Compiler;
using AutoStep.Editor.Client.Language;
using AutoStep.Editor.Shared;
using AutoStep.Projects;
using Blazor.Fluxor;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace AutoStep.Editor.Client.Store.CodeWindow
{
    public class CodeChangeEffect : Effect<CodeChangeAction>
    {
        private readonly ILoggerFactory logFactory;

        public CodeChangeEffect(ILoggerFactory logFactory)
        {
            this.logFactory = logFactory;
        }

        protected override async Task HandleAsync(CodeChangeAction action, IDispatcher dispatcher)
        {
            try
            {
                // Update the local version of the 'body'.
                action.File.Source.UpdateLocalBody(action.Body);

                // Initiate compilation.
                var projectCompiler = action.Project.Compiler;

                await projectCompiler.Compile(logFactory);

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
