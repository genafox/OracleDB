using CourseApp.DataAccess.Models;
using System.Collections.Generic;

namespace CourseApp.DataAccess.Repositories
{
    public interface IRepository<T, TIdentifier> where T : Entity<TIdentifier>
    {
        IEnumerable<T> Get();

        T GetById(TIdentifier id);

        void Create(T entity);

        void Update(T entity);

        void Delete(TIdentifier id);
    }
}
