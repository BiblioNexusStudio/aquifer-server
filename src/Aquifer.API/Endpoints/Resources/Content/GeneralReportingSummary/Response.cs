using System.Text.Json.Serialization;

namespace Aquifer.API.Endpoints.Resources.Content.GeneralReportingSummary;

public record Response
{
    public required List<ByParentResourceResponse> ResourcesByParentResource { get; set; }
    public required List<ByLanguageResponse> ResourcesByLanguage { get; set; }
    public required List<ParentResourceTotalsByMonthResponse> TotalsByMonth { get; set; }
    public required int AllResourcesCount { get; set; }
    public required int MultiLanguageResourcesCount { get; set; }
    public required List<string> Languages { get; set; }
    public required List<string> ParentResourceNames { get; set; }
}

public record ByParentResourceResponse
{
    public required int ResourceCount { get; set; }
    public required string ParentResourceName { get; set; }

    [JsonIgnore]
    public DateTime FullDateTime { get; set; }

    public DateOnly Date => DateOnly.FromDateTime(FullDateTime);
    public string MonthAbbreviation => FullDateTime.ToString("MMM");
}

public record ByLanguageResponse
{
    public required string Language { get; set; }
    public required int ResourceCount { get; set; }
    public required string ParentResourceName { get; set; }

    [JsonIgnore]
    public DateTime FullDateTime { get; set; }

    public DateOnly Date => DateOnly.FromDateTime(FullDateTime);
    public string MonthAbbreviation => FullDateTime.ToString("MMM");
}

public record ParentResourceTotalsByMonthResponse
{
    public required DateOnly Date { get; set; }
    public required string MonthAbbreviation { get; set; }
    public required int ResourceCount { get; set; }
}