namespace CourseApp.DataAccess.Models
{
    public class Course : Entity<int>
    {
        public Course(int id, string name, double price)
        {
            this.Id = id;
            this.Name = name;
            this.Price = price;
        }

        public override int Id { get; protected set; }

        public string Name { get; }

        public double Price { get; }
    }
}
