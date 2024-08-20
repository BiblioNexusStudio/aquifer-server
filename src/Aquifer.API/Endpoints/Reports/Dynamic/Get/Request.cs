namespace Aquifer.API.Endpoints.Reports.Dynamic.Get;

public record Request
{
    public required string Slug { get; set; }
    public DateOnly? StartDate { get; init; }
    public DateOnly? EndDate { get; init; }
    public int LanguageId { get; set; }
    public int ParentResourceId { get; set; }
}