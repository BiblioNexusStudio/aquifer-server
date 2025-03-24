using System.Reflection;

namespace Aquifer.Common.Messages;

public static class Queues
{
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

    // uploads
    public const string UploadResourceContentAudio = "upload-resource-content-audio";

    // similarities
    public const string GenerateResourceContentSimilarityScore = "generate-resource-content-similarity-score";

    public static IReadOnlyList<string> AllQueues { get; } = typeof(Queues)
        .GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy)
        .Where(qn => qn is { IsLiteral: true, IsInitOnly: false } && qn.FieldType == typeof(string))
        .Select(qn => (string)qn.GetRawConstantValue()!)
        .ToList();

    public static IReadOnlyList<string> AllPoisonQueues { get; } = AllQueues
        .Select(GetPoisonQueueName)
        .ToList();

    public static string GetPoisonQueueName(string queueName)
    {
        return $"{queueName}-poison";
    }
}