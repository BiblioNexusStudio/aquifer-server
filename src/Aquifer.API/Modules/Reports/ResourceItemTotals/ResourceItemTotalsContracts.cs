namespace Aquifer.API.Modules.Reports.ResourceItemTotals;

public class ResourceItemTotalsResponse
{
    public int TotalResources { get; set; }
    public int TotalResourcesThisMonth { get; set; }
    public int TotalNonEnglishResources { get; set; }
    public int TotalNonEnglishResourcesThisMonth { get; set; }
    public int TotalResourcesTwoPlus { get; set; }
    public int TotalResourcesTwoPlusThisMonth { get; set; }
    public int AquiferizedResources { get; set; }
    public int AquiferizedResourcesThisMonth { get; set; }
    public int TotalResourceTranslastion { get; set; }
    public int TotalResourceTranslastionedThisMonth { get; set; }
}