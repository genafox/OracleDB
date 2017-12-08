using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CourseApp.DataAccess.Models;
using CourseApp.DataAccess.Interfaces.Repositories;
using CourseApp.DataAccess.DataSource.API.Endpoints;

namespace CourseApp.DataAccess.DataSource.API.Repositories
{
    public class CourseRepository : ICourseRepository
    {
        private readonly CourseAPI api;
        private readonly ServiceProxy serviceProxy;

        public CourseRepository(CourseAPI api, ServiceProxy serviceProxy)
        {
            this.api = api;
            this.serviceProxy = serviceProxy;
        }

        public async Task<IEnumerable<Course>> GetAsync()
        {
            var courses = await this.serviceProxy.GetAsync<IEnumerable<Course>>(this.api.GetAllUri);

            return courses;
        }

        public Course GetById(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<int> Create(Course entity)
        {
            int newCourseId = await this.serviceProxy.PostAsync<Course, int>(this.api.GetAllUri, entity);

            return newCourseId;
        }

        public Task Update(Course entity)
        {
            throw new NotImplementedException();
        }

        public Task Delete(int id)
        {
            throw new NotImplementedException();
        }
    }
}
