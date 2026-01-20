using Microsoft.AspNetCore.Mvc;

namespace StockAlerts.Api.Controllers;

[ApiController]
[Route("auth")]
public sealed class AuthController : ControllerBase
{
    [HttpPost("register")]
    public IActionResult Register([FromBody] RegisterRequest request)
    {
        return Ok(new { message = "Registration queued", request.Email });
    }

    [HttpPost("login")]
    public IActionResult Login([FromBody] LoginRequest request)
    {
        return Ok(new { token = "demo-token", refreshToken = "demo-refresh" });
    }

    [HttpPost("refresh")]
    public IActionResult Refresh([FromBody] RefreshRequest request)
    {
        return Ok(new { token = "demo-token", refreshToken = "demo-refresh" });
    }
}

public sealed record RegisterRequest(string Email, string Password, string DisplayName);
public sealed record LoginRequest(string Email, string Password);
public sealed record RefreshRequest(string RefreshToken);
