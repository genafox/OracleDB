using Autofac;
using CourseApp.DataAccess.Oracle.Repositories;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
