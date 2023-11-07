using Microsoft.AspNetCore.Mvc;
using UrlShortener.Application.DTO;
using UrlShortener.Application.Interfaces;

namespace UrlShortener.API.Controllers;

[ApiController]
public class UrlShortenerController : ControllerBase
{
    private readonly IUrlService _urlService;
    public UrlShortenerController(IUrlService urlService)
    {
        _urlService = urlService ?? throw new ArgumentNullException(nameof(urlService));
    }

    /// <summary>
    /// Get the original url
    /// </summary>
    /// <param name="hash"></param>
    /// <returns>Redirect</returns>
    [HttpGet]
    [Route("{hash}")]
    [ProducesResponseType(StatusCodes.Status302Found)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Get([FromRoute] string hash)
    {
        string result = await _urlService.GetUrlAsync(hash);

        return result is null ?
                        NotFound() :
                        Redirect(result);
    }

    /// <summary>
    /// Create a short url
    /// </summary>
    /// <param name="urlDTO">Url to be shortened</param>
    /// <returns>Shortened url</returns>
    [HttpPost]
    [Route("api/v1/[controller]/shorten")]
    [ProducesResponseType(200, Type = typeof(ShortUrlDTO))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Post([FromBody] UrlDTO urlDTO)
    {
        return Ok(await _urlService.CreateShortUrlAsync(urlDTO));
    }

}
