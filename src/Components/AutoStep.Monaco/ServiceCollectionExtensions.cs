using AutoStep.Monaco.Interop;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutoStep.Monaco
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddMonaco(this IServiceCollection services)
        {
            services.AddSingleton<MonacoInterop>();

            return services;
        }
    }
}
