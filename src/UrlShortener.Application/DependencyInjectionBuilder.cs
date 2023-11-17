using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using UrlShortener.Application.Interfaces;
using UrlShortener.Application.Services;
using UrlShortener.Application.Settings;
using UrlShortener.Domain.Url.Repositories.Interfaces;
using UrlShortener.Infra.Configurations;
using UrlShortener.Infra.Repositories;

namespace UrlShortener.Application;
public static class DependencyInjectionBuilder
{
    public static void InjectApplicationDependencies(this IServiceCollection services)
    {
        services.AddScoped<IUrlService, UrlService>();
        services.AddScoped<ICacheService, CacheService>();
        services.AddHttpClient<IIpStackService, IpStackService>();
    }

    public static void InjectInfraDependencies(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<MongoSettings>(configuration.GetSection("MongoSettings"));

        services.AddStackExchangeRedisCache(options =>
        {
            options.Configuration = configuration.GetConnectionString("Redis");
            options.InstanceName = "Sample_";
        });

        services.AddSingleton<IUrlRepository, UrlRepository>();
    }

    public static void InjectSettingsDependencies(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<IpStackSettings>(configuration.GetSection("IpStackSettings"));
    }

    public static void ConfigureCors(this IServiceCollection services)
    {
        services.AddCors(options =>
        {
            options.AddPolicy("FreeCorsPolicy", builder =>
                                            builder.AllowAnyOrigin()
                                                    .AllowAnyMethod()
                                                    .AllowAnyHeader());
        });
    }

}
