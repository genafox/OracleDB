using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using CourseApp.DataAccess.Interfaces.Repositories;
using CourseApp.WebCore.Models;
using AutoMapper;
using CourseEntry = CourseApp.DataAccess.Models.Course;

namespace CourseApp.WebCore.Pages.Course
{
    public class CreateOrEditModel : PageModel
    {
        private readonly ICourseRepository courseRepository;

        public CreateOrEditModel(ICourseRepository courseRepository)
        {
            this.courseRepository = courseRepository;
        }

        public string Mode { get; set; }

        [BindProperty]
        public CourseModel Course { get; set; }

        public async Task OnGetAsync(int? id)
        {
            if (id.HasValue)
            {
                this.Mode = "Edit";

                CourseEntry entry = await this.courseRepository.GetById(id.Value);

                this.Course = Mapper.Map<CourseModel>(entry);
            }
            else
            {
                this.Mode = "Create";
                this.Course = new CourseModel();
            }
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            if (this.Course.Id.HasValue)
            {
                await this.courseRepository.Update(Mapper.Map<CourseEntry>(this.Course));
            }
            else
            {
                await this.courseRepository.Create(Mapper.Map<CourseEntry>(this.Course));
            }

            return RedirectToPage("/Index");
        }
    }
}