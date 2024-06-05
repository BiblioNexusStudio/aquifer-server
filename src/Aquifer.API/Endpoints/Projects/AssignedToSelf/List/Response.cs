namespace Aquifer.API.Endpoints.Projects.AssignedToSelf.List;

public class Response
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public required string Language { get; set; }
    public required string Company { get; set; }
    public required string ProjectPlatform { get; set; }
    public required int? Days { get; set; }
    public required bool IsStarted { get; set; }

    public required ProjectResourceStatusCounts Counts { get; set; }
}