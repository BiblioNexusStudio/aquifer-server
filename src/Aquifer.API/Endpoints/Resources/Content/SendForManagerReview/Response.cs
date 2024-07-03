namespace Aquifer.API.Endpoints.Resources.Content.SendForManagerReview;

public class Response
{
    public List<UserAssignment> Assignments { get; set; } = [];
}

public class UserAssignment
{
    public int ResourceContentId { get; set; }
    public int? AssignedUserId { get; set; }
}