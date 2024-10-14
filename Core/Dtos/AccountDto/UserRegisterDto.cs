namespace Core.Dtos.AccountDto;

public class UserRegisterDto 
{
    public string? Fullname { get; set; }
    public int Age { get; set; }
    public string? Role { get; set; }
    public string? Login { get; set; }
    public string? Password { get; set; }
}