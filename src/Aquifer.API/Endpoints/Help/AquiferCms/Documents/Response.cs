using Aquifer.Data.Entities;

namespace Aquifer.API.Endpoints.Help.AquiferCms.Documents;
public class Response
{
    public required IEnumerable<HelpDocumentResponse> Releases { get; init; }
    public required IEnumerable<HelpDocumentResponse> HowTos { get; init; }
}

public class HelpDocumentResponse
{
    public required string Title { get; set; }
    public required HelpDocumentType Type { get; set; }
    public required string Url { get; set; }
    public string? ThumbnailUrl { get; set; }
}