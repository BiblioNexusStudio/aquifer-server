using System.Collections.ObjectModel;
using Aquifer.Data.Entities;

namespace Aquifer.API.Common;

public static class Constants
{
    public const string PermissionsClaim = "permissions";
    public const string RolesClaim = "bnRoles";

    // this represents the resource types that we allow to be accessed as the starting point when viewing content
    public static readonly ReadOnlyCollection<string> RootParentResourceNames = new([
        "CBBTER"
    ]);

    // this represents the media types that we default to English for
    public static readonly ReadOnlyCollection<ResourceContentMediaType> FallbackToEnglishForMediaTypes =
        new([
            ResourceContentMediaType.Image,
            ResourceContentMediaType.Video
        ]);

    public static readonly ReadOnlyCollection<string> AllPermissions =
        new([
            PermissionName.ReadUsers,
            PermissionName.EditContent,
            PermissionName.PublishContent,
            PermissionName.AssignContent,
            PermissionName.AssignOverride,
            PermissionName.AssignOutsideCompany,
            PermissionName.CreateContent,
            PermissionName.SendReviewContent,
            PermissionName.ReviewContent,
            PermissionName.CreateProject,
            PermissionName.CreateUser,
            PermissionName.ReadProject,
            PermissionName.EditProject
        ]);
}

public static class PermissionName
{
    public const string ReadUsers = "read:users",
        ReadAllUsers = "read:all-users",
        EditContent = "edit:content",
        PublishContent = "publish:content",
        CreateContent = "create:content",
        AssignContent = "assign:content",
        AssignOverride = "assign:override",
        AssignOutsideCompany = "assign:outside-company",
        ReviewContent = "review:content",
        SendReviewContent = "send-review:content",
        CreateProject = "create:project",
        EditProject = "edit:projects",
        ReadProject = "read:projects",
        CreateUser = "create:user";
}

public static class KeyVaultSecretName
{
    public const string Auth0ClientSecret = "Auth0ClientSecret";
}

public static class Auth0Constants
{
    public const string ClientCredentials = "client_credentials";
    public const string Connection = "Username-Password-Authentication";
}