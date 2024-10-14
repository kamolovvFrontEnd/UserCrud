using System.ComponentModel.DataAnnotations;
using Core.Dtos.GadgetDtos;
using Core.Entity;

namespace Core.Dtos.UserDtos;

public class UserDto
{
    [Key]
    public int Id { get; set; }
    [Required, MaxLength(100)]
    public string FullName { get; set; }
    [Required]
    public int Age { get; set; }
    public List<Gadget> Gadget { get; set; }
    public string Role { get; set; }
    public required string Login { get; set; }
}