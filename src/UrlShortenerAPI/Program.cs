using UrlShortenerAPI.Configurations;
using UrlShortenerAPI.Data;

var builder = WebApplication.CreateBuilder(args);
builder.Services.Configure<UrlShortenerDatabaseSettings>(builder.Configuration.GetSection("UrlShortenerDatabaseSettings"));
builder.Services.AddSingleton<UrlService>();
builder.Services.ConfigureCors();
var app = builder.Build();
app.UseCors("CorsPolicy");

/// <summary>
/// 
/// </summary>
app.MapGet("/", () => "URL Shortener API is running!");

/// <summary>
/// Get all Urls
/// </summary>
app.MapGet("/Url", async (UrlService UrlService) => await UrlService.Get());

/// <summary>
/// Get a Url by hash
/// </summary>
app.MapGet("/Url/{hash}", async (UrlService urlService, string hash) =>
{
    var Url = await urlService.Get(hash);
    return Url is null ? Results.NotFound() : Results.Redirect(Url.OriginalUrl);
});

/// <summary>
/// Create a new Url
/// </summary>
app.MapPost("/Url", async (UrlService urlService, Url Url) => Results.Ok(await urlService.Create(Url)));

/// <summary>
/// Delete a Url
/// </summary>
app.MapDelete("/Url/{id}", async (UrlService urlService, string id) =>
{
    var Url = await urlService.Get(id);
    if (Url is null) return Results.NotFound();

    await urlService.Remove(Url.Id);

    return Results.NoContent();
});

app.Run();
