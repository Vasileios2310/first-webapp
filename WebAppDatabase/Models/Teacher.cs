namespace WebAppDatabase.Models;

public class Teacher
{
    public int Id { get; set; }
    public string? Firstname { get; set; }
    public string? Lastname { get; set; }
    public string? TaxNumber { get; set; }
    public string? Email { get; set; }

    public Teacher() { }

    public Teacher(int id, string? firstname, string? lastname, string? taxNumber, string? email)
    {
        Id = id;
        Firstname = firstname;
        Lastname = lastname;
        TaxNumber = taxNumber;
        Email = email;
    }
}