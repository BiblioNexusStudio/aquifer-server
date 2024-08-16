namespace Aquifer.API.Endpoints.Marketing.Subscribers.Choices;

public class Response
{
    public List<SubscriberChoice> LanguageChoices { get; set; } = [];
    public List<SubscriberChoice> ParentResourceChoices { get; set; } = [];
}

public class SubscriberChoice
{
    public int Id { get; set; }
    public string EnglishDisplayName { get; set; } = null!;
}