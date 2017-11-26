using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using CourseApp.DataAccess.Repositories.Interfaces;
using CourseEntry = CourseApp.DataAccess.Models.Course;

namespace CourseApp.WebCore.Pages
{
    public class CourseIndexModel : PageModel
    {
        private readonly ICourseRepository courseRepository;

        public CourseIndexModel(ICourseRepository courseRepository)
        {
            this.courseRepository = courseRepository;
        }

        public IList<CourseEntry> Courses { get; set; }

        public void OnGet()
        {
            this.Courses = new List<CourseEntry>()
            {
                new CourseEntry(1, "Course 1", 10)
            };
        }

        //public async Task OnGetAsync()
        //{
        //    IEnumerable<CourseEntry> entries = await this.courseRepository.GetAsync();

        //    this.Courses = entries.ToList();
        //}
    }
}