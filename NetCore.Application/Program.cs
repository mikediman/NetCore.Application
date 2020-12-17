using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using NLog.Web;
using System;

namespace NetCore.Application
{
    public class Program
    {
        public static void Main(string[] args)
        {
			var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")?.ToLower();
			var logger = NLogBuilder.ConfigureNLog($"nlog.{environment}.config").GetCurrentClassLogger();

			try
			{
				logger.Info("Web Api Started");
				CreateHostBuilder(args).Build().Run();
			}
			catch (Exception ex)
			{
				logger.Fatal(ex, "Host terminated unexpectedly");
				throw;
			}
			finally
			{
				NLog.LogManager.Shutdown();
			}
		}

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
