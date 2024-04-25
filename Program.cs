using UserApi.Config;
using UserApi.Utils;

DotEnv.Load(Path.Combine(Directory.GetCurrentDirectory(), ".env"));

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwagger();

builder.Services.AddCustomRouting();
builder.Services.AddCustomCors();

var app = builder.Build();

app.Urls.Add($"http://*:{Environment.GetEnvironmentVariable("HTTP_SERVER_PORT")}");

app.UseCustomSwagger();
app.UseCustomSwaggerUI();

app.MapControllers();

// app.UseHttpsRedirection();

app.Run();