﻿using Aquifer.API.Common;

namespace Aquifer.API.Endpoints.Comments.Threads.Create;

public class Request
{
    public int TypeId { get; set; }
    public CommentThreadType ThreadType { get; set; }
    public string Comment { get; set; } = null!;
}