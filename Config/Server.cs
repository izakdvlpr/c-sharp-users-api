namespace UserApi.Config;

public static class Server
{
    public static void AddCustomRouting(this IServiceCollection services)
    {
        services.AddRouting(options => options.LowercaseUrls = true);
    }

    public static void AddCustomCors(this IServiceCollection services)
    {
        services.AddCors(options =>
        {
            options.AddDefaultPolicy(builder =>
            {
                builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader();
            });
        });
    }
}