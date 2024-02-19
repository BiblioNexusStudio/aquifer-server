namespace Aquifer.API.Endpoints.Projects.Update;

public record Request
{
    public int Id { get; set; }
    public int? ProjectManagerUserId { get; set; }
    public int? CompanyLeadUserId { get; set; }
    public int? EffectiveWordCount { get; set; }
    public decimal? QuotedCost { get; set; }
    public DateOnly? ProjectedDeliveryDate { get; set; }
    public DateOnly? ProjectedPublishDate { get; set; }
}