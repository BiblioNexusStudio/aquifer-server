using System.ComponentModel.DataAnnotations;

namespace Aquifer.API.Endpoints.Resources.Content.Create;

public class Request
{
    [Required]
    public int LanguageId { get; set; }

    [Required]
    public int ParentResourceId { get; set; }

    [Required]
    public string EnglishLabel { get; set; } = null!;

    [Required]
    public string LanguageTitle { get; set; } = null!;
}