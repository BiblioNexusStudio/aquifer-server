﻿using Aquifer.Data.EventHandlers;
using Microsoft.EntityFrameworkCore;

namespace Aquifer.Data.Entities;

[Index(
    nameof(GreekNewTestamentId),
    nameof(WordIdentifier),
    IsUnique = true)]
public class GreekNewTestamentWordEntity : IHasUpdatedTimestamp
{
    public int Id { get; set; }
    public int GreekNewTestamentId { get; set; }
    public int GreekWordId { get; set; }
    public long WordIdentifier { get; set; }

    [SqlDefaultValue("getutcdate()")]
    public DateTime Created { get; set; } = DateTime.UtcNow;

    public GreekWordEntity GreekWord { get; set; } = null!;
    public GreekNewTestamentEntity GreekNewTestament { get; set; } = null!;

    public ICollection<GreekNewTestamentWordGroupWordEntity> GreekNewTestamentWordGroupWords { get; set; } = [];

    public ICollection<GreekNewTestamentWordSenseEntity> GreekNewTestamentWordSenses { get; set; } = [];

    [SqlDefaultValue("getutcdate()")]
    public DateTime Updated { get; set; } = DateTime.UtcNow;
}