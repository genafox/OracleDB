using AutoMapper;
using CourseApp.DataAccess.DataSource.API.DTOs;
using CourseApp.DataAccess.Interfaces.Repositories;
using CourseApp.DataAccess.Models;
using System;
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
        public async Task<IList<CourseDto>> Get()
        {
            IEnumerable<Course> courses = await this.courseRepository.GetAsync();

            return courses
                .Select(c => Mapper.Map<CourseDto>(c))
                .ToList();
        }

        [Route("{id:int}")]
        [HttpGet]
        public async Task<CourseDto> GetById(int id)
        {
            Course course = await this.courseRepository.GetById(id);

            return Mapper.Map<CourseDto>(course);
        }

        [Route("")]
        [HttpPost]
        public async Task<int> Create([FromBody] CourseDto model)
        {
            int newCourseId = await this.courseRepository.Create(Mapper.Map<Course>(model));

            return newCourseId;
        }

        [Route("")]
        [HttpPut]
        public async Task Update([FromBody] CourseDto model)
        {
            await this.courseRepository.Update(Mapper.Map<Course>(model));
        }

        [Route("{id:int}")]
        [HttpDelete]
        public async Task Delete(int id)
        {
            await this.courseRepository.Delete(id);
        }
    }
}
