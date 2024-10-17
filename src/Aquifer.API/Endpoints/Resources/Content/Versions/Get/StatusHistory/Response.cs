namespace Aquifer.API.Endpoints.Resources.Content.Versions.Get.StatusHistory;

public record Response
{
    public required string Event { get; set; }
    public required DateTime DateOfEvent { get; set; }
}