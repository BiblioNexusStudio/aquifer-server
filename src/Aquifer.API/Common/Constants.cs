using Aquifer.Data.Entities;
using System.Collections.ObjectModel;

namespace Aquifer.API.Common;

public static class Constants
{
    // this represents the resource types that we allow to be accessed as the starting point when viewing content
    public static readonly ReadOnlyCollection<string> RootParentResourceNames = new List<string> { "CBBTER" }.AsReadOnly();

    // this represents the media types that we default to English for
    public static readonly ReadOnlyCollection<ResourceContentMediaType> FallbackToEnglishForMediaTypes =
        new List<ResourceContentMediaType> { ResourceContentMediaType.Image, ResourceContentMediaType.Video }
            .AsReadOnly();
}