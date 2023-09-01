using Aquifer.API.Configuration;
using Aquifer.API.Data;
using Aquifer.API.Modules;
using Aquifer.API.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration.Get<ConfigurationOptions>();

builder.Services
    //.AddAuth(configuration)
    .AddSwagger()
    .AddApplicationInsightsTelemetry()
    .AddDbContext<AquiferDbContext>(options =>
        options.UseSqlServer(configuration?.ConnectionStrings?.BiblioNexusDb))
    .RegisterModules();

var app = builder.Build();

//app.UseAuth();
app.UseSwaggerWithUi();
app.MapEndpoints();
app.Run();