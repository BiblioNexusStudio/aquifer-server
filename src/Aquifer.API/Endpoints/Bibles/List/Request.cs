namespace Aquifer.API.Endpoints.Bibles.List;

public record Request
{
    public bool RestrictedLicense { get; set; }
}