namespace WebAppDatabase.DTO;

public class StudentReadonlyDTO:BaseDTO
{
    public string? Firstname { get; set; }
    public string? Lastname { get; set; }

    public StudentReadonlyDTO() { }

    public StudentReadonlyDTO(string? firstname, string? lastname)
    {
        Firstname = firstname;
        Lastname = lastname;
    }
}