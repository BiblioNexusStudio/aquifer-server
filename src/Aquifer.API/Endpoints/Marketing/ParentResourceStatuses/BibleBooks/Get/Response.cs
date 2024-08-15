namespace Aquifer.API.Endpoints.Marketing.ParentResourceStatuses.BibleBooks.Get;

public class Response
{
    public required string Book { get; set; }
    public required ParentResourceStatus Status { get; set; }
}