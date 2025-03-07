﻿namespace Aquifer.Common.Messages;

public static class Queues
{
    // audio
    public const string CompressAudio = "compress-audio";

    // email
    public const string SendEmail = "send-email";
    public const string SendTemplatedEmail = "send-templated-email";

    // notifications
    public const string SendProjectStartedNotification = "send-project-started-notification";

    // resource tracking
    public const string TrackResourceContentRequest = "track-resource-content-request";

    // translations
    public const string TranslateLanguageResources = "translate-language-resources";
    public const string TranslateProjectResources = "translate-project-resources";
    public const string TranslateResource = "translate-resource";
    
    // similarities
    public const string GenerateResourceContentSimilarityScore = "generate-resource-content-similarity-score";

    public static string GetPoisonQueueName(string queueName)
    {
        return $"{queueName}-poison";
    }
}