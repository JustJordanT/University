using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using University.Students.UI.Data;

namespace University.Students.UI.Pages.Students
{
    public class CreateModel : PageModel
    {
        public readonly StudentDAL _studentDAL;

        public CreateModel(StudentDAL studentDAL)
        {
            _studentDAL = studentDAL;
        }

        [BindProperty]
        public Models.Student Student { get; set; }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            _studentDAL.AddStudent(Student);
            return RedirectToPage("Index");
        }
    }
}
