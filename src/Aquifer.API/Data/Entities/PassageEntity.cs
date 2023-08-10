﻿namespace Aquifer.API.Data.Entities;

public class PassageEntity
{
    public int Id { get; set; }
    public int StartVerseId { get; set; }
    public int EndVerseId { get; set; }

    [SqlDefaultValue("getutcdate()")]
    public DateTime Created { get; set; }

    [SqlDefaultValue("getutcdate()")]
    public DateTime Updated { get; set; }

    public ICollection<PassageResourceEntity> PassageResources { get; set; } = new List<PassageResourceEntity>();
}