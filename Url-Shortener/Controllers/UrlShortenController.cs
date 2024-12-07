using MediatR;
using Microsoft.AspNetCore.Mvc;
using Url_Shortener.Features.Create;
using Url_Shortener.Features.Get;
using Url_Shortener.Filters;

namespace Url_Shortener.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UrlShortenController(IMediator mediator) : ControllerBase
    {
        [HttpPost("short-url")]
        [EndpointSummary("Create Short Url")]
        [ProducesResponseType(typeof(string), StatusCodes.Status201Created)]
        [ValidUrl("longUrl")]
        public async Task<IActionResult> CreateShortUrl(string longUrl)
        {
            var command = new CreateShortUrlCommand(longUrl);
            var result = await mediator.Send(command);
            return CreatedAtAction(nameof(CreateShortUrl), new { shortUrl = result });
        }

        [HttpGet("long-url")]
        [EndpointSummary("Get Long Url")]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ValidUrl("longUrl")]
        public async Task<IActionResult> GetLongUrl([FromQuery] string shortUrl)
        {
            var command = new GetUrlCommand(shortUrl);
            var result = await mediator.Send(command);
            if (result is null)
            {
                var problemDetails = new ProblemDetails
                {
                    Status = StatusCodes.Status404NotFound,
                    Title = "URL not found",
                    Detail = "The requested short URL does not exist."
                };
                return NotFound(problemDetails);
            }
            return Ok(new { longUrl = result });
        }
    }
}
