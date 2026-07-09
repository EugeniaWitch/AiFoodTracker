using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using server.DTOs.Auth;
using server.Services.Auth;
using System.Security.Claims;

namespace server.Controllers;

[ApiController]
[Route ("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;
    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("register")]
    public async Task<ActionResult<AuthResponse>> Register(RegisterRequest request)
    {
        try
        {
            var response = await _authService.RegisterAsync(request);
            return Ok(response);
        }
        catch(InvalidOperationException exception)
        {
            return BadRequest(new{message = exception.Message});
        }
    }

    [HttpPost("login")]
    public async Task<ActionResult<AuthResponse>> Login(LoginRequest request)
    {
        try
        {
            var response = await _authService.LoginAsync(request);
            return Ok(response);
        }
        catch(UnauthorizedAccessException exception)
        {
            return Unauthorized(new{message = exception.Message});
        }
    }

    [Authorize]
    [HttpGet("me")]
    public ActionResult GetMe()
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        var name = User.FindFirst(ClaimTypes.Name)?.Value;
        var email = User.FindFirst(ClaimTypes.Email)?.Value;
        var role = User.FindFirst(ClaimTypes.Role)?.Value;

        return Ok(new
        {
            userId,
            name,
            email,
            role
        });
    }
}