namespace Aquifer.API.Endpoints.Comments.Update;

public class Request
{
    public int CommentId { get; set; }
    public string Comment { get; set; } = null!;
}