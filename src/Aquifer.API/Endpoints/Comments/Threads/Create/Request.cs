namespace Aquifer.API.Endpoints.Comments.Threads.Create;

public class Request
{
    public int TypeId { get; set; }
    public CommentThreadType ThreadType { get; set; }
    public string Comment { get; set; } = null!;
}

public enum CommentThreadType
{
    ResourceContentVersion = 1
}