using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Domain.Common;
public abstract class EntityBase
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }
    public DateTime CreationDate { get; set; }
    public DateTime ExpirationDate { get; set; }
}
