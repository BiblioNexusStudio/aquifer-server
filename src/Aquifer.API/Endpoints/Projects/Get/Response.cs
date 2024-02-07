namespace Aquifer.API.Endpoints.Projects.Get;

public class Response
{
    public string Name { get; set; } = null!;
    public required string Language { get; set; }
    public required string ProjectManager { get; set; }
    public required string Company { get; set; }
    public required string ProjectPlatform { get; set; }
    public string? CompanyLead { get; set; }
    public int SourceWordCount { get; set; }
    public int? EffectiveWordCount { get; set; }
    public decimal? QuotedCost { get; set; }
    public DateTime? Started { get; set; }
    public DateOnly? ProjectedDeliveryDate { get; set; }
    public DateOnly? ActualDeliveryDate { get; set; }
    public DateOnly? ProjectedPublishDate { get; set; }
    public DateOnly? ActualPublishDate { get; set; }

    public ProjectResourceStatusCounts Counts { get; set; } = null!;
}

public class ProjectResourceStatusCounts
{
    public int InProgress { get; set; }
    public int InReview { get; set; }
    public int Completed { get; set; }
}