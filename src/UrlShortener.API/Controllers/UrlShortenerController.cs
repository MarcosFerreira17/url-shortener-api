using Microsoft.AspNetCore.Mvc;

namespace UrlShortener.API.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class UrlShortenerController : ControllerBase
{
    private readonly ILogger<UrlShortenerController> _logger;
    public UrlShortenerController(ILogger<UrlShortenerController> logger)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    [HttpGet]
    [Route("{urlId}")]
    public IActionResult Get([FromQuery] string urlId)
    {
        return Ok(urlId);
    }

    [HttpPost]
    [Route("shorten")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public IActionResult Post([FromBody] string longUrl)
    {
        return Ok("Hello World!");
    }

}
