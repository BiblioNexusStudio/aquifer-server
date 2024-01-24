namespace Aquifer.API.Modules.Reports.ResourceItemTotals;

public class ResourceItemTotalsResponse
{
    public int TotalResources { get; set; }
    public int TotalResourcesThisMonth { get; set; }
    public int TotalNonEnglishResources { get; set; }
    public int TotalNonEnglishResourcesThisMonth { get; set; }
    public int TotalResourcesTwoPlusLanguages { get; set; }
    public int TotalResourcesTwoPlusLanguagesThisMonth { get; set; }
    public int AquiferizedResources { get; set; }
    public int AquiferizedResourcesThisMonth { get; set; }
    public int TotalResourceBeingTranslated { get; set; }
    public int TotalResourceBeingTranslatedThisMonth { get; set; }
}