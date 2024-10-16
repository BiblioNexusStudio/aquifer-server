namespace Aquifer.Public.API.Endpoints.Resources.Get.Associations;

public class Response
{
    public List<PassageAssociation> PassageAssociations { get; set; }
    public List<ResourceItemAssociation> ResourceItemAssociations { get; set; }
}

public class PassageAssociation
{
    public string BookCode { get; set; }
    public int StartChapter { get; set; }
    public int StartVerse { get; set; }
    public int EndChapter { get; set; }
    public int EndVerse { get; set; }
}

public class ResourceItemAssociation
{
    public int ContentId { get; set; }
    public string DisplayName { get; set; }
}