using System.ComponentModel.DataAnnotations;

namespace CourseApp.Web.Models
{
    public class CourseModel
    {
        public int Id { get; set; }

        [StringLength(50)]
        public string Name { get; set; }

        public double Price { get; set; }
    }
}