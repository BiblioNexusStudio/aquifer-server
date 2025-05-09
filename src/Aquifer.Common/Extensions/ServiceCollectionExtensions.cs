using System.Threading.Channels;
using Aquifer.Common.Messages.Models;
using Aquifer.Common.Messages.Publishers;
using Aquifer.Common.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Aquifer.Common.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddTrackResourceContentRequestServices(this IServiceCollection services)
    {
        return services
            .AddScoped<IResourceContentRequestTrackingMessagePublisher, ResourceContentRequestTrackingMessagePublisher>()
            .AddHostedService<TrackResourceContentRequestBackgroundService>()
            .AddSingleton(_ => Channel.CreateUnbounded<TrackResourceContentRequestMessage>(
                new UnboundedChannelOptions
                {
                    AllowSynchronousContinuations = false,
                    SingleReader = false,
                    SingleWriter = false,
                }));
    }
}