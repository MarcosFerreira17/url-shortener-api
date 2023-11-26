using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Driver;
using UrlShortener.Domain.Common.Interfaces.Repositories;
using UrlShortener.Domain.Url.Entities;

namespace UrlShortener.Domain.Url.Interface.Repositories;

public interface IUrlAccessTraceRepository : IBaseRepository<UrlAccessTrace>
{
    public Task<List<UrlAccessTrace>> GetByFilterAsync(FilterDefinition<UrlAccessTrace> filter);
}