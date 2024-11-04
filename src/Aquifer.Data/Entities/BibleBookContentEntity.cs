using Aquifer.Data.Enums;
using Aquifer.Data.EventHandlers;
using Microsoft.EntityFrameworkCore;

namespace Aquifer.Data.Entities;

[PrimaryKey(nameof(BibleId), nameof(BookId))]
public class BibleBookContentEntity : IHasUpdatedTimestamp
{
    public int BibleId { get; set; }
    public BookId BookId { get; set; }
    public string DisplayName { get; set; } = null!;

    /// <summary>
    ///     JSON column. See <see cref="AudioUrlsData" /> for the deserialization class.
    /// </summary>
    public string? AudioUrls { get; set; }

    public int TextSize { get; set; }
    public int AudioSize { get; set; }
    public int ChapterCount { get; set; }

    [SqlDefaultValue("getutcdate()")]
    public DateTime Created { get; set; } = DateTime.UtcNow;

    public BibleEntity Bible { get; set; } = null!;
    public BookEntity Book { get; set; } = null!;

    [SqlDefaultValue("getutcdate()")]
    public DateTime Updated { get; set; } = DateTime.UtcNow;

    /// <summary>
    ///     POCO for deserializing the AudioUrls column.
    /// </summary>
    public sealed class AudioUrlsData
    {
        public IReadOnlyList<Chapter>? Chapters { get; set; }

        public sealed class Chapter
        {
            /// <summary>
            ///     The chapter number is a JSON string type but is currently only populated with integer values (this might change in the future).
            /// </summary>
            public required string Number { get; set; }

            public AudioUrl? Webm { get; set; }
            public AudioUrl? Mp3 { get; set; }
            public IReadOnlyList<AudioTimestamp>? AudioTimestamps { get; set; }

            public sealed class AudioUrl
            {
                public required string Url { get; set; }
                public required int Size { get; set; }
            }

            public sealed class AudioTimestamp
            {
                /// <summary>
                ///     The verse number is a JSON string type but is currently only populated with integer values (this might change in the future).
                /// </summary>
                public required string VerseNumber { get; set; }

                public required decimal Start { get; set; }
                public required decimal End { get; set; }
            }
        }
    }
}