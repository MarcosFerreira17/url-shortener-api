using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using UrlShortener.Domain.Common;

namespace UrlShortener.Domain.Entities;
public class ShortUrl : BaseEntity
{
    public ShortUrl(string id, DateTime createdAt, DateTime? updatedAt, string text, long longUrlId, string longUrlText) : base(createdAt, updatedAt)
    {
        Id = id;
        Text = text;
        LongUrlId = longUrlId;
        LongUrlText = longUrlText;
    }

    [BsonId]
    [BsonElement("ShortUrlId")]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; private set; }
    [BsonElement("ShortUrlText")]
    public string Text { get; private set; }
    public long LongUrlId { get; private set; }
    public string LongUrlText { get; private set; }
}
