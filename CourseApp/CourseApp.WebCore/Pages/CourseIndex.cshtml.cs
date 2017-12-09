using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.RazorPages;
using CourseApp.DataAccess.Interfaces.Repositories;
using System.Threading.Tasks;
using System.Linq;
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

        public async Task OnGetAsync()
        {
            IEnumerable<CourseEntry> entries = await this.courseRepository.GetAsync();

            this.Courses = entries.ToList();
        }
    }
}