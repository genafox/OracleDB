using CourseApp.DataAccess.Repositories.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CourseApp.DataAccess.Models;

namespace CourseApp.DataAccess.Databases.Oracle.Repositories
{
    public class OracleCourseRepository : ICourseRepository
    {
        public void Create(Course entity)
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
