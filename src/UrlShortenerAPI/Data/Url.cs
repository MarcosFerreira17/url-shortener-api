using MongoDB.Bson.Serialization.Attributes;

namespace UrlShortenerAPI.Data;
public class Url
{
    [BsonId]
    [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
    public string Id { get; set; }
    [BsonElement("original_url")]
    public string OriginalUrl { get; set; }
    [BsonElement("hash")]
    public string Hash { get; set; }
    public virtual string GerenateHash(string OriginalUrl)
    {
        return null;
    }
}
