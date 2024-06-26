namespace Aquifer.API.Endpoints.Users.AssignedWordCount.List;

public class Response
{
    public required int UserId { get; set; }
    public required string UserName { get; set; }
    public required int AssignedSourceWordCount { get; set; }
}