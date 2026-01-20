using Microsoft.AspNetCore.Mvc;

namespace StockAlerts.Api.Controllers;

[ApiController]
[Route("admin")]
public sealed class AdminController : ControllerBase
{
    [HttpGet("jobs")]
    public IActionResult Jobs() => Ok(new { status = "ready" });
}
