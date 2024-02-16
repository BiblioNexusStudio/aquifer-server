namespace Aquifer.API.Endpoints.Projects.Update;

public record Request
{
    public int Id { get; set; }

    public required int ProjectManagerUserId { get; set; }
    public required int? CompanyLeadUserId { get; set; }
    public required int? EffectiveWordCount { get; set; }
    public required decimal? QuotedCost { get; set; }
    public required DateOnly? ProjectedDeliveryDate { get; set; }
    public required DateOnly? ProjectedPublishDate { get; set; }
}