using Aquifer.API.Configuration;
using Aquifer.API.Modules;
using Aquifer.API.Services;
using Aquifer.Data;
using Microsoft.AspNetCore.Http.Json;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration.Get<ConfigurationOptions>();

builder.Services
    .AddAuth(configuration?.JwtSettings)
    .AddSwagger()
    .AddCors()
    .AddOutputCache()
    .AddApplicationInsightsTelemetry()
    .AddDbContext<AquiferDbContext>(options =>
        options.UseSqlServer(configuration?.ConnectionStrings?.BiblioNexusDb,
            providerOptions => providerOptions.EnableRetryOnFailure(3)))
    .RegisterModules()
    .Configure<JsonOptions>(options =>
    {
        options.SerializerOptions.Converters.Add(new JsonStringEnumConverter());
    })
    .AddScoped<IUserService, UserService>()
    .AddHealthChecks()
    .AddDbContextCheck<AquiferDbContext>();

var app = builder.Build();

app.UseCors(b => b.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod()); // need to expand on this
app.UseAuth();
app.UseSwaggerWithUi();
app.UseOutputCache();
app.UseHealthChecks("/_health");
app.MapEndpoints();
app.Run();