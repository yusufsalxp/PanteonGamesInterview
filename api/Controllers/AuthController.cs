using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using api.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.ObjectPool;

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
}