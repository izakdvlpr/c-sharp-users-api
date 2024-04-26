using System.Reflection;
using Microsoft.OpenApi.Models;

namespace UserApi.Config;

public static class SwaggerExtension
{
    public static void AddSwagger(this IServiceCollection services)
    {
        services.AddSwaggerGen(options =>
        {
            options.SupportNonNullableReferenceTypes();
            options.SwaggerDoc("v1", new OpenApiInfo
            {
                Version = "v1",
                Title = "Users API",
                Description = "A simple example ASP.NET Core Web API",
                TermsOfService = new Uri("https://example.com/terms"),
                Contact = new OpenApiContact
                {
                    Name = "Example Contact",
                    Url = new Uri("https://example.com/contact")
                },
                License = new OpenApiLicense
                {
                    Name = "Example License",
                    Url = new Uri("https://example.com/license")
                }
            });
            options.IncludeXmlComments(
                Path.Combine(AppContext.BaseDirectory,
                    $"{Assembly.GetExecutingAssembly().GetName().Name}.xml")
            );
        });
    }
    
    public static void UseCustomSwagger(this IApplicationBuilder app)
    {
        app.UseSwagger(option =>
        {
            option.RouteTemplate = "docs/{documentName}/swagger.json";
        });
    }

    public static void UseCustomSwaggerUI(this IApplicationBuilder app)
    {
        app.UseSwaggerUI(options =>
        {
            options.SwaggerEndpoint("/docs/v1/swagger.json", "v1");
            options.RoutePrefix = "docs";
            options.DocumentTitle = "Users API";
        });
    }
}