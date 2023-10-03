using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using UrlShortener.Domain.Common;

namespace UrlShortener.Domain.Entities;

public class User : BaseEntity
{
    public User() { }
    public User(string id, string username, string email, string password, DateTime createdAt, DateTime? updatedAt = null)
        : base(createdAt, updatedAt)
    {
        Id = id;
        Username = username;
        Email = email;
        Password = password;
    }

    public User(string username, string email, string password, DateTime createdAt, DateTime? updatedAt = null)
        : base(createdAt, updatedAt)
    {
        Username = username;
        Email = email;
        Password = password;
    }

    public User(string id, string username, string email, string password, bool acceptedSoftwareTerms, bool confirmedCode, bool active, string passwordResetToken, int userRoleId, DateTime createdAt, DateTime? updatedAt = null)
        : base(createdAt, updatedAt)
    {
        Id = id;
        Username = username;
        Email = email;
        Password = password;
        AcceptedSoftwareTerms = acceptedSoftwareTerms;
        ConfirmedCode = confirmedCode;
        Active = active;
        PasswordResetToken = passwordResetToken;
        UserRoleId = userRoleId;
    }

    public User(string id, long userRoleId) : base()
    {
        Id = id;
        UserRoleId = userRoleId;
    }

    public User(string id, bool acceptedSoftwareTerms) : base()
    {
        Id = id;
        AcceptedSoftwareTerms = acceptedSoftwareTerms;
    }

    [BsonId]
    [BsonElement("ShortUrlId")]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; private set; }
    public string Username { get; private set; }
    public string Email { get; private set; }
    public string Password { get; private set; }
    public bool AcceptedSoftwareTerms { get; set; }
    public bool ConfirmedCode { get; private set; }
    public bool Active { get; private set; }
    public string PasswordResetToken { get; private set; }
    public long UserRoleId { get; private set; }
}