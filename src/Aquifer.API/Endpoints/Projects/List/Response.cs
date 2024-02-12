namespace Aquifer.API.Endpoints.Projects.List;

public class Response
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public required string Language { get; set; }
    public required string ProjectLead { get; set; }
    public required string Company { get; set; }
    public required string ProjectPlatform { get; set; }
    public int? Days { get; set; }

    public ProjectResourceStatusCounts Counts { get; set; } = null!;
}

public class ProjectResourceStatusCounts
{
    public int InProgress { get; set; }
    public int InReview { get; set; }
    public int Completed { get; set; }
}