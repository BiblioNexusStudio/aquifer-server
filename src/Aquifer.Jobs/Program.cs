using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Aquifer.Data;

var host = new HostBuilder()
    .ConfigureFunctionsWorkerDefaults()
    .ConfigureServices((context, services) =>
    {
        var connectionString = context.Configuration.GetConnectionString("BiblioNexusDb");
        services.AddDbContext<AquiferDbContext>(options =>
            options.UseSqlServer(connectionString,
                providerOptions => providerOptions.EnableRetryOnFailure(3)));
    })
    .Build();

host.Run();
