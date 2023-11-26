using System.Net.Http.Json;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using UrlShortener.Application.DTO.IpStack;
using UrlShortener.Application.Interfaces;
using UrlShortener.Application.Settings;

namespace UrlShortener.Application.Services;
public class IpStackService : IIpStackService
{
    private readonly HttpClient _httpClient;
    private readonly IpStackSettings _ipStackSettings;

    public IpStackService(IOptions<IpStackSettings> ipStackSettings, HttpClient httpClient)
    {
        _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        _ipStackSettings = ipStackSettings.Value ?? throw new ArgumentNullException(nameof(ipStackSettings)); ;
    }

    public async Task<LocationInfoDTO> GetCountryByIpAsync(string ip)
    {
        var response = await _httpClient.GetAsync($"{_ipStackSettings.Url}/{ip}?access_key={_ipStackSettings.AccessKey}");

        string result = await HandleResponse(response);

        return JsonConvert.DeserializeObject<LocationInfoDTO>(result);
    }

    private static async Task<string> HandleResponse(HttpResponseMessage response)
    {
        if (!response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsStringAsync();
            throw new HttpRequestException(content);
        }

        return await response.Content.ReadAsStringAsync();
    }
}
