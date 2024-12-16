namespace Aquifer.Common.Messages.Models;

public sealed record TranslateLanguageResourcesMessage(
    int LanguageId,
    int ParentResourceId,
    int StartedByUserId);