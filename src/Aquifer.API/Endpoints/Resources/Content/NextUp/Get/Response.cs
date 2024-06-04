namespace Aquifer.API.Endpoints.Resources.Content.NextUp.Get;

public record Response
{
    public required int? NextUpResourceContentId { get; set; }
}