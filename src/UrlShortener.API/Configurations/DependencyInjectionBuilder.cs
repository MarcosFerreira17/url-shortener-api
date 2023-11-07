namespace UrlShortener.API.Configurations;
public static class DependencyInjectionBuilder
{
    public static void ConfigureCors(this IServiceCollection services) =>
        services.AddCors(options =>
        {
            options.AddPolicy("FreeCorsPolicy", builder =>
                                            builder.AllowAnyOrigin()
                                                    .AllowAnyMethod()
                                                    .AllowAnyHeader());
        });
}