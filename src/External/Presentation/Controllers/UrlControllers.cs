using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Domain.Entities;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Presentation.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class UrlController : ControllerBase
{
    private readonly IUrlRepository _repository;
    private readonly ILogger<UrlController> _logger;

    public UrlController(IUrlRepository repository, ILogger<UrlController> logger)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<Url>), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<IEnumerable<Url>>> GetUrls()
    {
        var Urls = await _repository.GetAll();
        return Ok(Urls);
    }

    [HttpGet("{id:length(24)}", Name = "GetUrl")]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [ProducesResponseType(typeof(Url), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<Url>> GetUrl(string id)
    {
        var Url = await _repository.Get(id);
        if (Url == null)
        {
            _logger.LogError($"Url with id: {id}, not found.");
            return NotFound();
        }
        return Ok(Url);
    }

    [HttpPost]
    [ProducesResponseType(typeof(Url), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<Url>> CreateUrl([FromBody] Url Url)
    {
        await _repository.Create(Url);

        return CreatedAtRoute("GetUrl", new { id = Url.Id }, Url);
    }

    [HttpPut]
    [ProducesResponseType(typeof(Url), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> Update([FromBody] Url Url)
    {
        await _repository.Update(Url);
        return Ok();
    }

    [HttpDelete("{id:length(24)}", Name = "Delete")]
    [ProducesResponseType(typeof(Url), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> DeleteUrlById(string id)
    {
        await _repository.Delete(id);
        return Ok();
    }
}
