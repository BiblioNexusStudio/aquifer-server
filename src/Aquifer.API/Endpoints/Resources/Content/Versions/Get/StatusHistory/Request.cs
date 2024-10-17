namespace Aquifer.API.Endpoints.Resources.Content.Versions.Get.StatusHistory;

public record Request
{
    public int VersionId { get; set; }
}