using Microsoft.AspNetCore.Mvc;

namespace StockAlerts.Api.Controllers;

[ApiController]
[Route("webhooks")]
public sealed class WebhooksController : ControllerBase
{
    [HttpPost("stripe")]
    public IActionResult Stripe([FromHeader(Name = "Stripe-Signature")] string signature)
    {
        return Ok(new { status = "received", signaturePresent = !string.IsNullOrWhiteSpace(signature) });
    }
}
