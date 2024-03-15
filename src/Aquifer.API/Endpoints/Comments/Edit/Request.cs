namespace Aquifer.API.Endpoints.Comments.Edit;

public class Request
{
    public int CommentId { get; set; }
    public string Comment { get; set; } = null!;
}