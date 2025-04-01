using System.ComponentModel.DataAnnotations;

namespace WebAppDatabase.DTO;

public class StudentUpdateDTO : BaseDTO
{
    [Required(ErrorMessage = "Firstname is required.")]
    [MinLength(3, ErrorMessage = "Firstname must be at least 3 characters.")]
    public string? Firstname { get; set; }
    [Required(ErrorMessage = "Lastname is required.")]
    [MinLength(3, ErrorMessage = "Lastname must be at least 3 characters.")]
    public string? Lastname { get; set; }

    public StudentUpdateDTO() { }

    public StudentUpdateDTO(int id,string? firstname, string? lastname)
    {
        Id = id;
        Firstname = firstname;
        Lastname = lastname;
    }
}