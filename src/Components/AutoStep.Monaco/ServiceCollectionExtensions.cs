using AutoStep.Monaco.Interop;
using Microsoft.Extensions.DependencyInjection;

namespace AutoStep.Monaco
{
    /// <summary>
    /// Extension methods fo
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Add services for the Monaco component.
        /// </summary>
        public static IServiceCollection AddMonaco(this IServiceCollection services)
        {
            services.AddSingleton<MonacoInterop>();

            return services;
        }
    }
}
