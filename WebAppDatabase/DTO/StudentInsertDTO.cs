using System.ComponentModel.DataAnnotations;

namespace WebAppDatabase.DTO;

public class StudentInsertDTO
{
    [Required(ErrorMessage = "Firstname is required.")]
    [MinLength(3, ErrorMessage = "Firstname must be at least 3 characters.")]
    public string? Firstname { get; set; }
    [Required(ErrorMessage = "Lastname is required.")]
    [MinLength(3, ErrorMessage = "Lastname must be at least 3 characters.")]
    public string? Lastname { get; set; }

    public StudentInsertDTO() { }

    public StudentInsertDTO(string? firstname, string? lastname)
    {
        Firstname = firstname;
        Lastname = lastname;
    }
}