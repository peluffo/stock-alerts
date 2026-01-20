using Microsoft.AspNetCore.Mvc;
using StockAlerts.Domain;

namespace StockAlerts.Api.Controllers;

[ApiController]
[Route("watchlist")]
public sealed class WatchlistController : ControllerBase
{
    [HttpGet]
    public IActionResult Get() => Ok(Array.Empty<WatchlistItem>());

    [HttpPost]
    public IActionResult Create([FromBody] WatchlistItem item)
    {
        item.Id = Guid.NewGuid();
        return Ok(item);
    }

    [HttpDelete("{id:guid}")]
    public IActionResult Delete(Guid id) => NoContent();
}
