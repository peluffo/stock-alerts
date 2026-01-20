using Microsoft.AspNetCore.Mvc;
using StockAlerts.Domain;

namespace StockAlerts.Api.Controllers;

[ApiController]
[Route("alert-rules")]
public sealed class AlertRulesController : ControllerBase
{
    [HttpGet]
    public IActionResult Get() => Ok(Array.Empty<AlertRule>());

    [HttpPost]
    public IActionResult Create([FromBody] AlertRule rule)
    {
        rule.Id = Guid.NewGuid();
        return Ok(rule);
    }

    [HttpPut("{id:guid}")]
    public IActionResult Update(Guid id, [FromBody] AlertRule rule)
    {
        rule.Id = id;
        return Ok(rule);
    }

    [HttpDelete("{id:guid}")]
    public IActionResult Delete(Guid id) => NoContent();
}
