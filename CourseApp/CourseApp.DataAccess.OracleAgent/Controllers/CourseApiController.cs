using AutoMapper;
using CourseApp.DataAccess.Interfaces.Repositories;
using CourseApp.DataAccess.Models;
using CourseApp.DataAccess.OracleAgent.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;

namespace CourseApp.DataAccess.OracleAgent.Controllers
{
    [RoutePrefix("api/courses")]
    public class CourseApiController : ApiController
    {
        private readonly ICourseRepository courseRepository;

        public CourseApiController(ICourseRepository courseRepository)
        {
            this.courseRepository = courseRepository;
        }

        [Route("")]
        [HttpGet]
        public async Task<IList<CourseModel>> Get()
        {
            IEnumerable<Course> courses = await this.courseRepository.GetAsync();

            return courses
                .Select(c => Mapper.Map<CourseModel>(c))
                .ToList();
        }

        [Route("")]
        [HttpPost]
        public async Task<int> Create([FromBody] CourseModel model)
        {
            int newCourseId = await this.courseRepository.Create(Mapper.Map<Course>(model));

            return newCourseId;
        }
    }
}
