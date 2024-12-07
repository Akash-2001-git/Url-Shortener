namespace Url_Shortener.Model
{
    public class ShortenUrl
    {
        public Guid Id { get; set; }
        public string LongUrl { get; set; }

        public string UniqueCode { get; set; }
        public string ShortUrl { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime ExpirationDate { get; set; }
    }
}
