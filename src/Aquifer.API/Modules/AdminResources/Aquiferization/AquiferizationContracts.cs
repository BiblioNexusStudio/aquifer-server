namespace Aquifer.API.Modules.AdminResources.Aquiferization;

public record AquiferizationRequest(int? AssignedUserId);

public record PublishRequest(int? AssignedUserId, bool CreateDraft = false);