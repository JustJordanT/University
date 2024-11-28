using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using University.Students.UI.Data;
using University.Students.UI.Models;

namespace University.Students.UI.Pages.Students
{
    public class DeleteModel : PageModel
    {
        public string StudentName { get; set; } = string.Empty;

        public readonly StudentDAL _studentDAL;
        [BindProperty]
        public int StudentId { get; set; }

        [BindProperty]
        public Student Student { get; set; }


        public DeleteModel(StudentDAL studentDal)
        {
            _studentDAL = studentDal;
        }

        public void OnGet()
        {
            Console.WriteLine($"Query string Id: {Request.Query["Id"]}");

            if (int.TryParse(Request.Query["Id"], out int id))
            {
                StudentId = id;
                Student = _studentDAL.GetStudentById(StudentId);
                Console.WriteLine($"OnGet StudentId parsed: {StudentId}");
            }
            else
            {
                Console.WriteLine("Failed to parse StudentId from query string");
            }
        }

        public IActionResult OnPost()
        {
            Console.WriteLine($"OnPost StudentId: {StudentId}");

            if (StudentId == 0)
            {
                return NotFound();
            }

            _studentDAL.DeleteStudent(StudentId);
            return RedirectToPage("Index");
        }
    }
}
