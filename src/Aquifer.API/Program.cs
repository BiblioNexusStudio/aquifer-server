using Aquifer.API.Configuration;
using Aquifer.API.Modules;
using Aquifer.API.Services;
using Aquifer.Data;
using Microsoft.AspNetCore.Http.Json;
using Microsoft.AspNetCore.HttpLogging;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

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
    .Configure<JsonOptions>(options =>
    {
        options.SerializerOptions.Converters.Add(new JsonStringEnumConverter());
    })
    .AddHttpLogging(logging =>
    {
        logging.LoggingFields = HttpLoggingFields.Response;
        logging.RequestBodyLogLimit = 4096;
        logging.ResponseBodyLogLimit = 4096;
    })
    .AddScoped<IUserService, UserService>()
    .AddScoped<IAzureKeyVaultService, AzureKeyVaultService>()
    .AddScoped<IResourceContentRequestService, ResourceContentRequestService>()
    .AddSingleton<ResourceContentRequestBackgroundService>()
    .AddHostedService<ResourceContentRequestBackgroundService>(sp =>
        sp.GetService<ResourceContentRequestBackgroundService>()
            ?? throw new InvalidOperationException("ResourceContentRequestBackgroundService not available"))
    .AddHealthChecks()
    .AddDbContextCheck<AquiferDbContext>();

builder.Services.AddOptions<ConfigurationOptions>().Bind(builder.Configuration);

var app = builder.Build();

app.UseCors(b => b.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
app.UseHealthChecks("/_health");
app.UseAuth();
app.UseOutputCache();
if (app.Environment.IsDevelopment())
{
    app.UseHttpLogging();
}

app.MapEndpoints();
app.Run();