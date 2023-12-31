using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace api.Controllers;

[Route("[controller]/[action]")]
[ApiController]
[Produces("application/json")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(
        IAuthService authService
        )
    {
        _authService = authService;
    }

    [AllowAnonymous]
    [HttpPost]
    public async Task<ActionResult<LoginResponseDto>> Login([FromBody] UserLoginDto dto)
    {
        var token = await _authService.Login(dto);

        return new LoginResponseDto()
        {
            Token = token
        };
    }


    [AllowAnonymous]
    [HttpPost]
    public async Task<ActionResult<UserResponseDto>> Register([FromBody] UserRegisterDto dto)
    {
        var user = await _authService.Register(dto);


        return new UserResponseDto
        {
            Id = user.Id.ToString(),
            Username = user.UserName!,
        };

    }


    [HttpGet]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<ActionResult<UserResponseDto>> Me()
    {
        var userId = HttpContext.User.Claims.FirstOrDefault(x => x.Type == "id")?.Value;

        if (userId != null)
        {
            var user = await _authService.GetById(Guid.Parse(userId));
            if (user != null)
            {
                var response = new UserResponseDto
                {
                    Id = user.Id.ToString(),
                    Username = user.UserName!,
                };
                return Ok(response);
            }
        }

        return BadRequest();
    }
}