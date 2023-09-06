using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using api.Entities;

namespace api.Controllers;

[Route("[controller]/[action]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IConfiguration _config;

    public static List<User> Users = new()
            {
                    new User(){ Username="griyum",Password="test",Email="griyum@gmail.com"}
            };
    public AuthController(IConfiguration config)
    {
        _config = config;
    }

    [AllowAnonymous]
    [HttpPost]
    public ActionResult Login([FromBody] UserLogin userLogin)
    {
        var user = Authenticate(userLogin);
        if (user != null)
        {
            var token = GenerateToken(user);
            return Ok(token);
        }

        return NotFound("user not found");
    }


    [AllowAnonymous]
    [HttpPost]
    public ActionResult Register([FromBody] UserLogin userLogin)
    {
        return NotFound("user not found");
    }

    // To generate token
    private string GenerateToken(User user)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]!));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
        var claims = new[]
        {
                new Claim(ClaimTypes.NameIdentifier,user.Username),
            };
        var token = new JwtSecurityToken(_config["Jwt:Issuer"],
            _config["Jwt:Audience"],
            claims,
            expires: DateTime.Now.AddMinutes(15),
            signingCredentials: credentials);


        return new JwtSecurityTokenHandler().WriteToken(token);

    }

    //To authenticate user
    private User? Authenticate(UserLogin userLogin)
    {
        var currentUser = Users.FirstOrDefault(x => x.Username.ToLower() ==
            userLogin.Username.ToLower() && x.Password == userLogin.Password);
        if (currentUser != null)
        {
            return currentUser;
        }
        return null;
    }
}