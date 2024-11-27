using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using University.Students.UI.Data;

namespace University.Students.UI.Pages.Students
{
    public class EditModel : PageModel
    {
        private readonly StudentDAL _studentDAL;

        public EditModel(StudentDAL studentDAL)
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

            var id = Request.Query["id"].ToString();

            _studentDAL.EditStudent(Student, id);
            return RedirectToPage("Index");
        }
    }
}
