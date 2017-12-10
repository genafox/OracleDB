using CourseApp.DataAccess.Interfaces.Repositories;
using CourseApp.DataAccess.Models;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Oracle.ManagedDataAccess.Client;
using System.Linq;
using CourseApp.DataAccess.Exceptions;

namespace CourseApp.DataAccess.Oracle.Repositories
{
    public class CourseRepository : ICourseRepository
    {
        private readonly OracleDbContext context;

        public CourseRepository(OracleDbContext dbContext)
        {
            this.context = dbContext;
        }

        public Task<IEnumerable<Course>> GetAsync(int pageNumber, int pageSize)
        {
            return this.context.ExecuteQueryAsync(
               $"SELECT * FROM TABLE(CourseAppPackage_GF.GetTopPopularCourses({pageNumber}, {pageSize}))",
               OracleDataMapper.MapCourseWithRating);
        }

        public async Task<Course> GetById(int id)
        {
            var entries = await this.context.ExecuteQueryAsync(
                $"SELECT * FROM Course_GF WHERE id = {id}",
                OracleDataMapper.MapCourse);

            return entries.Single();
        }

        public async Task<int> Create(Course entity)
        {
            string procName = "CourseAppPackage_GF.CreateCourseProc";

            var newCourseIdParam = new OracleParameter("newCourseId", OracleDbType.Int32, ParameterDirection.Output);

            try
            {
                await this.context.ExecuteProcedureAsync(
                    procName,
                    new OracleParameter("courseName", OracleDbType.Varchar2, entity.Name, ParameterDirection.Input),
                    new OracleParameter("coursePrice", OracleDbType.Double, entity.Price, ParameterDirection.Input),
                    newCourseIdParam);

                return int.Parse(newCourseIdParam.Value.ToString());
            }
            catch (UniqueNameViolationException ex)
            {
                return -1;
            }
        }

        public async Task Update(Course entity)
        {
            string command = $@"
                UPDATE Course_GF
                SET name = '{entity.Name}',
                    price = {entity.Price}
                WHERE id = {entity.Id}";

            await this.context.ExecuteNonQueryAsync(command);
        }

        public async Task Delete(int id)
        {
            string command = $@"
                DELETE FROM Course_GF
                WHERE id = {id}";

            await this.context.ExecuteNonQueryAsync(command);
        }
    }
}
