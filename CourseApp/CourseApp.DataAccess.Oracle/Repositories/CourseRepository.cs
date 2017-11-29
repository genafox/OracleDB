using CourseApp.DataAccess.Interfaces.Repositories;
using CourseApp.DataAccess.Models;
using CourseApp.DataAccess.Oracle;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CourseApp.DataAccess.Oracle.Repositories
{
    public class CourseRepository : ICourseRepository
    {
        private readonly OracleDbContext context;

        public CourseRepository(OracleDbContext dbContext)
        {
            this.context = dbContext;
        }

        public Task<IEnumerable<Course>> GetAsync()
        {
            return this.context.ExecuteQueryAsync(
                "SELECT * FROM Course_GF",
                OracleDataMapper.FromReader);
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
