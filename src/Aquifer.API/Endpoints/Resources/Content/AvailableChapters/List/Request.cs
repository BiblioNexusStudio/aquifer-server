namespace Aquifer.API.Endpoints.Resources.Content.AvailableChapters.List;

public record Request
{
    public int LanguageId { get; set; }
    public int ParentResourceId { get; set; }
}