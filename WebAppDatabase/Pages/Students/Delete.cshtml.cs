using Microsoft.AspNetCore.Mvc.RazorPages;
using WebAppDatabase.Models;
using WebAppDatabase.Services;

namespace WebAppDatabase.Pages.Students;

public class Delete : PageModel
{
    
    private readonly IStudentService _studentService;
    public List<Error> Errors { get; set; } = [];
    
    public Delete(IStudentService studentService)
    {
        _studentService = studentService;
    }

    public void OnGet(int id)
    {
        try
        {
            _studentService.DeleteStudent(id);
            Response.Redirect("/Students/getall");
        }
        catch(Exception e)
        {
            Errors.Add(new Error (" ", e.Message," "));
        }
    }
}