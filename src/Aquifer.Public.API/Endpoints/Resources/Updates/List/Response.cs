namespace Aquifer.Public.API.Endpoints.Resources.Updates.List;

public class Response
{
    public int ReturnedItemCount => Items.Count;
    public List<ResponseContent> Items { get; set; } = [];
}

public class ResponseContent
{
    public required ResponseContentUpdateType UpdateType { get; set; }

    public required int LanguageId { get; set; }
    public required int ResourceId { get; set; }
    public required DateTime Timestamp { get; set; }
}

public enum ResponseContentUpdateType
{
    New = 0,
    Updated = 1
}