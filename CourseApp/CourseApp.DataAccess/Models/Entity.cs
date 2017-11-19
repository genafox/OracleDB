using Oracle.ManagedDataAccess.Client;

namespace CourseApp.DataAccess.Models
{
    public abstract class Entity<TIdentifier>
    {
        protected Entity(OracleDataReader dataReader)
        {
        }

        public abstract TIdentifier Id { get; protected set; }
    }
}
