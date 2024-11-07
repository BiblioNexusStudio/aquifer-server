namespace Aquifer.Common.Jobs.Messages;

public sealed record TranslateProjectResourcesMessage(int ProjectId, int StartedByUserId);