using Aquifer.Data.Entities;

namespace Aquifer.API.Modules.Resources.ResourceTypes;

public record ResourceTypeResponse(int Id, string DisplayName, ResourceTypeComplexityLevel ComplexityLevel);