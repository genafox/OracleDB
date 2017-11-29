using CourseApp.DataAccess.Interfaces.Repositories;
using CourseApp.DataAccess.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;

namespace CourseApp.DataAccess.OracleAgent.Controllers
{
    public class CourseApiController : ApiController
    {
        private readonly ICourseRepository courseRepository;

        public CourseApiController(ICourseRepository courseRepository)
        {
            this.courseRepository = courseRepository;
        }

        [HttpGet]
        public async Task<IList<Course>> Get()
        {
            IEnumerable<Course> courses = await this.courseRepository.GetAsync();

            return courses.ToList();
        }
    }
}
