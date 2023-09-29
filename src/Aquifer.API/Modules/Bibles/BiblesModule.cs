using Aquifer.Data;
using Aquifer.Data.Enums;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace Aquifer.API.Modules.Bibles;

public class BiblesModule : IModule
{
    public IEndpointRouteBuilder MapEndpoints(IEndpointRouteBuilder endpoints)
    {
        var group = endpoints.MapGroup("bibles");
        group.MapGet("language/{languageId:int}", GetBibleByLanguage);
        group.MapGet("{bibleId:int}/book/{bookCode}", GetBibleBookDetails);

        return endpoints;
    }

    private async Task<Ok<List<BibleResponse>>> GetBibleByLanguage(int languageId,
        AquiferDbContext dbContext,
        CancellationToken cancellationToken)
    {
        var bibles = await dbContext.Bibles.Where(x => x.LanguageId == languageId)
            .Select(bible => new BibleResponse
            {
                Name = bible.Name,
                Abbreviation = bible.Abbreviation,
                Id = bible.Id,
                Books = bible.BibleBookContents.OrderBy(book => book.BookId).Select(book =>
                    new BibleResponseBook
                    {
                        BookCode = book.BookId.ToCode(),
                        DisplayName = book.DisplayName,
                        TextSize = book.TextSize,
                        AudioSize = book.AudioSize,
                        ChapterCount = book.ChapterCount
                    })
            }).ToListAsync(cancellationToken);

        return TypedResults.Ok(bibles);
    }

    private async Task<Results<Ok<BibleBookDetailsResponse>, NotFound>> GetBibleBookDetails(
        int bibleId,
        string bookCode,
        AquiferDbContext dbContext,
        CancellationToken cancellationToken)
    {
        var bookId = BookIdSerializer.FromCode(bookCode);
        if (bookId == null)
        {
            return TypedResults.NotFound();
        }

        var book = await dbContext.BibleBookContents
            .SingleOrDefaultAsync(b => b.BibleId == bibleId && b.BookId == bookId, cancellationToken);

        if (book == null)
        {
            return TypedResults.NotFound();
        }

        var response = new BibleBookDetailsResponse
        {
            AudioSize = book.AudioSize,
            AudioUrls = book.AudioUrls,
            BookCode = book.BookId.ToCode(),
            ChapterCount = book.ChapterCount,
            DisplayName = book.DisplayName,
            TextSize = book.TextSize,
            TextUrl = book.TextUrl
        };

        return TypedResults.Ok(response);
    }
}