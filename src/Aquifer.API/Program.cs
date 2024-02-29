using System.Text.Json;
using System.Text.Json.Serialization;
using Aquifer.API.Clients;
using Aquifer.API.Configuration;
using Aquifer.API.Middleware;
using Aquifer.API.Modules;
using Aquifer.API.Services;
using Aquifer.Common.Services;
using Aquifer.Data;
using FastEndpoints;
using Microsoft.AspNetCore.Http.Json;
using Microsoft.AspNetCore.HttpLogging;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration.Get<ConfigurationOptions>();

builder.Services
    .AddAuth(configuration?.JwtSettings)
    .AddCors()
    .AddOutputCache()
    .AddApplicationInsightsTelemetry()
    .AddDbContext<AquiferDbContext>(options =>
        options.UseSqlServer(configuration?.ConnectionStrings.BiblioNexusDb,
            providerOptions => providerOptions.EnableRetryOnFailure(3)))
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
    .AddScoped<IAdminResourceHistoryService, AdminResourceHistoryService>()
    .AddSingleton<IAzureKeyVaultClient, AzureKeyVaultClient>()
    .AddSingleton<ITrackResourceContentRequestService, TrackResourceContentRequestService>()
    .AddAzureClient(builder.Environment.IsDevelopment())
    .AddFastEndpoints()
    .AddHealthChecks()
    .AddDbContextCheck<AquiferDbContext>();

builder.Services.AddOptions<ConfigurationOptions>().Bind(builder.Configuration);

var app = builder.Build();

app.UseCors(b => b.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
app.UseHealthChecks("/_health");
app.UseAuth();
app.UseMiddleware<TrackResourceContentRequestMiddleware>();
app.UseOutputCache();
if (app.Environment.IsDevelopment())
{
    app.UseHttpLogging();
}

app.UseFastEndpoints(config => config.Serializer.Options.PropertyNamingPolicy = JsonNamingPolicy.CamelCase);

app.MapEndpoints();
app.Run();