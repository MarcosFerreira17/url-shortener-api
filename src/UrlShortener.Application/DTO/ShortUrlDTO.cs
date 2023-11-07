namespace UrlShortener.Application.DTO;
public class ShortUrlDTO
{
    public string ShortUrl { get; set; }
    public string Hash { get; set; }
    public string OriginalUrl { get; set; }
    public DateTime CreatedAt { get; set; }
}
