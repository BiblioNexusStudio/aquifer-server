namespace Aquifer.API.Endpoints.Reports.Resources.EditedLastThirtyDays;

public record Response
{
    public required string Resource { get; set; }
    public required string Label { get; set; }
    public required string Language { get; set; }
}