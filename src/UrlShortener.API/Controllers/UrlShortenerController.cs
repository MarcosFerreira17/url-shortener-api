using Microsoft.AspNetCore.Mvc;
using UrlShortener.Application.Interfaces;

namespace UrlShortener.API.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class UrlShortenerController : ControllerBase
{
    private readonly ILogger<UrlShortenerController> _logger;
    private readonly IUrlService _urlService;
    public UrlShortenerController(ILogger<UrlShortenerController> logger, IUrlService urlService)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _urlService = urlService ?? throw new ArgumentNullException(nameof(urlService));
    }

    [HttpGet]
    [Route("{urlId}")]
    public async Task Get(string urlId)
    {
        var result = await _urlService.GetUrlAsync(urlId); ;

        var url = $"http://localhost:5003/url/{result}";

        Results.Redirect(url);
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
