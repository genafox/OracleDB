using CourseApp.DataAccess.Databases.Oracle;
using CourseApp.DataAccess.Databases.Oracle.Repositories;
using CourseApp.DataAccess.Repositories.Interfaces.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace CourseApp.DataAccess
{
    public class DataAccessConfig
    {
        public static void Configure(IServiceCollection services, IConfiguration configuration)
        {
            IConfigurationSection oracleDbSettings = configuration.GetSection("Database:Oracle");
            services.Configure<OracleConnectionSettings>(oracleDbSettings);

            services.AddSingleton(serviceProvider =>
            {
                return serviceProvider.GetService<IOptions<OracleConnectionSettings>>().Value;
            });

            services.AddScoped<OracleDataProvider>();
            services.AddTransient<ICourseRepository, OracleCourseRepository>();
        }
    }
}
