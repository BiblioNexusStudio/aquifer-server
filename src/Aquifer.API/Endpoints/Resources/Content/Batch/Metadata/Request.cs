namespace Aquifer.API.Endpoints.Resources.Content.Batch.Metadata;

public record Request
{
    public int[] Ids { get; set; } = null!;
}