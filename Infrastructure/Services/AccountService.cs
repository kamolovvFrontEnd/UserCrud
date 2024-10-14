using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Core.Dtos.AccountDto;
using Core.Dtos.AccountDto.Response;
using Core.Dtos.UserDtos;
using Core.Entity;
using Infrastructure.Database;
using Infrastructure.Utilities;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Infrastructure.Services;

public class AccountService(Data _data, IConfiguration _configuration)
{
    private static readonly TimeSpan TokenLifeTime = TimeSpan.FromHours(8);
    
    public async Task<RegisterResponse> AddUser(UserRegisterDto? user)
    {
        if (await _data.Users.AnyAsync(x => x.Login == user!.Login))
            throw new ArgumentException("User already exists");

        var users = new User()
        {
            Login = user!.Login,
            FullName = user.Fullname,
            Role = user.Role,
            PasswordHashed = PasswordUtilitiy.HashPassword(user.Password),
            Age = user.Age,
        };

        await _data.Users.AddAsync(users);

        await _data.SaveChangesAsync();

        UserDto? userDto = _data.Users.Select(item => new UserDto
        {
            Role = item.Role,
            Login = item.Login,
            Age = item.Age,
            FullName = item.FullName
        }).FirstOrDefault();

        RegisterResponse response = new()
        {
            User = userDto,
            Token = GenerateToken(users)
        };

        return response;
    }
    
    public async Task<LoginResponse> Login(UserLoginDto? login)
    {
        User? user = await _data.Users.FirstOrDefaultAsync(x => x.Login == login!.Login);
        if (user == null)
        {
            throw new BadHttpRequestException("Не тот логин братан");
        }

        if (user.PasswordHashed != Utilities.PasswordUtilitiy.HashPassword(login.Password))
        {
            throw new BadHttpRequestException("Радной не правильный пароль");
        }

        var responseDto = new UserDto()
        {
            Login = user.Login,
            FullName = user.FullName,
            Role = user.Role,
            Age = user.Age,
        };

        LoginResponse loginResponse = new()
        {
            User = responseDto,
            Token = GenerateToken(user)
        };
        
        return loginResponse;
    }
    
    private string GenerateToken(User user)
    {
        JwtSecurityTokenHandler handler = new();

        string securityCode = _configuration["SecuritySetting:SecurityCode"];
        if (string.IsNullOrEmpty(securityCode))
        {
            throw new ArgumentNullException(nameof(securityCode), "SecurityCode is not set in the configuration.");
        }

        byte[] key = Encoding.ASCII.GetBytes(securityCode);

        var claims = new List<Claim>()
        {
            new(ClaimTypes.Name, user.Id.ToString()),
            new(ClaimTypes.Role, user.Role)
        };

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.Add(TokenLifeTime),
            Issuer = _configuration["SecuritySetting:Issuer"] ??
                     throw new ArgumentNullException(nameof(_configuration), "Issuer is not set in the configuration."),
            Audience = _configuration["SecuritySetting:Audience"] ??
                       throw new ArgumentNullException(nameof(_configuration),
                           "Audience is not set in the configuration."),
            SigningCredentials =
                new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };

        var token = handler.CreateToken(tokenDescriptor);
        return $"Bearer {handler.WriteToken(token)}";
    }
}