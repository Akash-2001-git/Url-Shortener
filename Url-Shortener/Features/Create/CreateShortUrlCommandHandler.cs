using MediatR;
using Url_Shortener.Database;
using Url_Shortener.Helpers;
using Url_Shortener.Model;

namespace Url_Shortener.Features.Create
{
    public class CreateShortUrlCommandHandler : IRequestHandler<CreateShortUrlCommand, string>
    {
        private readonly AppDbContext _appDbContext;
        public CreateShortUrlCommandHandler(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public async Task<string> Handle(CreateShortUrlCommand request, CancellationToken cancellationToken)
        {
            var uniqueCode = await ShortenHelper.GenerateUniqueCode(_appDbContext);
            var uri = new Uri(request.LongUrl);
            var shortUrl = FormShortenUrl(uri, uniqueCode);
            var shortenUrl = new ShortenUrl
            {
                Id = Guid.NewGuid(),
                LongUrl = request.LongUrl,
                ShortUrl = shortUrl,
                UniqueCode = uniqueCode,
                CreatedAt = DateTime.UtcNow,
                ExpirationDate = DateTime.UtcNow.AddDays(30), //set to 30 days
            };
            await _appDbContext.ShortenUrl.AddAsync(shortenUrl, cancellationToken);
            await _appDbContext.SaveChangesAsync(cancellationToken);
            return shortUrl;
        }

        private string FormShortenUrl(Uri uri, string uniqueCode)
        {
            return $"{uri.Scheme}://{uri.Host}/{uniqueCode}";
        }

    }
}
