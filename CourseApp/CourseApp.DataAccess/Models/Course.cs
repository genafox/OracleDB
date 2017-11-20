using Oracle.ManagedDataAccess.Client;

namespace CourseApp.DataAccess.Models
{
    public class Course : Entity<int>
    {
        public Course(int id, string name, double price) : base(null)
        {
            this.Id = id;
            this.Name = name;
            this.Price = price;
        }

        public Course(OracleDataReader dataReader) : base(dataReader)
        {
            this.Id = dataReader.GetInt32(0);
            this.Name = dataReader.GetString(1);
            this.Price = dataReader.IsDBNull(2)
                ? default(double)
                : dataReader.GetDouble(2);
        }
        
        public override int Id { get; protected set; }

        public string Name { get; }

        public double Price { get; }
    }
}
