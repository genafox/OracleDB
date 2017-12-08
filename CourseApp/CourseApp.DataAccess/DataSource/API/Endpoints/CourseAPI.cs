using System;

namespace CourseApp.DataAccess.DataSource.API.Endpoints
{
    public class CourseAPI
    {
        private readonly Uri hostUri;

        public CourseAPI(Uri hostUri)
        {
            this.hostUri = hostUri;
        }

        public Uri GetAllUri => new Uri(this.hostUri, "courses");

        public Uri GetByIdUri => new Uri(this.hostUri, "course");

        public Uri CreateUri { get; }

        public Uri UpdateUri { get; }

        public Uri DeleteUri { get; }
    }
}
