using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebAppDatabase.DTO;
using WebAppDatabase.Models;
using WebAppDatabase.Services;

namespace WebAppDatabase.Pages.Students;

public class Index : PageModel
{
    public List<StudentReadonlyDTO> StudentsReadonlyDTO { get; set; } = [];
    public Error ErrorObj { get; set; } = new();
    
    private readonly IStudentService _studentService;

    public Index(IStudentService studentService)
    {
        _studentService = studentService;
    }

    public IActionResult OnGet()
    {
        try
        {
            ErrorObj = new();
            StudentsReadonlyDTO = _studentService.GetAllStudents();
        }
        catch (Exception ex)
        {
            ErrorObj = new Error("", ex.Message, "");
        }
        return Page();
    }
}