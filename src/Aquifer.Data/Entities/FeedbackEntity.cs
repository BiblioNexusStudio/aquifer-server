namespace Aquifer.Data.Entities;

public class FeedbackEntity
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string? Email { get; set; }
    public string? Phone { get; set; }
    public string Feedback { get; set; } = null!;
    public DateTime Created { get; set; } = DateTime.UtcNow;
}