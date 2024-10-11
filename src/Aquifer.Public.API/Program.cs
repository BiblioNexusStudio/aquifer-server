using System.Text.Json;
using System.Text.Json.Serialization;
using Aquifer.Common.Middleware;
using Aquifer.Common.Services;
using Aquifer.Data;
using Aquifer.Public.API.Configuration;
using Aquifer.Public.API.OpenApi;
using Aquifer.Public.API.Services;
using FastEndpoints;
using Microsoft.AspNetCore.Http.Json;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration.Get<ConfigurationOptions>();

builder.Services
    .AddDbContext<AquiferDbContext>(options => options
        .UseSqlServer(configuration?.ConnectionStrings.BiblioNexusDb, providerOptions => providerOptions.EnableRetryOnFailure(3))
        .EnableSensitiveDataLogging(sensitiveDataLoggingEnabled: builder.Environment.IsDevelopment())
        .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking))
    .Configure<JsonOptions>(options => options.SerializerOptions.Converters.Add(new JsonStringEnumConverter()))
    .AddFastEndpoints()
    .AddMemoryCache()
    .AddCachingServices()
    .AddSingleton<IResourceContentRequestTrackingService, ResourceContentRequestTrackingService>()
    .AddAzureClient(builder.Environment.IsDevelopment())
    .AddSwaggerDocumentSettings()
    .AddOutputCache()
    .AddApplicationInsightsTelemetry()
    .AddHealthChecks()
    .AddDbContextCheck<AquiferDbContext>();

builder.Services.AddOptions<ConfigurationOptions>().Bind(builder.Configuration);

var app = builder.Build();

StaticLoggerFactory.LoggerFactory = app.Services.GetRequiredService<ILoggerFactory>();

app.UseHealthChecks("/_health")
    .UseResponseCaching()
    .UseOutputCache()
    .UseFastEndpoints(config =>
    {
        config.Serializer.Options.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
        config.Endpoints.Configurator = ep => ep.AllowAnonymous();
    })
    .UseOpenApi()
    .UseReDoc(options => options.Path = "/docs");

app.UseResponseCachingVaryByAllQueryKeys();

app.Run();