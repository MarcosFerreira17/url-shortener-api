using Domain.Common;
using MongoDB.Bson.Serialization.Attributes;

namespace Domain.Entities;
public class Url : EntityBase
{
    [BsonElement("original_url")]
    public string OriginalUrl { get; set; }
    [BsonElement("hash")]
    public string Hash { get; set; }
    public virtual string GerenateHash(string OriginalUrl)
    {
        return null;
    }
}
