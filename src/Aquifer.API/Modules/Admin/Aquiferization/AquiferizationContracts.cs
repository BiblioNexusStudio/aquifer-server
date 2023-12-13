namespace Aquifer.API.Modules.Admin;

public class AquiferizationRequest
{
    public int? AssignedUserId { get; set; }
}

public class PublishRequest
{
    public bool? CreateDraft { get; set; }
    public int? AssignedUserId { get; set; }
}