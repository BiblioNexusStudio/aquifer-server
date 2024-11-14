﻿namespace Aquifer.Common.Messages;

public static class Queues
{
    // email
    public const string SendEmail = "send-email";
    public const string SendTemplatedEmail = "send-templated-email";

    // notifications
    public const string SendProjectStartedNotification = "send-project-started-notification";
    public const string SendResourceCommentCreatedNotification = "send-resource-comment-created-notification";

    // resource tracking
    public const string TrackResourceContentRequest = "track-resource-content-request";

    // translations
    public const string TranslateProjectResources = "translate-project-resources";
    public const string TranslateResource = "translate-resource";

    public static string GetPoisonQueueName(string queueName)
    {
        return $"{queueName}-poison";
    }
}