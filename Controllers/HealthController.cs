using Microsoft.AspNetCore.Mvc;
namespace API.Controllers;

[ApiController]
[Route("/health-check")] 
public class HealthCheck : ControllerBase
{

    [HttpGet]
     public IActionResult Get()
    {
        return Ok();
    }
}
