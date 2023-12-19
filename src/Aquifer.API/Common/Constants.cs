using Aquifer.Data.Entities;
using System.Collections.ObjectModel;

namespace Aquifer.API.Common;

public static class Constants
{
    public const string PermissionsClaim = "permissions";

    // this represents the resource types that we allow to be accessed as the starting point when viewing content
    public static readonly ReadOnlyCollection<string> RootParentResourceNames = new(["CBBTER"]);

    // this represents the media types that we default to English for
    public static readonly ReadOnlyCollection<ResourceContentMediaType> FallbackToEnglishForMediaTypes =
        new([ResourceContentMediaType.Image, ResourceContentMediaType.Video]);

    public static readonly ReadOnlyCollection<string> AllPermissions =
        new([
            PermissionName.ReadUsers,
            PermissionName.EditContent,
            PermissionName.PublishContent,
            PermissionName.AssignContent,
            PermissionName.AssignOverride,
            PermissionName.AquiferizeContent,
            PermissionName.SendReviewContent,
            PermissionName.SendReviewOverride,
            PermissionName.ReviewContent
        ]);
}

public static class PermissionName
{
    public const string ReadUsers = "read:users",
        EditContent = "edit:content",
        PublishContent = "publish:content",
        AquiferizeContent = "aquiferize:content",
        AssignContent = "assign:content",
        AssignOverride = "assign:override",
        ReviewContent = "review:content",
        SendReviewContent = "send-review:content",
        SendReviewOverride = "send-review:override";
}