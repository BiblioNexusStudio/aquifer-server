namespace Aquifer.API.Endpoints.Projects.Create;

public record Request
{
    public string Title { get; set; } = null!;
    public int SourceLanguageId { get; set; }
    public int? TargetLanguageId { get; set; }
    public int ProjectManagerUserId { get; set; }
    public int CompanyId { get; set; }
    public int CompanyLeadUserId { get; set; }
    public int[] ResourceIds { get; set; } = [];
    public bool IsAlreadyTranslated { get; set; }
}