﻿using CourseApp.DataAccess.Databases.Oracle;
using CourseApp.DataAccess.Databases.Oracle.Repositories;
using CourseApp.DataAccess.Interfaces.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using CourseApp.DataAccess.Databases.Oracle.References;

namespace CourseApp.DataAccess
{
    public static class DataAccessConfig
    {
        public static void Setup(IServiceCollection services, IConfiguration configuration)
        {
            IConfigurationSection oracleDbSettings = configuration.GetSection("Database:Oracle");
            var oracleAgentUri = new Uri(oracleDbSettings["AgentUrl"]);

            services.AddSingleton<CourseApiReferences>();

            services.AddScoped(sp => new DataProvider(oracleAgentUri));
            services.AddTransient<ICourseRepository, CourseRepository>();
        }
    }
}
