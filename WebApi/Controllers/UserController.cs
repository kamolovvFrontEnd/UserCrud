using Core.Dtos.UserDtos;
using Infrastructure.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController(UserServices _userServices) : ControllerBase
{
    [Authorize(Roles = "Admin")]
    [HttpGet("GetAllUsers")]
    public async Task<List<UserDto>> GetAllUsers()
    {
        return await _userServices.GetAllUsers();
    }

    [HttpGet("GetUserById")]
    public async Task<UserDto> GetUserById(int id)
    {
        return await _userServices.GetUserById(id);
    }

    [HttpDelete("DeleteUser")]
    public async Task<string> DeleteUser(int id)
    {
        return await _userServices.DeleteUser(id);
    }

    [HttpPut("UpdateUser")]
    public async Task<UserDto> UpdateUser(UpdateUserDto user)
    {
        UserDto result = await _userServices.UpdateUser(user);
        return result;
    }
}