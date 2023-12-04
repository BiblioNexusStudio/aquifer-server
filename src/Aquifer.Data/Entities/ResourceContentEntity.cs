﻿using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;

namespace Aquifer.Data.Entities;

[Index(nameof(ResourceId),
    nameof(LanguageId),
    nameof(MediaType),
    IsUnique = true)]
public class ResourceContentEntity
{
    public int Id { get; set; }
    public int ResourceId { get; set; }
    public int LanguageId { get; set; }
    public ResourceContentStatus Status { get; set; }
    public bool Trusted { get; set; }
    public ResourceContentMediaType MediaType { get; set; }

    public ICollection<ResourceContentVersionEntity> Versions { get; set; } =
        new List<ResourceContentVersionEntity>();

    [SqlDefaultValue("getutcdate()")]
    public DateTime Created { get; set; } = DateTime.UtcNow;

    [SqlDefaultValue("getutcdate()")]
    public DateTime Updated { get; set; } = DateTime.UtcNow;

    public LanguageEntity Language { get; set; } = null!;
    public ResourceEntity Resource { get; set; } = null!;
}

[JsonConverter(typeof(JsonConverter))]
public enum ResourceContentMediaType
{
    None = 0,
    Text = 1,
    Audio = 2,
    Video = 3,
    Image = 4
}

public enum ResourceContentStatus
{
    None = 0,

    [Display(Name = "Aquiferize - Not Started")]
    AquiferizeNotStarted = 1,

    [Display(Name = "Aquiferize - In Progress")]
    AquiferizeInProgress = 2,

    [Display(Name = "Complete")] Complete = 3,

    [Display(Name = "Aquiferize - In Review")]
    AquiferizeInReview = 4,

    [Display(Name = "Translate - Not Started")]
    TranslateNotStarted = 5,

    [Display(Name = "Translate - Drafting")]
    TranslateDrafting = 6,

    [Display(Name = "Translate - Editing")]
    TranslateEditing = 7,

    [Display(Name = "Translate - Reviewing")]
    TranslateReviewing = 8,

    [Display(Name = "On Hold")] OnHold = 9
}