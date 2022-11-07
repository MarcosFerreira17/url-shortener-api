using UrlShortenerAPI.Data;

var builder = WebApplication.CreateBuilder(args);
builder.Services.Configure<UrlShortenerDatabaseSettings>(builder.Configuration.GetSection("UrlShortenerDatabaseSettings"));
builder.Services.AddSingleton<UrlService>();
var app = builder.Build();

/// <summary>
/// 
/// </summary>
app.MapGet("/", () => "Urls API!");

/// <summary>
/// Get all Urls
/// </summary>
app.MapGet("/api/Urls", async (UrlService UrlService) => await UrlService.Get());

/// <summary>
/// Get a Url by id
/// </summary>
app.MapGet("/api/Urls/{id}", async (UrlService UrlService, string id) =>
{
    var Url = await UrlService.Get(id);
    return Url is null ? Results.NotFound() : Results.Ok(Url);
});

/// <summary>
/// Create a new Url
/// </summary>
app.MapPost("/api/Urls", async (UrlService UrlService, Url Url) =>
{
    await UrlService.Create(Url);
    return Results.Ok();
});

/// <summary>
/// Update a Url
/// </summary>
app.MapPut("/api/Urls/{id}", async (UrlService UrlService, string id, Url updatedUrl) =>
{
    var Url = await UrlService.Get(id);
    if (Url is null) return Results.NotFound();

    updatedUrl.Id = Url.Id;
    await UrlService.Update(id, updatedUrl);

    return Results.NoContent();
});

/// <summary>
/// Delete a Url
/// </summary>
app.MapDelete("/api/Urls/{id}", async (UrlService UrlService, string id) =>
{
    var Url = await UrlService.Get(id);
    if (Url is null) return Results.NotFound();

    await UrlService.Remove(Url.Id);

    return Results.NoContent();
});

app.Run();
