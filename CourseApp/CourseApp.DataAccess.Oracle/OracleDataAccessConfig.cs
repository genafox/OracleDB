using Autofac;
using CourseApp.DataAccess.Oracle.Repositories;
using System.Configuration;

namespace CourseApp.DataAccess.Oracle
{
    public static class OracleDataAccessConfig
    {
        public static void Register(ContainerBuilder builder)
        {
            var configSection = ConfigurationManager.GetSection("oracleDbConnectionSettings");

            builder
                .RegisterInstance((OracleDbConnectionSettings)configSection)
                .SingleInstance();

            builder
                .RegisterType<OracleDbContext>().AsSelf()
                .InstancePerLifetimeScope();

            builder
                .RegisterType<CourseRepository>().AsImplementedInterfaces()
                .InstancePerLifetimeScope();
        }
    }
}
