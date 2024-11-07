namespace Aquifer.Common.Jobs.Messages;

public sealed record TranslateResourceMessage(int ResourceContentId, int StartedByUserId, TranslationOrigin TranslationOrigin);