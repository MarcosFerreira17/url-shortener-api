using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UrlShortener.Domain.Services;
public interface KeyGenerationService
{
    public string GenerateKey();
}
