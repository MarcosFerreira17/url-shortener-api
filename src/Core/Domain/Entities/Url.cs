using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Entities;
public class Url : EntityBase
{
    public string LongUrl { get; set; }
    public string ShortUrl { get; set; }
}
