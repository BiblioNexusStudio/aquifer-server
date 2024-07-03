namespace Aquifer.API.Endpoints.Resources.Content.SendForManagerReview;

public class UserAssignedToContent
{
    public int ResourceContentId { get; set; }
    public int AssignedUserId { get; set; }
}

public class Response
{
    public List<UserAssignedToContent> Assignments { get; set; } = new();
}