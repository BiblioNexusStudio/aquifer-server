namespace Aquifer.Common.Messages.Models;

public sealed record TranslateLanguageResourcesMessage(
    int SourceLanguageId,
    int TargetLanguageId,
    int ParentResourceId,
    int StartedByUserId);