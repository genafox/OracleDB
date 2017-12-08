using System.Web.Http;

namespace CourseApp.DataAccess.OracleAgent.Configuration
{
    public class WebApiConfig
    {
        public static void RegisterRoutes(HttpConfiguration config)
        {
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional });

            config.Routes.MapHttpRoute(
                name: "AllCourses",
                routeTemplate: "courses",
                defaults: new { controller = "CourseApi", action = "Get" });
        }
    }
}