namespace UrlShortener.Application.DTO;
public record class UrlAccessTraceDTO
{
    public string Id { get; set; }
    public string ContinentName { get; set; }
    public string CountryName { get; set; }
    public string RegionName { get; set; }
    public string City { get; set; }
    public string Hash { get; set; }
}
