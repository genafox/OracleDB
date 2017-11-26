using CourseApp.DataAccess.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CourseApp.DataAccess.Repositories.Interfaces
{
    public interface IRepository<T, TIdentifier> where T : Entity<TIdentifier>
    {
        Task<IEnumerable<T>> GetAsync();

        T GetById(TIdentifier id);

        void Create(T entity);

        void Update(T entity);

        void Delete(TIdentifier id);
    }
}
