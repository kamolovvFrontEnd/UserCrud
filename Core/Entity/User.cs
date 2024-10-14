using System.ComponentModel.DataAnnotations;

namespace Core.Entity;

public class User
{
    [Key]
    public int Id { get; set; }
    [Required, MaxLength(100)]
    public string? FullName { get; set; }
    [Required]
    public int Age { get; set; }
    public List<Gadget>? Gadget { get; set; }
    public string? Role { get; set; }
    public required string? Login { get; set; }
    public string? PasswordHashed { get; set; }
}