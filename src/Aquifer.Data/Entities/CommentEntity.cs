﻿using Aquifer.Data.EventHandlers;

namespace Aquifer.Data.Entities;

public class CommentEntity : IHasUpdatedTimestamp
{
    public int Id { get; set; }
    public int ThreadId { get; set; }
    public int UserId { get; set; }
    public string Comment { get; set; } = null!;

    [SqlDefaultValue("getutcdate()")]
    public DateTime Created { get; set; }

    public CommentThreadEntity Thread { get; set; } = null!;

    [SqlDefaultValue("getutcdate()")]
    public DateTime Updated { get; set; }
}