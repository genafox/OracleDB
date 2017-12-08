using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CourseApp.DataAccess.Databases.Oracle.References;
using CourseApp.DataAccess.Models;
using CourseApp.DataAccess.Interfaces.Repositories;

namespace CourseApp.DataAccess.Databases.Oracle.Repositories
{
    public class CourseRepository : ICourseRepository
    {
        private CourseApiReferences apiReferences;

        public CourseRepository(CourseApiReferences apiReferences)
        {
            this.apiReferences = apiReferences;
        }

        public Task<int> Create(Course entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
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

        public void Update(Course entity)
        {
            throw new NotImplementedException();
        }
    }
}
