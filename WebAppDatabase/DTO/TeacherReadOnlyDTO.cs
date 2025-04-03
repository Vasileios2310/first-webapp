using System.ComponentModel.DataAnnotations;

namespace WebAppDatabase.DTO;

public class TeacherReadOnlyDTO
{
   
    public string? Firstname { get; set; }
    
    public string? Lastname { get; set; }
    
    public string? TaxNumber { get; set; }
    
    public string? Email { get; set; }

    public TeacherReadOnlyDTO() { }
    
    public TeacherReadOnlyDTO(string? firstname, string? lastname, string? taxNumber, string? email)
    {
        Firstname = firstname;
        Lastname = lastname;
        TaxNumber = taxNumber;
        Email = email;
    }
}