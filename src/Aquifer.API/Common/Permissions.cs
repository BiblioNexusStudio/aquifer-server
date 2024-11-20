namespace Aquifer.API.Common;

public static class Permissions
{
    public const string PermissionsClaim = "permissions";
    public const string RolesClaim = "bnRoles";
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
        SendReviewCommunityContent = "send-review:community-content",
        SetStatusTranslationNotApplicable = "set-status:translation-not-applicable",
        SetStatusCompleteNotApplicable = "set-status:complete-not-applicable";
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