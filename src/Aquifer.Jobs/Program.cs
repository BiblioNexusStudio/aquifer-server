using Aquifer.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var host = new HostBuilder()
    .ConfigureFunctionsWorkerDefaults()
    .ConfigureServices((context, services) =>
    {
        string? connectionString = context.Configuration.GetConnectionString("BiblioNexusDb");
        services.AddDbContext<AquiferDbContext>(options =>
            options.UseSqlServer(connectionString,
                providerOptions => providerOptions.EnableRetryOnFailure(3)));
    })
    .Build();

host.Run();