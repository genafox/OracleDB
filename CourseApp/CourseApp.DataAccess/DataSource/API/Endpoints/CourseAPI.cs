using System;

namespace CourseApp.DataAccess.DataSource.API.Endpoints
{
    public class CourseAPI
    {
        public string GetAllUri => "/api/courses";

        public string GetByIdUri => "/api/courses/{0}";

        public string CreateUri => "/api/courses";

        public string UpdateUri => "/api/courses";

        public string DeleteUri => "/api/courses";
    }
}
