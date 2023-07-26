namespace Aquifer.API.Data.Entities;

public class PassageEntity
{
    public int Id { get; set; }
    public int StartBnVerse { get; set; }
    public int EndBnVerse { get; set; }
    [SqlDefaultValue("getutcdate()")]
    public DateTime Created { get; set; }
    [SqlDefaultValue("getutcdate()")]
    public DateTime Updated { get; set; }

    public ICollection<PassageResourceEntity> PassageResources { get; set; } = new List<PassageResourceEntity>();
}
