using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CourseApp.DataAccess.Models;
using CourseApp.DataAccess.Interfaces.Repositories;
using CourseApp.DataAccess.DataSource.API.Endpoints;

namespace CourseApp.DataAccess.DataSource.API..Repositories
{
    public class CourseRepository : ICourseRepository
    {
        private CourseAPI api;

        public CourseRepository(CourseAPI api)
        {
            this.api = api;
        }

        public Task<int> Create(Course entity)
        {
            throw new NotImplementedException();
        }

        public Task Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Course>> GetAsync()
        {
            throw new NotImplementedException();
        }

        public Course GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Task Update(Course entity)
        {
            throw new NotImplementedException();
        }
    }
}
