using System.Text.Json;
using System.Text.Json.Serialization;
using Aquifer.API.Configuration;
using Aquifer.API.Modules;
using Aquifer.API.Services;
using Aquifer.API.Telemetry;
using Aquifer.Common.Clients;
using Aquifer.Common.Messages;
using Aquifer.Common.Middleware;
using Aquifer.Common.Services;
using Aquifer.Common.Services.Caching;
using Aquifer.Data;
using Aquifer.Data.Entities;
using Aquifer.Data.Services;
using FastEndpoints;
using Microsoft.ApplicationInsights.Extensibility;
using Microsoft.AspNetCore.Http.Json;
using Microsoft.AspNetCore.HttpLogging;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration.Get<ConfigurationOptions>() ??
    throw new InvalidOperationException($"Unable to bind {nameof(ConfigurationOptions)}.");

builder.Services
    .AddAuthServices(configuration.JwtSettings)
    .AddCors()
    .AddOutputCache()
    .AddMemoryCache()
    .AddApplicationInsightsTelemetry()
    .AddSingleton<ITelemetryInitializer, RequestTelemetryInitializer>()
    .AddDbContext<AquiferDbContext>(
        options => options
            .UseAzureSql(configuration.ConnectionStrings.BiblioNexusDb, providerOptions => providerOptions.EnableRetryOnFailure(3))
            .EnableSensitiveDataLogging(builder.Environment.IsDevelopment())
            .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking))
    .RegisterModules()
    .Configure<JsonOptions>(options => options.SerializerOptions.Converters.Add(new JsonStringEnumConverter()))
    .AddHttpLogging(
        logging =>
        {
            logging.LoggingFields = HttpLoggingFields.Response;
            logging.RequestBodyLogLimit = 4096;
            logging.ResponseBodyLogLimit = 4096;
        })
    .AddQueueServices(configuration.ConnectionStrings.AzureStorageAccount)
    .AddScoped<IUserService, UserService>()
    .AddScoped<IResourceHistoryService, ResourceHistoryService>()
    .AddScoped<IResourceContentSearchService, ResourceContentSearchService>()
    .AddScoped<ICachingLanguageService, CachingLanguageService>()
    .AddScoped<ICachingApiKeyService, CachingApiKeyService>()
    .AddSingleton<IAzureKeyVaultClient, AzureKeyVaultClient>()
    .AddQueueMessagePublisherServices()
    .AddAzureClient(builder.Environment.IsDevelopment())
    .AddFastEndpoints()
    .AddResponseCaching()
    .AddHealthChecks()
    .AddDbContextCheck<AquiferDbContext>();

if (builder.Environment.IsDevelopment())
{
    builder.Services.AddResponseCompression(options => options.Providers.Add<GzipCompressionProvider>());
}

builder.Services.AddOptions<ConfigurationOptions>().Bind(builder.Configuration);
builder.Services.Configure<ApiKeyAuthorizationMiddlewareOptions>(o => o.Scope = ApiKeyScope.InternalApi);

var app = builder.Build();

StaticLoggerFactory.LoggerFactory = app.Services.GetRequiredService<ILoggerFactory>();

app.UseCors(b => b.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
app.UseHealthChecks("/_health");
if (builder.Environment.IsDevelopment())
{
    app.UseResponseCompression();
}

app.UseMiddleware<ApiKeyAuthorizationMiddleware>();
app.UseAuth();
app.UseOutputCache();
if (app.Environment.IsDevelopment())
{
    app.UseHttpLogging();
}

app.UseResponseCaching().UseFastEndpoints(config => config.Serializer.Options.PropertyNamingPolicy = JsonNamingPolicy.CamelCase);

app.UseResponseCachingVaryByAllQueryKeys();

app.MapEndpoints();
app.Run();

// make this class public in order to access from integration tests
public partial class Program;