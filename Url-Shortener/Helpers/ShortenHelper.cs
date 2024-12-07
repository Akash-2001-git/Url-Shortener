using Microsoft.EntityFrameworkCore;
using Url_Shortener.Database;

namespace Url_Shortener.Helpers
{
    public class ShortenHelper
    {
        private const int MAX_LENGTH = 7;
        private const string AllLettersAndNumbers = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
        public static async Task<string> GenerateUniqueCode(AppDbContext appDbContext)
        {
            Random random = new();

            var chars = new char[MAX_LENGTH];

            while (true)
            {
                for (int i = 0; i < MAX_LENGTH; i++)
                {
                    var randomIndex = random.Next(AllLettersAndNumbers.Length);

                    chars[i] = AllLettersAndNumbers[randomIndex];
                }

                string uniqueCode = new string(chars);

                if (!await IsUniqueCodeExist(appDbContext, uniqueCode))
                {
                    return uniqueCode;
                }
            }
        }
        public static string? ExtractUniqueCode(string url)
        {
            if (Uri.TryCreate(url, UriKind.Absolute, out var uri))
            {
                var segments = uri.Segments;
                if (segments.Length == 2) // The host and one segment
                {
                    var uniqueCode = segments[1].Trim('/');
                    if (uniqueCode.Length == 7)
                    {
                        return uniqueCode;
                    }
                }
            }
            return null;
        }
        private static async Task<bool> IsUniqueCodeExist(AppDbContext appDbContext, string uniqueCode) =>
        await appDbContext.ShortenUrl
        .AnyAsync(d => string.Equals(d.UniqueCode, uniqueCode));
    }
}

