using System.ComponentModel.DataAnnotations;

namespace CourseApp.DataAccess.OracleAgent.Models
{
    public class CourseModel
    {
        public int? Id { get; set; }

        [Required]
        [MaxLength(50, ErrorMessage = "Course name cannot be more than 50 characters")]
        public string Name { get; set; }

        public double Price { get; set; }
    }
}