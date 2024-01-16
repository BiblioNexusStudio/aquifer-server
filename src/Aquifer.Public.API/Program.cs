using System.Text.Json;
using Aquifer.Data;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AquiferDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("BiblioNexusDb"),
        providerOptions => providerOptions.EnableRetryOnFailure(3)));

builder.Services.AddFastEndpoints();

var app = builder.Build();
app.UseFastEndpoints(config =>
{
    config.Serializer.Options.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
    config.Endpoints.Configurator = ep => { ep.AllowAnonymous(); };
});
app.Run();