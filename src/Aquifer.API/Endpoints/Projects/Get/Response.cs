namespace Aquifer.API.Endpoints.Projects.Get;

public class Response
{
    public required int Id { get; set; }
    public required string Name { get; set; } = null!;
    public required string Language { get; set; }
    public required string ProjectManager { get; set; }
    public required string Company { get; set; }
    public required string ProjectPlatform { get; set; }
    public required string? CompanyLead { get; set; }
    public required int SourceWordCount { get; set; }
    public required int? EffectiveWordCount { get; set; }
    public required decimal? QuotedCost { get; set; }
    public required DateTime? Started { get; set; }
    public required DateOnly? ProjectedDeliveryDate { get; set; }
    public required DateOnly? ActualDeliveryDate { get; set; }
    public required DateOnly? ProjectedPublishDate { get; set; }
    public required DateOnly? ActualPublishDate { get; set; }
    public required IEnumerable<ProjectResourceItem> Items { get; set; }

    public ProjectResourceStatusCounts Counts { get; set; } = null!;
}

public class ProjectResourceStatusCounts
{
    public required int InProgress { get; set; }
    public required int InReview { get; set; }
    public required int Completed { get; set; }
}

public class ProjectResourceItem
{
    public required int ResourceContentId { get; set; }
    public required string EnglishLabel { get; set; }
    public required string ParentResourceName { get; set; }
    public required string? AssignedUserName { get; set; }
    public required string StatusDisplayName { get; set; }
}