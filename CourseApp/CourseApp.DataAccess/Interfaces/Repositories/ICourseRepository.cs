using CourseApp.DataAccess.Models;
using CourseApp.DataAccess.Repositories.Interfaces.Repositories;

namespace CourseApp.DataAccess.Interfaces.Repositories
{
    public interface ICourseRepository : IRepository<Course, int>
    {
    }
}
