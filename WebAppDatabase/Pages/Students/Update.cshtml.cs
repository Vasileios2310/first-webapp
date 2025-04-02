using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebAppDatabase.DTO;
using WebAppDatabase.Models;
using WebAppDatabase.Services;

namespace WebAppDatabase.Pages.Students;

public class Update : PageModel
{
    
    [BindProperty] 
    public StudentUpdateDTO StudentUpdateDto { get; set; } = new();

    public List<Error> ErrorsArray { get; set; } = [];
    
    public readonly IStudentService _studentService;

    public Update(IStudentService studentService)
    {
        _studentService = studentService;
    }
    public IActionResult OnGet(int id)
    {
        try
        {
            StudentReadonlyDTO? studentReadonlyDto = _studentService.GetStudent(id);
            StudentUpdateDto = new StudentUpdateDTO()
            {
                Id = studentReadonlyDto.Id,
                Firstname = studentReadonlyDto.Firstname,
                Lastname = studentReadonlyDto.Lastname,
            };
        }
        catch (Exception ex)
        {
            ErrorsArray.Add(new Error("", ex.Message, " "));
        }
        return Page();
    }

    public void OnPost(int id)
    {
        try
        {
            StudentUpdateDto.Id = id;
            _studentService.UpdateStudent(StudentUpdateDto);
            Response.Redirect("/Students/getall");
        }
        catch (Exception ex)
        {
            ErrorsArray.Add(new Error("", ex.Message, " "));
        }
    }
}