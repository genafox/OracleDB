using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Configuration;

namespace CourseApp.DataAccess
{
    public class DataAccessConfig
    {
        public static void Configure(IServiceCollection services, IConfiguration configuration)
        {
            string databaseType = configuration["DatabaseType"];

            var dbConnectionSettings = ConfigurationManager.GetSection("RegisterCompanies");

            services.AddSingleton();

            services.AddScoped<OracleDbContext>();
            services.AddTransient<ICourseRepository, CourseRepository>();
        }
    }
}
