using Aquifer.Data.Entities;

namespace Aquifer.API.Endpoints.Help;

public class Response
{
    public required List<HelpDocumentResponse> Releases { get; set; } = [];
    public required List<HelpDocumentResponse> HowTos { get; set; } = [];
}

public class HelpDocumentResponse
{
    public required int Id { get; set; }
    public required string Title { get; set; }
    public required HelpDocumentType Type { get; set; }
    public required string Url { get; set; }
}