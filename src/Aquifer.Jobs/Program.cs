using Aquifer.AI;
using Aquifer.Common.Clients;
using Aquifer.Common.Clients.Http.IpAddressLookup;
using Aquifer.Common.Messages;
using Aquifer.Common.Messages.Publishers;
using Aquifer.Common.Services;
using Aquifer.Data;
using Aquifer.Data.Services;
using Aquifer.Jobs.Clients;
using Aquifer.Jobs.Configuration;
using Aquifer.Jobs.Services;
using Microsoft.Azure.Functions.Worker;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Polly;
using Polly.Contrib.WaitAndRetry;
using Polly.Extensions.Http;

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
        services.AddSingleton(cfg => cfg.GetService<IOptions<ConfigurationOptions>>()!.Value.OpenAi);
        services.AddSingleton(cfg => cfg.GetService<IOptions<ConfigurationOptions>>()!.Value.OpenAiTranslation);

        var configuration = context.Configuration.Get<ConfigurationOptions>()
                            ?? throw new InvalidOperationException($"Unable to bind {nameof(ConfigurationOptions)}.");

        services.AddDbContext<AquiferDbContext>(options => options
            .UseAzureSql(configuration.ConnectionStrings.BiblioNexusDb, providerOptions => providerOptions.EnableRetryOnFailure(3))
            .EnableSensitiveDataLogging(sensitiveDataLoggingEnabled: isDevelopment)
            .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking));

        services.AddApplicationInsightsTelemetryWorkerService();
        services.ConfigureFunctionsApplicationInsights();
        services.AddAzureClient(isDevelopment);

        services.AddQueueServices(configuration.ConnectionStrings.AzureStorageAccount);

        services.AddSingleton<IAquiferAppInsightsClient, AquiferAppInsightsClient>();
        services.AddSingleton<IAquiferApiManagementClient, AquiferApiManagementClient>();
        services.AddHttpClient<IIpAddressLookupHttpClient, IpAddressLookupHttpClient>()
            .AddPolicyHandler(HttpPolicyExtensions
                .HandleTransientHttpError()
                .WaitAndRetryAsync(Backoff.DecorrelatedJitterBackoffV2(TimeSpan.FromSeconds(0.5), 2)));
        services.AddSingleton<IAzureKeyVaultClient, AzureKeyVaultClient>();
        services.AddScoped<IResourceHistoryService, ResourceHistoryService>();
        services.AddSingleton<IEmailMessagePublisher, EmailMessagePublisher>();
        services.AddSingleton<INotificationMessagePublisher, NotificationMessagePublisher>();
        services.AddSingleton<ITranslationService, OpenAiTranslationService>();
        services.AddSingleton<IEmailService, SendGridEmailService>();
    })
    .Build();

StaticLoggerFactory.LoggerFactory = host.Services.GetRequiredService<ILoggerFactory>();

host.Run();