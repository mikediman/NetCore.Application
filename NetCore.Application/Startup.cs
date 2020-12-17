using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NetCore.Application.Controller;
using NetCore.Application.Implementation;
using NetCore.Application.Interfaces;
using System.Data;

namespace NetCore.Application
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRegistrationUserService(Configuration.GetConnectionString("ApplicationSampleDb"));
            services.AddHealthChecks().AddSqlServer(Configuration["ConnectionStrings:ApplicationSampleDb"]);
            services.AddControllers().AddNewtonsoftJson().AddApplicationPart(typeof(ApplicationController).Assembly);
        }
        
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseDefaultFiles();
            app.UseStaticFiles();
            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync("Welcome to asp .net core application. Enjoy!");
                });
                endpoints.MapHealthChecks("/health", new HealthCheckOptions() {
                    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse,
                    AllowCachingResponses = false
                });
            });
        }
    }
}
