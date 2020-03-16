using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoStep.Editor.Client.Store.App;
using AutoStep.Projects;
using Blazor.Fluxor;
using Microsoft.Extensions.Logging;

namespace AutoStep.Editor.Client.Language
{
    internal class CompilationService
    {
        private readonly ILogger<CompilationService> logger;
        private readonly ILoggerFactory loggerFactory;

        public CompilationService(ILoggerFactory loggerFactory)
        {
            this.logger = loggerFactory.CreateLogger<CompilationService>();
            this.loggerFactory = loggerFactory;
        }

        public async ValueTask CompileAndLink(Project project)
        {
            // Do an initial compile and link.
            var projectCompiler = project.Compiler;

            logger.LogDebug("Running Project Compile");

            var compileResult = await projectCompiler.CompileAsync(loggerFactory);

            logger.LogDebug("Project Compile Complete");

            foreach (var msg in compileResult.Messages)
            {
                logger.LogDebug("Compiler Message: {0}", msg.ToString());
            }

            logger.LogDebug("Running Project Link");

            var linkResult = projectCompiler.Link();

            logger.LogDebug("Project Link Complete");

            foreach (var msg in linkResult.Messages)
            {
                logger.LogDebug("Linker Message: {0}", msg.ToString());
            }
        }
    }
}
