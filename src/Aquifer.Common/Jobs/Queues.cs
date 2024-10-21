namespace Aquifer.Common.Jobs;

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
}