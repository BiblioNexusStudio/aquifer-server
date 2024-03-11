namespace Aquifer.API.Endpoints.Resources.Content.Aquiferize;

public record Request
{
    public int ContentId { get; set; }
    public int? AssignedUserId { get; set; }
}