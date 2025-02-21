using Microsoft.AspNetCore.Mvc;
namespace API.Controllers;

[ApiController]
[Route("")]
public class SettingsController : ControllerBase
{
    private readonly IConfiguration _configuration;

    public SettingsController(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    [HttpGet("/integration.json")]
    public IActionResult GetSettings()
    {
        var filePath = _configuration["Path:URL"];

        if (string.IsNullOrEmpty(filePath))
        {
            return NotFound("File path not configured.");
        }

        if (!System.IO.File.Exists(filePath))
        {
            return NotFound("Settings file not found");
        }

        var jsonContent = System.IO.File.ReadAllText(filePath);

        return Content(jsonContent, "application/json");
    }

}

