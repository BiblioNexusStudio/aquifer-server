using Aquifer.Data.Entities;
using System.Collections.ObjectModel;

namespace Aquifer.API.Common;

public static class Constants
{
    // this represents the resource types that we allow to be accessed as the starting point when viewing content
    public static readonly ReadOnlyCollection<string> RootParentResourceNames =
        new List<string> { "CBBTER" }.AsReadOnly();

    // this represents the media types that we default to English for
    public static readonly ReadOnlyCollection<ResourceContentMediaType> FallbackToEnglishForMediaTypes =
        new List<ResourceContentMediaType> { ResourceContentMediaType.Image, ResourceContentMediaType.Video }
            .AsReadOnly();

    public static readonly string PermissionsClaim = "permissions";

    public static readonly ReadOnlyCollection<string> AllPermissions =
        new List<string> {
            PermissionName.ReadUsers,
            PermissionName.EditContent,
            PermissionName.PublishContent,
            PermissionName.AssignContent,
            PermissionName.AssignOverride,
            PermissionName.AquiferizeContent
        }.AsReadOnly();
}

public static class PermissionName
{
    public const string ReadUsers = "read:users",
        EditContent = "edit:content",
        PublishContent = "publish:content",
        AquiferizeContent = "aquiferize:content",
        AssignContent = "assign:content",
        AssignOverride = "assign:override",
        ReviewContent = "review:content";
}