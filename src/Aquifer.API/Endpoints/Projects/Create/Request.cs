namespace Aquifer.API.Endpoints.Projects.Create;

public record Request
{
    public required string Title { get; set; }
    public required int LanguageId { get; set; }
    public required int ProjectManagerUserId { get; set; }
    public required int CompanyId { get; set; }
    public required int ProjectPlatformId { get; set; }
    public required int? CompanyLeadUserId { get; set; }
    public required int[] ResourceIds { get; set; }
}