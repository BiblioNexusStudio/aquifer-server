using System.Collections.ObjectModel;
using Aquifer.Data.Entities;

namespace Aquifer.API.Common;

public static class Constants
{
    public const string PermissionsClaim = "permissions";
    public const string RolesClaim = "bnRoles";

    // Some guide types have predetermined passages (rather than freeform "select any Bible section").
    // ID 1 = FIA (formerly CBBT-ER)
    public static readonly ReadOnlyCollection<int> PredeterminedPassageGuideIds = new([1]);

    // this represents the media types that we default to English for
    public static readonly ReadOnlyCollection<ResourceContentMediaType> FallbackToEnglishForMediaTypes =
        new([ResourceContentMediaType.Image, ResourceContentMediaType.Video]);

    public static readonly ReadOnlyCollection<ResourceContentStatus> AquiferizationStatuses =
        new([
            ResourceContentStatus.AquiferizeInProgress,
            ResourceContentStatus.AquiferizeReviewPending,
            ResourceContentStatus.AquiferizePublisherReview,
            ResourceContentStatus.AquiferizeManagerReview
        ]);

    public static readonly ReadOnlyCollection<ResourceContentStatus> TranslationStatuses =
        new([
            ResourceContentStatus.TranslationNotStarted,
            ResourceContentStatus.TranslationInProgress,
            ResourceContentStatus.TranslationReviewPending,
            ResourceContentStatus.TranslationPublisherReview,
            ResourceContentStatus.TranslationManagerReview
        ]);

    public static readonly ReadOnlyCollection<ResourceContentStatus> ReviewPendingStatuses =
        new([
            ResourceContentStatus.TranslationReviewPending,
            ResourceContentStatus.AquiferizeReviewPending
        ]);

    public static readonly ReadOnlyCollection<ResourceContentStatus> PublisherReviewStatuses =
        new([
            ResourceContentStatus.TranslationPublisherReview,
            ResourceContentStatus.AquiferizePublisherReview
        ]);

    public static readonly ReadOnlyCollection<ResourceContentStatus> ManagerReviewStatuses =
        new([
            ResourceContentStatus.TranslationManagerReview,
            ResourceContentStatus.AquiferizeManagerReview
        ]);
}

public static class PermissionName
{
    public const string AiSimplify = "ai:simplify",
        AiTranslate = "ai:translate",
        AssignContent = "assign:content",
        AssignOutsideCompany = "assign:outside-company",
        AssignOverride = "assign:override",
        CreateCommunityContent = "create:community-content",
        CreateContent = "create:content",
        CreateProject = "create:project",
        CreateUser = "create:user",
        CreateUsersInCompany = "create:users-in-company",
        DisableUser = "disable:users",
        DisableUsersInCompany = "disable:users-in-company",
        EditContent = "edit:content",
        EditContentBibleReferences = "edit:content-bible-references",
        EditContentResourceReferences = "edit:content-resource-references",
        EditProject = "edit:projects",
        PublishContent = "publish:content",
        ReadAllUsers = "read:all-users",
        ReadCompanyContentAssignments = "read:company-content-assignments",
        ReadProject = "read:projects",
        ReadProjectsInCompany = "read:projects-in-company",
        ReadReports = "read:reports",
        ReadResourceLists = "read:resource-lists",
        ReadResources = "read:resources",
        ReadUsers = "read:users",
        ReviewContent = "review:content",
        SendReviewContent = "send-review:content",
        SendReviewCommunityContent = "send-review:community-content";
}

public static class KeyVaultSecretName
{
    public const string Auth0ClientSecret = "Auth0ClientSecret",
        OpenAiApiKey = "OpenAiApiKey";
}

public static class Auth0Constants
{
    public const string ClientCredentials = "client_credentials";
    public const string Connection = "Username-Password-Authentication";
}