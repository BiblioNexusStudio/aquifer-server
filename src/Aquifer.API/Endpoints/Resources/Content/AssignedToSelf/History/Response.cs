namespace Aquifer.API.Endpoints.Resources.Content.AssignedToSelf.History;

public class Response
{
    public required int Id { get; set; }
    public required string EnglishLabel { get; set; }
    public required string ParentResourceName { get; set; }
    public required DateTime LastActionTime { get; set; }
    public required int? SourceWords { get; set; }
    public required int SortOrder { get; set; }
}