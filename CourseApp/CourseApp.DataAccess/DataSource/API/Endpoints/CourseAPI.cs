using System;

namespace CourseApp.DataAccess.DataSource.API.Endpoints
{
    public class CourseAPI
    {
        public string GetAllUri(int pageNumber, int pageSize) => $"/api/courses?pageNumber={pageNumber}&pageSize={pageSize}";

        public string GetByIdUri(int id) => $"/api/courses/{id}";

        public string CreateUri => "/api/courses";

        public string UpdateUri => "/api/courses";

        public string DeleteUri(int id) => $"/api/courses/{id}";
    }
}
