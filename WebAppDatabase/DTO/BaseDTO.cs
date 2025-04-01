using System.ComponentModel.DataAnnotations;

namespace WebAppDatabase.DTO;

public abstract class BaseDTO
{
    [Required(ErrorMessage = "The {0} is required.")]
    public int Id { get; set; }

    protected BaseDTO() { }

    protected BaseDTO(int id)
    {
        Id = id;
    }
}