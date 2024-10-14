using Core.Dtos.AccountDto;
using Core.Dtos.AccountDto.Response;
using Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers.OAuth;

[ApiController]
[Route("api/[controller]")]
public class OAuthController(AccountService _accountService) : Controller
{
    [HttpPost("AddUser")]
    public async Task<IActionResult> RegisterUser(UserRegisterDto user)
    {
        RegisterResponse result = await _accountService.AddUser(user);
        if (result == null)
        {
            return BadRequest("Failed to register user");
        }

        return Ok(result);
    }
}