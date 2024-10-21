namespace Aquifer.Common.Jobs;

public static class Queues
{
    public const string SendEmail = "aquifer-jobs-send-email";
    public const string SendTemplatedEmail = "aquifer-jobs-send-templated-email";
    public const string TrackResourceContentRequest = "aquifer-jobs-track-resource-content-request";
    public const string SendProjectStartedNotification = "aquifer-jobs-send-project-started-notification";
    public const string SendResourceCommentCreatedNotification = "aquifer-jobs-send-resource-comment-created-notification";
}