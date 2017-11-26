using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using CourseApp.DataAccess.Repositories.Interfaces;
using CourseApp.DataAccess.Models;

namespace CourseApp.WebCore.Pages
{
    public class CourseIndexModel : PageModel
    {
        private readonly ICourseRepository courseRepository;

        public CourseIndexModel(ICourseRepository courseRepository)
        {
            this.courseRepository = courseRepository;
        }

        public IList<Course> Courses { get; set; }

        public async Task OnGetAsync()
        {
            IEnumerable<Course> courses = await this.courseRepository.GetAsync();

            this.Courses = courses.ToList();
        }
    }
}