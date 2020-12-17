using Microsoft.Data.SqlClient;
using Microsoft.Extensions.DependencyInjection;
using NetCore.Application.Interfaces;
using System.Data;

namespace NetCore.Application.Implementation
{
    public static class ApplicationServiceDIExtensions
    {
        public static IServiceCollection AddRegistrationUserService(this IServiceCollection services, string dbConnectionString)
        {
            services.AddScoped<IDbConnection>(db => new SqlConnection(dbConnectionString));
            services.AddHttpClient("default");
            services.AddScoped<IApplicationService, ApplicationService>();
            return services;
        }
    }
}
