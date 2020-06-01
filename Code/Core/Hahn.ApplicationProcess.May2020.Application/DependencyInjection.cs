using AutoMapper;
using Hahn.ApplicationProcess.May2020.Application.Common.Behaviours;
using Hahn.ApplicationProcess.May2020.Application.Common.Interfaces;
using Hahn.ApplicationProcess.May2020.Application.Utilities;
using Hahn.ApplicationProcess.May2020.Domain.Common;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Hahn.ApplicationProcess.May2020.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestValidationBehavior<,>));
            services.AddSingleton<ICountryFinder, CountryFinder>();
            services.Configure<CountryFinderApiOpts>(opts => configuration.Bind("CountryFinderApi", opts));

            return services;
        }
    }
}
