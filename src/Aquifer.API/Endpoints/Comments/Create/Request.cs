namespace Aquifer.API.Endpoints.Comments.Create;

public class Request
{
    public int ThreadId { get; set; }
    public string Comment { get; set; } = null!;
}