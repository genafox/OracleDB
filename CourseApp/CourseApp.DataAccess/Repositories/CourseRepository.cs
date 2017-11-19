using CourseApp.DataAccess.Models;
using System;
using System.Collections.Generic;

namespace CourseApp.DataAccess.Repositories
{
    public class CourseRepository : IRepository<Course, int>
    {
        private readonly OracleDbContext context;

        public CourseRepository()
        {
            this.context = new OracleDbContext();
        }

        public IEnumerable<Course> Get()
        {
            return this.context.ExecuteQuery(
                "SELECT * FROM Course_GF",
                reader => new Course(reader));
        }

        public Course GetById(int id)
        {
            throw new NotImplementedException();
        }

        public void Create(Course entity)
        {
            throw new NotImplementedException();
        }

        public void Update(Course entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }
    }
}
