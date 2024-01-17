using System.Text.Json;
using Aquifer.Data;
using FastEndpoints;
using FastEndpoints.Swagger;
using Microsoft.EntityFrameworkCore;
using NSwag;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AquiferDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("BiblioNexusDb"),
        providerOptions => providerOptions.EnableRetryOnFailure(3)));

builder.Services.AddFastEndpoints()
    .SwaggerDocument(sd =>
    {
        sd.ShortSchemaNames = true;
        sd.EnableJWTBearerAuth = false;
        sd.DocumentSettings = ds =>
        {
            ds.Title = "Aquifer API";
            ds.AddAuth("ApiKey",
                new OpenApiSecurityScheme
                {
                    Name = "api-key",
                    In = OpenApiSecurityApiKeyLocation.Header,
                    Type = OpenApiSecuritySchemeType.ApiKey
                });
        };
    })
    .AddOutputCache();

var app = builder.Build();
app.UseResponseCaching()
    .UseOutputCache()
    .UseFastEndpoints(config =>
    {
        config.Serializer.Options.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
        config.Endpoints.Configurator = ep => { ep.AllowAnonymous(); };
    }).UseSwaggerGen();

app.Run();