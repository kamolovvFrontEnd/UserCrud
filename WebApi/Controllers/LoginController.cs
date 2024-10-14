using Core.Dtos.AccountDto;
using Core.Dtos.AccountDto.Response;
using Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class LoginController(AccountService service) : ControllerBase
{
    [HttpPost("login")]
    public async Task<ActionResult<LoginResponse>> Login(UserLoginDto user)
    {
        var loginResponse = await service.Login(user);

        if (loginResponse == null)
        {
            return Unauthorized("Invalid credentials");
        }

        return Ok(loginResponse);
    }
}