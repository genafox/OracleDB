using CourseApp.DataAccess.Interfaces.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;

namespace CourseApp.WebCore.Pages.Course
{
    public class DeleteModel : PageModel
    {
        private readonly ICourseRepository courseRepository;

        public DeleteModel(ICourseRepository courseRepository)
        {
            this.courseRepository = courseRepository;
        }

        [BindProperty]
        public int CourseId { get; set; }

        public void OnGet(int id)
        {
            this.CourseId = id;
        }

        public async Task<IActionResult> OnPostAsync()
        {
            await this.courseRepository.Delete(this.CourseId);

            return RedirectToPage("/CourseIndex");
        }
    }
}