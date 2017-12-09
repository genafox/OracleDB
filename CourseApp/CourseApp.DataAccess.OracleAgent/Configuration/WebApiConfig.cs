using Autofac;
using Autofac.Integration.WebApi;
using System.Net.Http.Formatting;
using System.Reflection;
using System.Web.Http;

namespace CourseApp.DataAccess.OracleAgent.Configuration
{
    public class WebApiConfig
    {
        public static void Register(HttpConfiguration config, ContainerBuilder builder)
        {
            // Register Web API controllers in executing assembly.
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

            // Register the filter provider if you have custom filters that need DI.
            builder.RegisterWebApiFilterProvider(config);

            Routes(config);
            Formatters(config);
        }

        private static void Routes(HttpConfiguration config)
        {
            config.MapHttpAttributeRoutes();

            //config.Routes.MapHttpRoute(
            //    name: "DefaultApi",
            //    routeTemplate: "api/{controller}/{id}",
            //    defaults: new { id = RouteParameter.Optional });
        }

        private static void Formatters(HttpConfiguration config)
        {
            config.Formatters.AddRange(new MediaTypeFormatter[] 
            {
                new JsonMediaTypeFormatter(),
                new XmlMediaTypeFormatter()
            });
        }
    }
}