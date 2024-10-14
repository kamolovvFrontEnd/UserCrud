using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AutoMapper;
using Core.Dtos.AccountDto;
using Core.Dtos.AccountDto.Response;
using Core.Dtos.UserDtos;
using Core.Entity;
using Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Services;

public class UserServices(Data _data, IMapper _mapper, IConfiguration? _configuration)
{
    public async Task<List<UserDto>> GetAllUsers()
    {
        List<User> users = await _data.Users.ToListAsync();

        return _mapper.Map<List<User>, List<UserDto>>(users);
    }

    public async Task<UserDto> GetUserById(int id)
    {
        User? user = await _data.Users.FindAsync(id);

        return _mapper.Map<User, UserDto>(user!);
    }
    

    public async Task<UserDto> UpdateUser(UpdateUserDto user)
    {
        User? item = await _data.Users.FindAsync(user.Id);

        if (item != null)
        {
            _mapper.Map(user, item);

            await _data.SaveChangesAsync();
        }

        UserDto dto = _mapper.Map<User, UserDto>(item!);

        return dto;
    }

    public async Task<string> DeleteUser(int id)
    {
        User? item = await _data.Users.FindAsync(id);
        if (item != null)
        {
            _data.Users.Remove(item);
            await _data.SaveChangesAsync();
            
            return "User deleted successfully";
        }
        else
        {
            return "User not found";
        }
    }
}