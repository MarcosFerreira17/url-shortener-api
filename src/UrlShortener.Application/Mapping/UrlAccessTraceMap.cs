using AutoMapper;
using UrlShortener.Application.DTO;
using UrlShortener.Application.DTO.IpStack;
using UrlShortener.Domain.Url.Entities;

namespace UrlShortener.Application.Mapping;
public class UrlAccessTraceMap : Profile
{
    public UrlAccessTraceMap()
    {
        CreateMap<LocationInfoDTO, UrlAccessTrace>();
        CreateMap<LocationInfoDTO, UrlAccessTraceDTO>();
        CreateMap<UrlAccessTrace, UrlAccessTraceDTO>().ReverseMap();
    }
}
