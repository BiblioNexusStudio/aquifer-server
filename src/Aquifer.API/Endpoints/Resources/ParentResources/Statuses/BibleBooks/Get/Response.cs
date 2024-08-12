namespace Aquifer.API.Endpoints.Resources.ParentResources.Statuses.BibleBooks.Get;

public class Response
{
    public required string Book { get; set; }
    public required ParentResourceStatus Status { get; set; }
}