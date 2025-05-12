// using Aquifer.Well.API.Endpoints.PushNotifications.Models;

namespace Aquifer.Well.API.Endpoints.PushNotifications.Installations.Create;

public sealed class Request
{
    public required string InstallationId { get; set; }
    
    public required string Platform { get; set; }
    
    public required string PushChannel { get; set; }

    public IList<string> Tags { get; set; } = Array.Empty<string>();
}