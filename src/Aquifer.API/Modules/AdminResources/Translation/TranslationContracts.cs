namespace Aquifer.API.Modules.AdminResources.Translation;

public class CreateTranslationRequest
{
    public int LanguageId { get; set; }
    public int BaseContentId { get; set; }
    public bool UseDraft { get; set; }
}

public class AssignTranslatorRequest
{
    public int AssignedUserId { get; set; }
}