using Autofac;
using Autofac.Integration.WebApi;
using CourseApp.DataAccess.Oracle;
using CourseApp.DataAccess.OracleAgent.Configuration;
using Microsoft.Owin;
using Owin;
using System.Web.Http;

[assembly: OwinStartup(typeof(CourseApp.DataAccess.OracleAgent.Startup))]
namespace CourseApp.DataAccess.OracleAgent
{
    public class Startup
    {
        public void Configuration(IAppBuilder appBuilder)
        {
            var httpConfiguration = new HttpConfiguration();
            var builder = new ContainerBuilder();

            // Register Web Api
            WebApiConfig.Register(httpConfiguration, builder);

            // Register dependencies
            OracleDataAccessConfig.Register(builder);

            // Setup auto mapper
            MapperConfig.Setup();

            var container = builder.Build();

            // Create and assign a dependency resolver for Web API to use.
            httpConfiguration.DependencyResolver = new AutofacWebApiDependencyResolver(container);

            // Register the Autofac middleware FIRST. This also adds
            // Autofac-injected middleware registered with the container.
            appBuilder.UseAutofacMiddleware(container);

                        // Make sure the Autofac lifetime scope is passed to Web API.
            appBuilder.UseAutofacWebApi(httpConfiguration);
            appBuilder.UseWebApi(httpConfiguration);
        }
    }
}