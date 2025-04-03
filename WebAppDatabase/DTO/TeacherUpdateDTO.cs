using System.ComponentModel.DataAnnotations;

namespace WebAppDatabase.DTO;

public class TeacherUpdateDTO : BaseDTO
{
    [Required(ErrorMessage = "Firstname is required")]
    [MinLength(3, ErrorMessage = "Firstname must be at least 3 characters long")]
    public string? Firstname { get; set; }
    
    [Required(ErrorMessage = "Lastname is required")]
    [MinLength(3, ErrorMessage = "Lastname must be at least 3 characters long")]
    public string? Lastname { get; set; }
    
    [Required(ErrorMessage = "TaxNumber is required")]
    [RegularExpression(@"^\d{9,10}$", ErrorMessage = "Invalid Tax Number. TaxNumber must be at least 9 characters long")]
    public string? TaxNumber { get; set; }
    
    [Required(ErrorMessage = "Email is required")]
    [EmailAddress(ErrorMessage = "Invalid email address")]
    public string? Email { get; set; }

    public TeacherUpdateDTO() { }
    
    public TeacherUpdateDTO(int id,string? firstname, string? lastname, string? taxNumber, string? email)
    {
        Id = id;
        Firstname = firstname;
        Lastname = lastname;
        TaxNumber = taxNumber;
        Email = email;
    }
}