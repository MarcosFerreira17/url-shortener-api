using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace UrlShortener.Domain.Url.Entities;
public class UrlAccessTrace
{
    public UrlAccessTrace(string id, string continentName, string countryName, string regionName, string city, string hash)
    {
        Id = id;
        ContinentName = continentName;
        CountryName = countryName;
        RegionName = regionName;
        City = city;
        Hash = hash;
        CreatedAt = DateTime.UtcNow;
    }
    public UrlAccessTrace(string continentName, string countryName, string regionName, string city, string hash)
    {
        ContinentName = continentName;
        CountryName = countryName;
        RegionName = regionName;
        City = city;
        Hash = hash;
        CreatedAt = DateTime.UtcNow;
    }

    [BsonId]
    [BsonElement("UrlAccessTraceId")]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; private set; }
    public string ContinentName { get; private set; }
    public string CountryName { get; private set; }
    public string RegionName { get; private set; }
    public string City { get; private set; }
    public string Hash { get; private set; }
    public DateTime CreatedAt { get; private set; }
}
