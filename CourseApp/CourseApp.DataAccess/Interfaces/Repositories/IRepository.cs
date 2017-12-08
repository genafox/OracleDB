using System.Collections.Generic;
using System.Threading.Tasks;
using CourseApp.DataAccess.Models;

namespace CourseApp.DataAccess.Interfaces.Repositories
{
    public interface IRepository<T, TIdentifier> where T : Entity<TIdentifier>
    {
        Task<IEnumerable<T>> GetAsync();

        T GetById(TIdentifier id);

        Task<TIdentifier> Create(T entity);

        Task Update(T entity);

        Task Delete(TIdentifier id);
    }
}
