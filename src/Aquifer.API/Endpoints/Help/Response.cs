using Aquifer.Data.Entities;

namespace Aquifer.API.Endpoints.Help;

public class Response
{
    public required List<HelpDocumentEntity> Releases { get; set; } = [];
    public required List<HelpDocumentEntity> HowTos { get; set; } = [];
}