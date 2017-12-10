using System.Collections.Generic;
using System.Threading.Tasks;
using CourseApp.DataAccess.Models;
using CourseApp.DataAccess.Interfaces.Repositories;
using CourseApp.DataAccess.DataSource.API.Endpoints;
using CourseApp.DataAccess.DataSource.API.DTOs;
using System.Linq;

namespace CourseApp.DataAccess.DataSource.API.Repositories
{
    public class CourseRepository : ICourseRepository
    {
        private readonly CourseAPI api;
        private readonly DataProvider dataProvider;

        public CourseRepository(CourseAPI api, DataProvider dataProvider)
        {
            this.api = api;
            this.dataProvider = dataProvider;
        }

        public async Task<IEnumerable<Course>> GetAsync(int pageNumber = 1, int pageSize = int.MaxValue)
        {
            var coursesDtos = await this.dataProvider.GetAsync<IEnumerable<CourseDto>>(this.api.GetAllUri(pageNumber, pageSize));

            return coursesDtos.Select(FromDto);
        }

        public async Task<Course> GetById(int id)
        {
            var course = await this.dataProvider.GetAsync<CourseDto>(this.api.GetByIdUri(id));

            return FromDto(course);
        }

        public async Task<int> Create(Course entity)
        {
            CourseDto dto = ToDto(entity);

            int newCourseId = await this.dataProvider.PostAsync<CourseDto, int>(this.api.CreateUri, dto);

            return newCourseId;
        }

        public async Task Update(Course entity)
        {
            CourseDto dto = ToDto(entity);
            await this.dataProvider.PutAsync<CourseDto>(this.api.UpdateUri, dto);
        }

        public async Task Delete(int id)
        {
            await this.dataProvider.DeleteAsync<int>(this.api.DeleteUri(id));
        }

        private static Course FromDto(CourseDto dto)
        {
            return new Course(dto.Id, dto.Name, dto.Price, dto.Rating);
        }

        private static CourseDto ToDto(Course course)
        {
            return new CourseDto
            {
                Id = course.Id,
                Name = course.Name,
                Price = course.Price
            };
        }
    }
}
