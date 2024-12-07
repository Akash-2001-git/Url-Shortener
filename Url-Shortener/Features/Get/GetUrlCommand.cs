using MediatR;

namespace Url_Shortener.Features.Get
{
    public record GetUrlCommand(string ShortUrl) : IRequest<string>;
}
