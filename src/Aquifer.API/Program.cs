using System.Text.Json;
using System.Text.Json.Serialization;
using Aquifer.API.Configuration;
using Aquifer.API.Modules;
using Aquifer.API.Services;
using Aquifer.API.Telemetry;
using Aquifer.Common.Clients;
using Aquifer.Common.Middleware;
using Aquifer.Common.Services;
using Aquifer.Data;
using FastEndpoints;
using Microsoft.ApplicationInsights.Extensibility;
using Microsoft.AspNetCore.Http.Json;
using Microsoft.AspNetCore.HttpLogging;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration.Get<ConfigurationOptions>();

builder.Services.AddAuth(configuration?.JwtSettings)
    .AddCors()
    .AddOutputCache()
    .AddApplicationInsightsTelemetry()
    .AddSingleton<ITelemetryInitializer, RequestTelemetryInitializer>()
    .AddDbContext<AquiferDbContext>(options =>
        options.UseSqlServer(configuration?.ConnectionStrings.BiblioNexusDb, providerOptions => providerOptions.EnableRetryOnFailure(3)))
    .RegisterModules()
    .Configure<JsonOptions>(options => options.SerializerOptions.Converters.Add(new JsonStringEnumConverter()))
    .AddHttpLogging(logging =>
    {
        logging.LoggingFields = HttpLoggingFields.Response;
        logging.RequestBodyLogLimit = 4096;
        logging.ResponseBodyLogLimit = 4096;
    })
    .AddAquiferHttpServices()
    .AddScoped<IUserService, UserService>()
    .AddScoped<IResourceHistoryService, ResourceHistoryService>()
    .AddSingleton<IAzureKeyVaultClient, AzureKeyVaultClient>()
    .AddSingleton<IResourceContentRequestTrackingService, ResourceContentRequestTrackingService>()
    .AddSingleton<ISendGridClient, SendGridClient>()
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

var app = builder.Build();

app.UseCors(b => b.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
app.UseHealthChecks("/_health");
if (builder.Environment.IsDevelopment())
{
    app.UseResponseCompression();
}

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