using MediatR;

namespace Url_Shortener.Features.Create
{
    public record CreateShortUrlCommand(string LongUrl) : IRequest<string>;
}
