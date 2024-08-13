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
}

public static class PermissionName
{
    public const string ReadUsers = "read:users",
        ReadAllUsers = "read:all-users",
        ReadCompanyContentAssignments = "read:company-content-assignments",
        EditContent = "edit:content",
        EditContentBibleReferences = "edit:content-bible-references",
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
        ReadProjectsInCompany = "read:projects-in-company",
        CreateUser = "create:user",
        CreateUsersInCompany = "create:users-in-company",
        ReadReports = "read:reports",
        AiSimplify = "ai:simplify",
        AiTranslate = "ai:translate",
        DisableUser = "disable:users",
        DisableUsersInCompany = "disable:users-in-company";
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