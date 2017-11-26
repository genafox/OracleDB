namespace CourseApp.DataAccess.Models
{
    public abstract class Entity<TIdentifier>
    {
        public abstract TIdentifier Id { get; protected set; }
    }
}
