using FastEndpoints.Testing;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Aquifer.Public.API.IntegrationTests;

/// <summary>
/// This is a FastEndpoints <see cref="App"/> that sits on top of a <see cref="WebApplicationFactory{TEntryPoint}"/>
/// which allows for in-memory web requests to be sent and received without actual network traffic.
/// Details:
/// * https://fast-endpoints.com/docs/integration-unit-testing
/// * https://learn.microsoft.com/en-us/aspnet/core/test/integration-tests
/// </summary>
public sealed class App : AppFixture<Program>
{
    /// <summary>
    /// Configure Clients here.
    /// The default Client is anonymous and has no API Key header value.
    /// This is sufficient for Public API tests because all routes are anonymous and no actual web requests are sent via this fixture.
    /// The API key is enforced by Azure API Management when proxying requests, and thus it is not needed locally.
    /// </summary>
    /// <remarks>
    /// Example of adding a default header to the existing client:
    /// <example>
    /// <code>
    /// Client.DefaultRequestHeaders.Add("api-key", "TODO");
    /// </code>
    /// </example>
    /// An entirely new client could be defined in this class and set up in this method as well (e.g. an authenticated client).
    /// </remarks>
    protected override Task SetupAsync()
    {
        return Task.CompletedTask;
    }

    /// <summary>
    /// The app is configured in <see cref="Program"/> before this method is called.
    /// Only use this method to override or extend existing host configuration.
    /// </summary>
    protected override void ConfigureApp(IWebHostBuilder builder)
    {
        // The environment must be explicitly set as it will not default to "Development".
        // On the build server this environment variable should be explicitly populated.
        if (string.IsNullOrEmpty(Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")))
        {
            builder.UseEnvironment(Environments.Development);
        }
    }

    /// <summary>
    /// The service registrations in <see cref="Program"/> run before this method is called.
    /// Only use this method to add additional services or to override existing registrations from the API.
    /// </summary>
    protected override void ConfigureServices(IServiceCollection services)
    {
    }

    /// <summary>
    /// Use this method to dispose of any <see cref="HttpClient"/>s created in <see cref="SetupAsync"/>.
    /// </summary>
    protected override Task TearDownAsync()
    {
        return Task.CompletedTask;
    }
}