using Aquifer.API.Configuration;
using Aquifer.API.Data;
using Aquifer.API.Modules;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration.Get<ConfigurationOptions>();

builder.Services
    //.AddAuth(configuration)
    .AddApplicationInsightsTelemetry()
    .AddDbContext<AquiferDbContext>(options =>
        options.UseSqlServer(configuration?.ConnectionStrings?.BiblioNexusDb))
    .RegisterModules();

var app = builder.Build();

//app.UseAuth();
app.MapEndpoints();
app.Run();