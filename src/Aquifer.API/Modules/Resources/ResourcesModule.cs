using Aquifer.API.Modules.Resources.LanguageResources;
using Aquifer.API.Modules.Resources.ResourceContentBatch;
using Aquifer.API.Modules.Resources.ResourceContentItem;

namespace Aquifer.API.Modules.Resources;

public class ResourcesModule : IModule
{
    public IEndpointRouteBuilder MapEndpoints(IEndpointRouteBuilder endpoints)
    {
        var group = endpoints.MapGroup("resources").WithTags("Resources");
        group.MapGet("{contentId:int}/content", ResourceContentItemEndpoints.GetResourceContentById);
        group.MapGet("{contentId:int}/metadata", ResourceContentItemEndpoints.GetResourceMetadataById);
        group.MapGet("{contentId:int}/thumbnail", ResourceContentItemEndpoints.GetResourceThumbnailById);
        group.MapGet("batch/metadata", ResourceContentBatchEndpoints.GetResourceMetadataByIds);
        group.MapGet("batch/content/text", ResourceContentBatchEndpoints.GetResourceTextContentByIds);
        group.MapGet("language/{languageId:int}/book/{bookCode}", LanguageResourcesEndpoints.GetBookByLanguage);

        return endpoints;
    }
}