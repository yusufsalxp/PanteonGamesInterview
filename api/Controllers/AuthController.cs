using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace api.Controllers;

[Route("[controller]/[action]")]
[ApiController]
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
    public async Task<string> Login([FromBody] UserLoginDto dto)
    {
        return await _authService.Login(dto);
    }


    [AllowAnonymous]
    [HttpPost]
    public async Task<UserResponseDto> Register([FromBody] UserRegisterDto dto)
    {
        var user = await _authService.Register(dto);


        return new UserResponseDto()
        {
            Id = user.Id,
            Username = user.UserName!,
        };

    }


    [HttpGet]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<UserResponseDto> Me()
    {
        return null;
    }
}