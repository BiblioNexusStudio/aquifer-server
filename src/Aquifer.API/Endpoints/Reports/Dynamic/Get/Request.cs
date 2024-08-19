namespace Aquifer.API.Endpoints.Reports.Dynamic.Get;

public record Request
{
    public required string Slug { get; set; }
    public DateOnly StartDate { get; init; } = DateOnly.FromDateTime(DateTime.UtcNow.AddMonths(-3));
    public DateOnly EndDate { get; init; } = DateOnly.FromDateTime(DateTime.UtcNow);
    public int LanguageId { get; set; }
    public int ParentResourceId { get; set; }
}