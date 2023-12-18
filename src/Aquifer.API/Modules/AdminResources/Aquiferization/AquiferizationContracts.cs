namespace Aquifer.API.Modules.AdminResources.Aquiferization;

public class AquiferizationRequest
{
    public int? AssignedUserId { get; set; }
}

public class PublishRequest
{
    public bool CreateDraft { get; set; } = false;
    public int? AssignedUserId { get; set; }
}