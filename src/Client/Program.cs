using AutoStep.Compiler;
using AutoStep.Editor.Client.Language;
using AutoStep.Monaco;
using Blazor.Fluxor;
using Microsoft.AspNetCore.Blazor.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace AutoStep.Editor.Client
{
    public class Program
    {
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
