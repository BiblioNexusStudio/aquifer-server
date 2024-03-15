using Aquifer.API.Common.Dtos;

namespace Aquifer.API.Endpoints.Comments.Threads.List;

public class Response
{
    public required int Id { get; set; }
    public required bool Resolved { get; set; }
    public required List<CommentResponse> Comments { get; set; } = [];
}

public class CommentResponse
{
    public required int Id { get; set; }
    public required UserDto User { get; set; }
    public required string Comment { get; set; }
}