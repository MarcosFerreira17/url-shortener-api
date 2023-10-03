using System;

namespace UrlShortener.Domain.Common;
public abstract class BaseEntity
{
    public BaseEntity(DateTime createdAt, DateTime? updatedAt = null)
    {
        CreatedAt = createdAt;
        UpdatedAt = updatedAt;
    }

    public BaseEntity(DateTime? updatedAt = null)
    {
        UpdatedAt = updatedAt;
    }

    public BaseEntity(DateTime createdAt)
    {
        CreatedAt = createdAt;
    }

    public DateTime CreatedAt { get; private set; }
    public DateTime? UpdatedAt { get; private set; }
}
