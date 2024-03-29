using Microsoft.AspNetCore.Authorization;
using UrlShortener.API.Configurations;
using UrlShortener.Application;
using UrlShortener.Application.Middlewares;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerConfig();

builder.Services.InjectApplicationDependencies();

builder.Services.InjectInfraDependencies(builder.Configuration);

builder.Services.ConfigureCors();

var app = builder.Build();

app.UseCors("FreeCorsPolicy");

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
