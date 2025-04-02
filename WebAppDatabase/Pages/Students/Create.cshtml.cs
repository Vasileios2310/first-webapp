using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebAppDatabase.DTO;
using WebAppDatabase.Models;
using WebAppDatabase.Services;

namespace WebAppDatabase.Pages.Students;

public class Create : PageModel
{
    [BindProperty] 
    public StudentInsertDTO StudentInsertDto { get; set; } = new();

    public List<Error> ErrorsArray { get; set; } = [];
    
    public readonly IStudentService _studentService;

    public Create(IStudentService studentService)
    {
        _studentService = studentService;
    }
    
    public void OnGet() { }

    public void OnPost()
    {
        if (!ModelState.IsValid)
        {
            return;
        }
        try
        {
            StudentReadonlyDTO? studentReadonlyDto = _studentService.InsertStudent(StudentInsertDto);
            Response.Redirect("/Students/getall");
        }
        catch (Exception ex)
        {
            ErrorsArray.Add(new Error ( "" , ex.Message, ""));
            return;
        }
    }
}