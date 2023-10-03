using Microsoft.AspNetCore.Authorization;
using UrlShortener.API.Configurations;
using UrlShortener.API.Middlewares;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerConfig();

builder.Services.AddApplicationServicesConfig();

var app = builder.Build();

app.UseMiddleware<GlobalExceptionHandler>();

app.UseSwaggerConfig();

app.UseRouting();

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    if (builder.Environment.IsDevelopment())
        endpoints.MapControllers().WithMetadata(new AllowAnonymousAttribute());
    else
        endpoints.MapControllers();
});

app.Run();
