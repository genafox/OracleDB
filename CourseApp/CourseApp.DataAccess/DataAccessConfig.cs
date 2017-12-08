using CourseApp.DataAccess.DataSource.API;
using CourseApp.DataAccess.DataSource.API.Endpoints;
using CourseApp.DataAccess.DataSource.API.Repositories;
using CourseApp.DataAccess.Interfaces.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace CourseApp.DataAccess
{
    public static class DataAccessConfig
    {
        public static void Setup(IServiceCollection services, IConfiguration configuration)
        {
            IConfigurationSection oracleDbSettings = configuration.GetSection("Database:Oracle");
            var oracleAgentUri = new Uri(oracleDbSettings["AgentUrl"]);

            services.AddSingleton<CourseAPI>();

            services.AddTransient<ICourseRepository, CourseRepository>();

            services.AddScoped(sp => new ServiceProxy(oracleAgentUri));
        }
    }
}
