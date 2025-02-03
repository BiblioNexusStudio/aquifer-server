namespace Aquifer.API.Endpoints.Resources.Content.AssignedToSelf.List;

public record Request
{
    public bool? HasAudio { get; set; }
    public bool? HasUnresolvedCommentThreads { get; set; }
}