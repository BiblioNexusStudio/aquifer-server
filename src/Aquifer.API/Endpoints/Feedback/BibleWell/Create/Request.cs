namespace Aquifer.API.Endpoints.Feedback.BibleWell.Create;

public class Request
{
    public string Name { get; set; } = null!;
    public string? Email { get; set; }
    public string? Phone { get; set; }
    public string Feedback { get; set; } = null!;
}