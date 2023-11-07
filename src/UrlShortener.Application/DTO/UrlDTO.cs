using System.ComponentModel.DataAnnotations;

namespace UrlShortener.Application.DTO;
public class UrlDTO
{
    [Url]
    [Required]
    public string Url { get; set; }
}
