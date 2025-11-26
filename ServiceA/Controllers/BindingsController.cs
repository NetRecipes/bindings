using Dapr.Client;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;

namespace ServiceA.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BindingsController(
    DaprClient daprClient,
    ILogger<BindingsController> logger) : ControllerBase
{
    [HttpPost("cron")]
    public async Task<IActionResult> Cron()
    {
        logger.LogInformation("Cron trigger received.");
        return Ok();
    }

    [HttpPost("upload")]
    [Consumes(MediaTypeNames.Multipart.FormData)]
    public async Task<IActionResult> Upload([FromForm] UploadRequest request)
    {
        var file = request.File;

        if (file == null || file.Length ==0)
        {
            logger.LogError("No file uploaded");
            return BadRequest();
        }

        byte[] fileBytes;
        using (var ms = new MemoryStream())
        {
            await file.CopyToAsync(ms);
            fileBytes = ms.ToArray();
        }

        var fileName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";

        Dictionary<string, string> metadata = new()
        {
            { "fileName", fileName },
            { "blobName", fileName }
        };

        await daprClient.InvokeBindingAsync(
            "storage",
            "create",
            fileBytes,
            metadata);

        return Ok(fileName);
    }

    [HttpGet("download")]
    public async Task<IActionResult> Download(string fileName)
    {
        Dictionary<string, string> metadata = new()
        {
            { "fileName", fileName },
            { "blobName", fileName }
        };

        var bindingRequest = new BindingRequest("storage", "get");
        bindingRequest.Metadata.Add("fileName", fileName);
        bindingRequest.Metadata.Add("blobName", fileName);

        var response = await daprClient.InvokeBindingAsync(bindingRequest);

        byte[] fileBytes = response.Data.ToArray();

        return File(fileBytes, MediaTypeNames.Application.Octet, fileName);
    }
}
