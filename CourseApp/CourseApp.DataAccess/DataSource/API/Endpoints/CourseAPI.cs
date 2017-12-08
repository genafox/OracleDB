using System;

namespace CourseApp.DataAccess.DataSource.API.Endpoints
{
    public class CourseAPI
    {
        public string GetAllUri => "courses";

        public string GetByIdUri => "courses/{0}";

        public string CreateUri => "courses";

        public string UpdateUri => "courses";

        public string DeleteUri => "courses";
    }
}
