using Microsoft.AspNetCore.Mvc;
namespace API.Controllers;

[ApiController]
[Route("[controller]")]
public class HealthCheck : ControllerBase
{

    [HttpGet]
     public IActionResult Get()
    {
        return Ok();
    }
}
