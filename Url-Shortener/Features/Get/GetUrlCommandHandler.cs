using MediatR;
using Microsoft.EntityFrameworkCore;
using Url_Shortener.Database;
using Url_Shortener.Helpers;

namespace Url_Shortener.Features.Get
{
    public class GetUrlCommandHandler : IRequestHandler<GetUrlCommand, string>
    {
        private readonly AppDbContext _appDbContext;
        public GetUrlCommandHandler(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public async Task<string?> Handle(GetUrlCommand request, CancellationToken cancellationToken)
        {
            var uniqueCode = ShortenHelper.ExtractUniqueCode(request.ShortUrl);
            if (uniqueCode == null)
            {
                await Task.FromResult(false);
            }
            var shortenUrl = await _appDbContext.ShortenUrl.SingleOrDefaultAsync(x => x.UniqueCode == uniqueCode);
            if (shortenUrl == null)
            {
                return null;
            }
            return shortenUrl.LongUrl;
        }
    }
}
