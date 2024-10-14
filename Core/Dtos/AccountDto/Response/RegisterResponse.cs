using Core.Dtos.UserDtos;

namespace Core.Dtos.AccountDto.Response;

public class RegisterResponse
{
    public UserDto? User { get; set; }
    public string? Token { get; set; }
}