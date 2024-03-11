namespace Aquifer.API.Endpoints.Resources.Content.Unpublish;

public record Request
{
    public int ContentId { get; set; }
}