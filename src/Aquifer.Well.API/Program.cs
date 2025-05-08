using System.Text.Json;
using System.Text.Json.Serialization;
using Aquifer.Common.Extensions;
using Aquifer.Common.Messages;
using Aquifer.Common.Middleware;
using Aquifer.Common.Services;
using Aquifer.Common.Services.Caching;
using Aquifer.Data;
using Aquifer.Data.Entities;
using Aquifer.Well.API.Configuration;
using Aquifer.Well.API.OpenApi;
using Aquifer.Well.API.Telemetry;
using FastEndpoints;
using Microsoft.ApplicationInsights.Extensibility;
using Microsoft.AspNetCore.Http.Json;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

var configuration = builder.Configuration.Get<ConfigurationOptions>() ??
    throw new InvalidOperationException($"Unable to bind {nameof(ConfigurationOptions)}.");

builder.Services.AddOptions<ConfigurationOptions>().Bind(builder.Configuration);

builder.Services
    .AddSingleton(cfg => cfg.GetService<IOptions<ConfigurationOptions>>()!.Value.AzureStorageAccount)
    .AddDbContext<AquiferDbReadOnlyContext>(
        options => options
            .UseAzureSql(configuration.ConnectionStrings.BiblioNexusReadOnlyDb, providerOptions => providerOptions.EnableRetryOnFailure(3))
            .EnableSensitiveDataLogging(builder.Environment.IsDevelopment())
            .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking))
    .AddScoped<AquiferDbContext, AquiferDbReadOnlyContext>()
    .Configure<JsonOptions>(options => options.SerializerOptions.Converters.Add(new JsonStringEnumConverter()))
    .AddFastEndpoints()
    .AddMemoryCache()
    .AddScoped<ICachingLanguageService, CachingLanguageService>()
    .AddScoped<ICachingApiKeyService, CachingApiKeyService>()
    .AddSingleton<IQueueClientFactory, QueueClientFactory>()
    .AddTrackResourceContentRequestServices()
    .AddSingleton<ITelemetryInitializer, RequestTelemetryInitializer>()
    .AddAzureClient(builder.Environment.IsDevelopment())
    .AddSwaggerDocumentSettings()
    .AddOutputCache()
    .AddApplicationInsightsTelemetry()
    .AddHealthChecks()
    .AddDbContextCheck<AquiferDbReadOnlyContext>();

builder.Services.AddOptions<ConfigurationOptions>().Bind(builder.Configuration);
builder.Services.Configure<ApiKeyAuthorizationMiddlewareOptions>(o => o.Scope = ApiKeyScope.WellApi);

var app = builder.Build();

StaticLoggerFactory.LoggerFactory = app.Services.GetRequiredService<ILoggerFactory>();

app.UseHealthChecks("/_health")
    .UseMiddleware<ApiKeyAuthorizationMiddleware>()
    .UseResponseCaching()
    .UseOutputCache()
    .UseOpenApi()
    .UseFastEndpoints(
        config =>
        {
            config.Serializer.Options.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
            config.Endpoints.Configurator = ep => ep.AllowAnonymous();
            config.Versioning.Prefix = "v";
        })
    .UseResponseCachingVaryByAllQueryKeys();

await app.GenerateApiClientAsync(SwaggerDocumentSettings.DocumentName);

app.Run();