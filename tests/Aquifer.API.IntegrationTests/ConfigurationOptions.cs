﻿using Aquifer.API.Configuration;

namespace Aquifer.API.IntegrationTests;

public sealed class ConfigurationOptions
{
    public required ConnectionStringOptions ConnectionStrings { get; init; }
    public required Auth0Settings IntegrationTestAuth0Settings { get; init; }
    public required UserSettings IntegrationTestUserSettings { get; init; }
    public required string KeyVaultUri { get; init; }
    public required NotificationOptions IntegrationNotificationOptions { get; init; }
    public required string InternalApiKey { get; init; }

    public sealed class ConnectionStringOptions
    {
        public required string BiblioNexusDb { get; init; }
    }

    public sealed class UserSettings
    {
        public required string TestUserPassword { get; init; }
    }

    public class NotificationOptions
    {
        public string? NotifyIdsOnCommunityReviewerComment { get; init; }
    }
}