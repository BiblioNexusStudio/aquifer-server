using System.ComponentModel.DataAnnotations.Schema;

namespace Aquifer.API.Data.Entities;

public class VerseEntity
{
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    public int Id { get; set; }

    public ICollection<VerseResourceEntity> VerseResources { get; set; } =
        new List<VerseResourceEntity>();
}