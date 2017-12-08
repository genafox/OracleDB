﻿using Autofac;
using Autofac.Integration.WebApi;
using CourseApp.DataAccess.Oracle;
using CourseApp.DataAccess.OracleAgent.Configuration;
using Owin;
using System.Reflection;
using System.Web.Http;

namespace CourseApp.DataAccess.OracleAgent
{
    public class Startup
    {
        public void Configuration(IAppBuilder appBuilder)
        {
            var httpConfiguration = new HttpConfiguration();

            WebApiConfig.RegisterRoutes(httpConfiguration);

            var builder = new ContainerBuilder();

            // Register dependencies
            OracleDataAccessConfig.Register(builder);

            // Setup auto mapper
            MapperConfig.Setup();

            // Register Web API controller in executing assembly.
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

            // Register the filter provider if you have custom filters that need DI.
            builder.RegisterWebApiFilterProvider(httpConfiguration);

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