using Microsoft.AspNetCore.Mvc;

namespace CompanyApi.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthController : ControllerBase
{
    private readonly ILogger<AuthController> _logger;
    public AuthController(ILogger<AuthController> logger)
    {
        _logger = logger;
    }

    [HttpGet("GetToken")]
    public async Task<IActionResult> GetToken()
    {
        return Ok("GetToken OK!");
    }
}