using Microsoft.AspNetCore.Mvc;
using StockAlerts.Domain;

namespace StockAlerts.Api.Controllers;

[ApiController]
[Route("signals")]
public sealed class SignalsController : ControllerBase
{
    [HttpGet]
    public IActionResult Get([FromQuery] string? symbol, [FromQuery] int page = 1, [FromQuery] int pageSize = 20)
    {
        return Ok(new
        {
            page,
            pageSize,
            symbol,
            items = Array.Empty<SignalEvent>()
        });
    }
}
