using Aquifer.Common.Clients;
using Aquifer.Common.Clients.Http.IpAddressLookup;
using Aquifer.Common.Jobs;
using Aquifer.Common.Services;
using Aquifer.Data;
using Aquifer.Jobs.Clients;
using Aquifer.Jobs.Configuration;
using Aquifer.Jobs.Services;
using Microsoft.Azure.Functions.Worker;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

var host = new HostBuilder()
    .ConfigureAppConfiguration((context, builder) => builder
        .SetBasePath(context.HostingEnvironment.ContentRootPath)
        .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
        .AddJsonFile(
            $"appsettings.{context.HostingEnvironment.EnvironmentName}.json", 
            optional: true, 
            reloadOnChange: true)
    )
    .ConfigureFunctionsWorkerDefaults()
    .ConfigureServices((context, services) =>
    {
        // This requires setting env var name AZURE_FUNCTIONS_ENVIRONMENT in Dev/QA and Prod
        var isDevelopment = context.HostingEnvironment.EnvironmentName == "Development";

        services.AddOptions<ConfigurationOptions>().Bind(context.Configuration);

        var configuration = context.Configuration.Get<ConfigurationOptions>()
            ?? throw new InvalidOperationException($"Unable to bind {nameof(ConfigurationOptions)}.");

        services.AddDbContext<AquiferDbContext>(options => options
            .UseSqlServer(configuration.ConnectionStrings.BiblioNexusDb, providerOptions => providerOptions.EnableRetryOnFailure(3))
            .EnableSensitiveDataLogging(sensitiveDataLoggingEnabled: isDevelopment)
            .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking));

        services.AddApplicationInsightsTelemetryWorkerService();
        services.ConfigureFunctionsApplicationInsights();
        services.AddAzureClient(isDevelopment);

        services.AddQueueServices(configuration.ConnectionStrings.AzureStorageAccount);

        services.AddSingleton<IAquiferAppInsightsClient, AquiferAppInsightsClient>();
        services.AddSingleton<IAquiferApiManagementClient, AquiferApiManagementClient>();
        services.AddHttpClient<IIpAddressLookupHttpClient, IpAddressLookupHttpClient>();
        services.AddSingleton<IAzureKeyVaultClient, AzureKeyVaultClient>();
        services.AddSingleton<IEmailService, EmailService>();
        services.AddKeyedSingleton<IEmailService, SendGridEmailService>(nameof(SendGridEmailService));
    })
    .Build();

StaticLoggerFactory.LoggerFactory = host.Services.GetRequiredService<ILoggerFactory>();

host.Run();