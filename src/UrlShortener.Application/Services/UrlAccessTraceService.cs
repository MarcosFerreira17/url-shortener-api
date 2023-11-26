using AutoMapper;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;
using UrlShortener.Application.DTO;
using UrlShortener.Application.DTO.IpStack;
using UrlShortener.Application.Interfaces;
using UrlShortener.Domain.Url.Entities;
using UrlShortener.Domain.Url.Interface.Repositories;

namespace UrlShortener.Application.Services;
public class UrlAccessTraceService : IUrlAccessTraceService
{
    private readonly IUrlAccessTraceRepository _urlAccessTraceRepository;
    private readonly IIpStackService _ipStackService;
    private readonly ILogger<UrlAccessTraceService> _logger;
    private readonly ICacheService _cache;
    private readonly IMapper _mapper;
    public UrlAccessTraceService(IUrlAccessTraceRepository _urlAccessTraceRepository, IIpStackService ipStackService, IMapper mapper, ILogger<UrlAccessTraceService> logger, ICacheService cache)
    {
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _urlAccessTraceRepository = _urlAccessTraceRepository ?? throw new ArgumentNullException(nameof(_urlAccessTraceRepository));
        _ipStackService = ipStackService ?? throw new ArgumentNullException(nameof(ipStackService));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _cache = cache ?? throw new ArgumentNullException(nameof(cache));
    }

    public async Task<List<UrlAccessTraceDTO>> GetByAccessByHashAsync(string hash)
    {
        var filter = Builders<UrlAccessTrace>.Filter.Eq(options => options.Hash, hash);
        var result = await _urlAccessTraceRepository.GetByFilterAsync(filter);

        return null;
    }

    public async Task InsertAsync(string hash, string ip)
    {
        LocationInfoDTO ipLocation = await _ipStackService.GetCountryByIpAsync(ip);

        var accessTrace = _mapper.Map<UrlAccessTrace>(ipLocation);

        await _urlAccessTraceRepository.InsertOneAsync(accessTrace);
    }
}
