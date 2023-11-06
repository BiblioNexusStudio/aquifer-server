using Aquifer.API.Utilities;
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
        group.MapGet("/", GetAllBibles);
        group.MapGet("language/{languageId:int}", GetBiblesByLanguage);
        group.MapGet("{bibleId:int}/book/{bookCode}", GetBibleBookDetails);

        return endpoints;
    }

    private async Task<Ok<List<BasicBibleResponse>>> GetAllBibles(AquiferDbContext dbContext,
        CancellationToken cancellationToken)
    {
        var bibles = await dbContext.Bibles.Select(bible => new BasicBibleResponse
        {
            Name = bible.Name,
            Abbreviation = bible.Abbreviation,
            Id = bible.Id,
            SerializedLicenseInfo = bible.LicenseInfo,
            LanguageId = bible.LanguageId
        }).ToListAsync(cancellationToken);

        return TypedResults.Ok(bibles);
    }

    private async Task<Ok<List<BibleResponse>>> GetBiblesByLanguage(int languageId,
        AquiferDbContext dbContext,
        CancellationToken cancellationToken)
    {
        var bibles = await dbContext.Bibles.Where(x => x.LanguageId == languageId)
            .Select(bible => new BibleResponse
            {
                Name = bible.Name,
                Abbreviation = bible.Abbreviation,
                SerializedLicenseInfo = bible.LicenseInfo,
                Id = bible.Id,
                LanguageId = bible.LanguageId,
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
        if (bookId == BookId.None)
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
            AudioUrls = JsonUtilities.DefaultDeserialize(book.AudioUrls),
            BookCode = book.BookId.ToCode(),
            ChapterCount = book.ChapterCount,
            DisplayName = book.DisplayName,
            TextSize = book.TextSize,
            TextUrl = book.TextUrl
        };

        return TypedResults.Ok(response);
    }
}