using Aquifer.API.Configuration;
using Aquifer.API.Modules;
using Aquifer.API.Services;
using Aquifer.Data;
using Microsoft.AspNetCore.Http.Json;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using Microsoft.Extensions.Diagnostics.HealthChecks;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration.Get<ConfigurationOptions>();

builder.Services
    .AddAuth(configuration?.JwtSettings)
    .AddSwagger()
    .AddCors()
    .AddOutputCache()
    .AddApplicationInsightsTelemetry()
    .AddDbContext<AquiferDbContext>(options =>
        options.UseSqlServer(configuration?.ConnectionStrings?.BiblioNexusDb))
    .RegisterModules()
    .Configure<JsonOptions>(options =>
    {
        options.SerializerOptions.Converters.Add(new JsonStringEnumConverter());
    })
    .AddHealthChecks()
    .AddDbContextCheck<AquiferDbContext>(nameof(AquiferDbContext), HealthStatus.Unhealthy);
var app = builder.Build();

app.UseAuth();
app.UseSwaggerWithUi();
app.UseCors(b => b.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod()); // need to expand on this
app.UseOutputCache();
app.UseHealthChecks("/_health");
app.MapEndpoints();
app.Run();