namespace Aquifer.API.Endpoints.Resources.Content.NextUp.Get;

public record Response
{
    public required int? NextUpForManager { get; set; }
    public required int? NextUpForEditor { get; set; }
}