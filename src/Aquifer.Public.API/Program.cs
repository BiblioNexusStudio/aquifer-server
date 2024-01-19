using System.Text.Json;
using Aquifer.Data;
using Aquifer.Public.API.OpenApi;
using Aquifer.Public.API.Middleware;
using Azure.Storage.Queues;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AquiferDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("BiblioNexusDb"),
        providerOptions => providerOptions.EnableRetryOnFailure(3)));

builder.Services.AddFastEndpoints()
    .AddSingleton(sp =>
    {
        var connectionString = builder.Configuration?.GetConnectionString("AzureStorageAccount");
        return new QueueClient(connectionString, "your-queue-name");
    })
    .AddSwaggerDocumentSettings()
    .AddOutputCache()
    .AddHealthChecks()
    .AddDbContextCheck<AquiferDbContext>();

var app = builder.Build();
app.UseHealthChecks("/_health")
    .UseMiddleware<TrackResourceRequestMiddleware>()
    .UseResponseCaching()
    .UseOutputCache()
    .UseFastEndpoints(config =>
    {
        config.Serializer.Options.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
        config.Endpoints.Configurator = ep => { ep.AllowAnonymous(); };
    })
    .UseOpenApi()
    .UseReDoc(options => { options.Path = "/docs"; });

app.Run();
