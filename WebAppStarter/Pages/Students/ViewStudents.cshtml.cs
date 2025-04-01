using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebAppStarter.DTO;
using WebAppStarter.Models;

namespace WebAppStarter.Pages.Students;

public class ViewStudents : PageModel
{
    public List<StudentReadOnlyDTO> StudentsReadOnlyDTOs { get; set; } = [
    MapStudentToDTO(new Student() {Id = 1 , Firstname = "John", Lastname = "Doe"}),
        MapStudentToDTO(new Student() {Id= 2 , Firstname = "Mike" , Lastname = "James"}),
            MapStudentToDTO(new Student() {Id= 3 , Firstname = "Nick" , Lastname = "Jones"})
    ];
    
    public IActionResult OnGet()
    {
        if (Request.Query.TryGetValue("lastname", out var lastname))
        {
            StudentsReadOnlyDTOs = StudentsReadOnlyDTOs.Where(s => s.Lastname == lastname).ToList();
            return Page();
        }
        return Page();
    }

    private static StudentReadOnlyDTO MapStudentToDTO(Student student)
    {
        return new StudentReadOnlyDTO()
        {
            Id = student.Id,
            Firstname = student.Firstname,
            Lastname = student.Lastname,
        };
    }
}