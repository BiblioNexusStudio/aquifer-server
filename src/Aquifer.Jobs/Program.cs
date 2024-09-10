using Aquifer.Common.Clients;
using Aquifer.Common.Services;
using Aquifer.Data;
using Aquifer.Jobs.Clients;
using Aquifer.Jobs.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var host = new HostBuilder().ConfigureFunctionsWorkerDefaults()
    .ConfigureServices((context, services) =>
    {
        services.AddOptions<ConfigurationOptions>().Bind(context.Configuration);

        var connectionString = context.Configuration.GetConnectionString("BiblioNexusDb");

        services.AddSingleton<IAquiferAppInsightsClient, AquiferAppInsightsClient>();
        services.AddSingleton<IAzureClientService, AzureClientService>();
        services.AddSingleton<IAquiferApiManagementClient, AquiferApiManagementClient>();
        services.AddSingleton<ISendGridClient, SendGridClient>();
        services.AddSingleton<IAzureKeyVaultClient, AzureKeyVaultClient>();
        services.AddAzureClient(context.Configuration.Get<ConfigurationOptions>()!.IsDevelopment);
        services.AddDbContext<AquiferDbContext>(options =>
            options.UseSqlServer(connectionString, providerOptions => providerOptions.EnableRetryOnFailure(3)));
    })
    .Build();

host.Run();