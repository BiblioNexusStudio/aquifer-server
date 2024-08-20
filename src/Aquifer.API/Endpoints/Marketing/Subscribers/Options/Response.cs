namespace Aquifer.API.Endpoints.Marketing.Subscribers.Options;

public class Response
{
    public List<SubscriberOption> LanguageOptions { get; set; } = [];
    public List<SubscriberOption> ParentResourceOptions { get; set; } = [];
}

public class SubscriberOption
{
    public int Id { get; set; }
    public string EnglishDisplayName { get; set; } = null!;
}