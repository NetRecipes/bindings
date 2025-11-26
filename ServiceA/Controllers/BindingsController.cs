using Microsoft.AspNetCore.Mvc;

namespace ServiceA.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BindingsController(ILogger<BindingsController> logger) : ControllerBase
{
    [HttpPost("cron")]
    public async Task<IActionResult> Cron()
    {
        logger.LogInformation("Cron trigger received.");
        return Ok();
    }
}
