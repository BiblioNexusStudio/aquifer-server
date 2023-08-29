using Aquifer.API.Data;
using Aquifer.API.Modules;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.Authority = configuration["JwtSettings:Issuer"];
        options.Audience = configuration["JwtSettings:Audience"];
        options.TokenValidationParameters = new TokenValidationParameters { NameClaimType = ClaimTypes.NameIdentifier };
    });
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("write",
        p => p.RequireClaim("permissions", "write:values"));
});

builder.Services
    .AddApplicationInsightsTelemetry()
    .AddDbContext<AquiferDbContext>(options =>
        options.UseSqlServer(configuration.GetConnectionString("BiblioNexus")))
    .RegisterModules();

var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();

app.MapEndpoints();
app.Run();