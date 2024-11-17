using Microsoft.EntityFrameworkCore;
using PlatformService.Data;
using PlatformService.SyncDataServices.Http;
using RestSharp;

var builder = WebApplication.CreateBuilder(args);

// In-memory database for development
builder.Services.AddDbContext<PlatformDbContext>(options =>
    options.UseInMemoryDatabase("InMem"));

builder.Services.AddScoped<IPlatformRepo, PlatformRepo>();

// Register HttpClient and RestClient
builder.Services.AddSingleton<RestClient>();  // Singleton RestClient for injection
builder.Services.AddSingleton<IConfiguration>(builder.Configuration);  // IConfiguration to fetch app settings

// Register the HttpCommandDataClient
builder.Services.AddScoped<ICommandDataClient, HttpCommandDataClient>();

// Add Swagger for API documentation
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});
var app = builder.Build();
app.UseCors("AllowAll");

// Logging the Command Service endpoint URL
Console.WriteLine($"Command Service Endpoint: {builder.Configuration["CommandService"]}");

app.UseSwagger();
app.UseSwaggerUI();

//app.UseHttpsRedirection();
app.UseRouting();
app.UseEndpoints(endpoints => endpoints.MapControllers());

PrepDb.PrepPopulation(app);
app.Run();
