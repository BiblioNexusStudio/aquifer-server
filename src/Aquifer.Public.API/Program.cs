using System.Text.Json;
using Aquifer.Data;
using Aquifer.Public.API.OpenApi;
using Aquifer.Public.API.Configuration;
using Aquifer.Public.API.Middleware;
using Azure.Storage.Queues;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration.Get<ConfigurationOptions>();

builder.Services.AddDbContext<AquiferDbContext>(options =>
    options.UseSqlServer(configuration?.ConnectionStrings.BiblioNexusDb,
        providerOptions => providerOptions.EnableRetryOnFailure(3)));

builder.Services.AddFastEndpoints()
    .AddSingleton(sp =>
    {
        var connectionString = configuration?.ConnectionStrings.AzureStorageAccount;
        return new QueueClient(connectionString, configuration?.JobSettings.JobQueueName);
    })
    .AddSwaggerDocumentSettings()
    .AddOutputCache()
    .AddHealthChecks()
    .AddDbContextCheck<AquiferDbContext>();

builder.Services.AddOptions<ConfigurationOptions>().Bind(builder.Configuration);

var app = builder.Build();
app.UseHealthChecks("/_health")
    .UseMiddleware<TrackResourceContentRequestMiddleware>()
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
