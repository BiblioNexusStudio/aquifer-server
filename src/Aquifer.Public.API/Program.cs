using System.Text.Json;
using Aquifer.Data;
using Aquifer.Public.API.Swagger;
using FastEndpoints;
using FastEndpoints.Swagger;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AquiferDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("BiblioNexusDb"),
        providerOptions => providerOptions.EnableRetryOnFailure(3)));

builder.Services.AddFastEndpoints()
    .AddSwaggerDocumentSettings()
    .AddOutputCache();

var app = builder.Build();
app.UseResponseCaching()
    .UseOutputCache()
    .UseFastEndpoints(config =>
    {
        config.Serializer.Options.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
        config.Endpoints.Configurator = ep => { ep.AllowAnonymous(); };
    }).UseSwaggerGen();

app.Run();