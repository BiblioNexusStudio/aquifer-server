namespace Aquifer.API.Endpoints.FeedBack.BibleWell;

public record Request
{
    public string Name { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public string Feedback { get; set; }
}