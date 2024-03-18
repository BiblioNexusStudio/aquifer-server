using Aquifer.API.Common;

namespace Aquifer.API.Endpoints.Comments.Threads.List;

public class Request
{
    public int TypeId { get; set; }
    public CommentThreadType ThreadType { get; set; }
}