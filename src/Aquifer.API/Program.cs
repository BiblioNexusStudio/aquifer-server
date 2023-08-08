using Aquifer.API.Data;
using Aquifer.API.Modules;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

builder.Services
    .AddApplicationInsightsTelemetry()
    .AddDbContext<AquiferDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("BiblioNexus")))
    .RegisterModules();

var app = builder.Build();

app.MapEndpoints();
app.Run();
