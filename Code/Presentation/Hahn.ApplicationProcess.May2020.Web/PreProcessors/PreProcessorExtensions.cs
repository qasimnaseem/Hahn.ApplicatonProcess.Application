using Hahn.ApplicationProcess.May2020.Persistence;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Hahn.ApplicationProcess.May2020.Web.PreProcessors
{
    public static class PreProcessorExtensions
    {
        public static IApplicationBuilder RunDatabaseMigration(this IApplicationBuilder builder)
        {
            using var serviceScope = builder.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope();
            using var context = serviceScope.ServiceProvider.GetService<AppDbContext>();

            try { context.Database.Migrate(); }
            catch { }

            return builder;
        }
    }
}
