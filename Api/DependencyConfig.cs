using Microsoft.OpenApi.Models;

namespace Api;

public static class DependencyConfig
{
    public static void AddApiConfigure(this IServiceCollection services)
    {
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(s =>
        {
            s.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "Vertical Slice Architecture Angelier",
                Version = "v1"
            });

        });

        services.AddControllers();

        services.AddProblemDetails();

        services.AddRouting(options => options.LowercaseUrls = true);

        services.AddOpenApi();

    }

    public static void UseApiConfigure(this WebApplication app)
    {
        app.UseExceptionHandler();

        app.MapOpenApi();
        app.UseSwagger();
        app.UseSwaggerUI(options =>
        {
            options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
        });

        app.UseHttpsRedirection();

        app.MapControllers();

    }

}