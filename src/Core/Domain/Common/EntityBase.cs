using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Common;
public abstract class EntityBase
{
    public long Id { get; set; }
    public DateTime CreationDate { get; set; }
    public DateTime ExpirationDate { get; set; }

    public override bool Equals(object obj)
    {
        return base.Equals(obj);
    }

    public override int GetHashCode()
    {
        return base.GetHashCode();
    }

    public override string ToString()
    {
        return base.ToString();
    }
}
