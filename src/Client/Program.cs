using System.Threading.Tasks;
using AutoStep.Monaco;
using Blazor.Fluxor;
using Microsoft.AspNetCore.Blazor.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace AutoStep.Editor.Client
{
    /// <summary>
    /// Entry point for the client application.
    /// </summary>
    public static class Program
    {
        /// <summary>
        /// Entry method.
        /// </summary>
        /// <param name="args">Start arguments.</param>
        /// <returns>Completion task.</returns>
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);

            builder.Services.AddFluxor(options =>
                options.UseDependencyInjection(typeof(Program).Assembly)
            );

            builder.Services.AddMonaco();

            builder.Services.AddLogging(cfg => cfg.SetMinimumLevel(LogLevel.Debug));

            builder.RootComponents.Add<App>("app");

            await builder.Build().RunAsync();
        }
    }
}
