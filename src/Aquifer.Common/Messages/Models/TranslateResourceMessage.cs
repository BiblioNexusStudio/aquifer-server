namespace Aquifer.Common.Messages.Models;

public sealed record TranslateResourceMessage(int ResourceContentId, int StartedByUserId, TranslationOrigin TranslationOrigin);