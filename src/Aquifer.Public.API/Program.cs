using System.Text.Json;
using Aquifer.Data;
using Aquifer.Public.API.OpenApi;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AquiferDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("BiblioNexusDb"),
        providerOptions => providerOptions.EnableRetryOnFailure(3)));

builder.Services.AddFastEndpoints()
    .AddSwaggerDocumentSettings()
    .AddOutputCache()
    .AddHealthChecks()
    .AddDbContextCheck<AquiferDbContext>();

var app = builder.Build();
app.UseHealthChecks("/_health")
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