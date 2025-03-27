using System.Text.RegularExpressions;
using Aquifer.Data.Entities;

namespace Aquifer.Data.Utilities;

public static partial class CommentMentionsUtility
{
    /// <summary>
    /// Parses mentions from the comment and updates the <paramref name="comment"/>'s <see cref="CommentEntity.Mentions"/>.
    /// Note: Caller must save DbContext changes.
    /// </summary>
    public static void UpsertCommentMentions(AquiferDbContext dbContext, CommentEntity comment)
    {
        var parsedMentionedUserIds = ParseMentionedUserIdsFromCommentText(comment.Comment);
        var recordedMentionedUserIds = comment.Mentions.Select(m => m.UserId).ToHashSet();

        var existingMentionsToDelete = comment.Mentions
            .Where(m => !parsedMentionedUserIds.Contains(m.UserId))
            .ToList();

        var userIdMentionsToAdd = parsedMentionedUserIds
            .Where(userId => !recordedMentionedUserIds.Contains(userId))
            .Select(userId => new CommentMentionEntity
            {
                CommentId = comment.Id,
                UserId = userId,
            })
            .ToList();

        dbContext.CommentMentions.RemoveRange(existingMentionsToDelete);

        dbContext.CommentMentions.AddRange(userIdMentionsToAdd);
    }

    public static IReadOnlySet<int> ParseMentionedUserIdsFromCommentText(string text)
    {
        return MentionsRegex()
            .Matches(text)
            .Where(m => m.Success)
            .Select(m => int.Parse(m.Groups["userId"].Value))
            .ToHashSet();
    }

    // examples:
    // {@123|John Doe}
    // {@98342|X Æ A-12}
    [GeneratedRegex(@"\{@(?<userId>\d+)\|(?<fallbackUserName>[^}]+)\}")]
    private static partial Regex MentionsRegex();
}