using Scalar.AspNetCore;
using Microsoft.EntityFrameworkCore;
using LibraryAPI;
using LibraryAPI.Models;
using FluentValidation;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddValidatorsFromAssembly(typeof(Program).Assembly, includeInternalTypes: true);

builder.Services.AddControllers();

// Configure ApiKey options for the named "ApiKey" scheme from configuration (section "LibraryApi").
// This allows using env var `LibraryApi__ApiKey`, user-secrets, or a secret store.
builder.Services.Configure<ApiKeyAuthenticationOptions>("ApiKey",
    builder.Configuration.GetSection("LibraryApi"));

builder.Services
    .AddAuthentication("ApiKey")
    .AddScheme<ApiKeyAuthenticationOptions, ApiKeyAuthenticationHandler>(
        "ApiKey",
        options => { /* named options configured via Configure<ApiKeyAuthenticationOptions>("ApiKey", ...) */ });

builder.Services.AddAuthorization();

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
        builder =>
        {
            builder.AllowAnyOrigin()
                   .AllowAnyHeader()
                   .AllowAnyMethod();
        });
});

builder.Services.AddDbContext<BookContext>(opt =>
    opt.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

// Fail fast if API key is not configured
var configuredApiKey = app.Configuration["LibraryApi:ApiKey"];
if (string.IsNullOrWhiteSpace(configuredApiKey))
{
    throw new InvalidOperationException("API key not configured. Set LibraryApi:ApiKey via environment variable or user-secrets.");
}

app.UseCors();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();

// Expose Program for integration tests (WebApplicationFactory<Program>)
public partial class Program { }
