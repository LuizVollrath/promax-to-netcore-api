using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NLog;
using NLog.Web;
using System;
using System.Diagnostics.CodeAnalysis;
using LogLevel = Microsoft.Extensions.Logging.LogLevel;

namespace Promax.Api.To.NetCore.Api
{
    [ExcludeFromCodeCoverage]
    public static class Program
    {
        private const string LogConfig = "nlog.config";

        public static void Main(string[] args)
        {
            var logger = NLogBuilder.ConfigureNLog(LogConfig).GetCurrentClassLogger();
            try
            {
                logger.Trace("Starting application");
                CreateWebHostBuilder(args).Build().Run();
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Application stopped by exception");
                throw;
            }
            finally
            {
                LogManager.Shutdown();
            }
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .ConfigureLogging(logging =>
                {
                    logging.ClearProviders();
                    logging.SetMinimumLevel(LogLevel.Trace);
                })
                .UseKestrel(options => { options.Limits.MaxRequestBodySize = null; })
                .UseNLog();
    }
}
