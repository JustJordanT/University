using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using University.Students.UI.Data;
using University.Students.UI.Models;

namespace University.Students.UI.Pages.Students
{
    public class StudentsModel : PageModel
    {
        private readonly StudentDAL _studentDAL;

        public StudentsModel(StudentDAL studentDAL)
        {
            _studentDAL = studentDAL;
        }

        public IEnumerable<Student> Students { get; private set; }

        //public IActionResult OnGetRedirectStudentCreate()
        //{
        //    return RedirectToPage("./Students/Create");
        //}

        public void OnGet()
        {
            Students = _studentDAL.GetStudents();
        }
    }
}
