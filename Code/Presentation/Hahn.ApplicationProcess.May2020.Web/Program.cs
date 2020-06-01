using Hahn.ApplicationProcess.May2020.Domain.Constants;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Serilog;
using System;

namespace Hahn.ApplicationProcess.May2020.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration().
                Enrich.FromLogContext().
                WriteTo.Console().
                CreateLogger();

            try
            {
                Log.Information(AppConstants.InfoMessages.StartingUp);
                CreateHostBuilder(args).Build().Run();
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, AppConstants.ErrorMessages.StartupFailed);
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)

                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.
                    UseStartup<Startup>().
                    UseSerilog((hostingContext, loggerConfiguration) => loggerConfiguration.ReadFrom.
                        Configuration(hostingContext.Configuration).Enrich.
                        FromLogContext().
                        WriteTo.Console());
                });
    }
}
