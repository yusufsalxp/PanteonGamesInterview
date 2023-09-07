using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using api.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;


public class AuthService : IAuthService
{
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signinManager;
    private readonly IConfiguration _configuration;

    public AuthService(
        UserManager<User> userManager,
        SignInManager<User> signinManager,
        IConfiguration configuration
        )
    {
        _userManager = userManager;
        _signinManager = signinManager;
        _configuration = configuration;
    }

    public async Task<string> Login(UserLoginDto dto)
    {
        var user = await _userManager.FindByEmailAsync(dto.Username);

        if (user != null)
        {
            var result = await _signinManager.PasswordSignInAsync(user, dto.Password, true, true);

            if (result.Succeeded)
            {
                return GenerateToken(user);
            }
            else
            {
                throw new Exception("Some errors has been occured!");
            }

        }
        else
        {
            throw new Exception("User Not Found!");
        }

    }

    public async Task<User?> GetById(Guid id)
    {
        return await _userManager.FindByIdAsync(id.ToString());
    }

    public async Task<User> Register(UserRegisterDto dto)
    {
        var user = new User
        {
            UserName = dto.Username,
            Email = dto.Email,
        };

        var result = await _userManager.CreateAsync(user, dto.Password);

        if (result.Succeeded)
        {

            user = await _userManager.FindByEmailAsync(dto.Email);
            if (user != null)
                return user;
            else
                throw new Exception("User couldn't be created!");

        }
        else
        {
            throw new Exception("Some errors has been occured!");
        }
    }

    private string GenerateToken(User user)
    {
        var claims = new List<Claim>
            {
               new Claim("id", user.Id.ToString())
            };
        var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!));
        var _TokenExpiryTimeInHour = Convert.ToInt64(_configuration["Jwt:TokenExpiryTimeInHour"]);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Issuer = _configuration["Jwt:Issuer"],
            Audience = _configuration["Jwt:Audience"],
            Expires = DateTime.UtcNow.AddHours(_TokenExpiryTimeInHour),
            SigningCredentials = new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256),
            Subject = new ClaimsIdentity(claims)
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
}