using System.ComponentModel.DataAnnotations;

namespace WebAppStarter.DTO;

public class InsertStudentDTO 
{
    [Required(ErrorMessage = "Id is required")]
    public int Id { get; set; }
    [Required(ErrorMessage = "Firstname is required")]
    public string? Firstname { get; set; }
    [Required(ErrorMessage = "Lastname is required")]
    public string? Lastname { get; set; }

    public InsertStudentDTO() { }

    public InsertStudentDTO(int id, string? firstname, string? lastname)
    {
        Id = id;
        Firstname = firstname;
        Lastname = lastname;
    }

    public override string ToString()
    {
        return $"Id: {Id} , Firstname: {Firstname} , Lastname: {Lastname}";
    }
}