using Aquifer.API.Common;
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
        var group = endpoints.MapGroup("bibles").WithTags("Bibles");
        group.MapGet("/", GetAllBibles);
        group.MapGet("language/{languageId:int}", GetBiblesByLanguage);
        group.MapGet("{bibleId:int}/book/{bookCode}", GetBibleBookDetails);

        return endpoints;
    }

    private async Task<Ok<List<BibleResponse>>> GetAllBibles(AquiferDbContext dbContext,
        CancellationToken cancellationToken)
    {
        var bibles = await dbContext.Bibles.Select(bible => new BibleResponse
        {
            Name = bible.Name,
            Abbreviation = bible.Abbreviation,
            Id = bible.Id,
            SerializedLicenseInfo = bible.LicenseInfo,
            LanguageId = bible.LanguageId
        }).ToListAsync(cancellationToken);

        return TypedResults.Ok(bibles);
    }

    private async Task<Ok<List<BibleWithBooksMetadataResponse>>> GetBiblesByLanguage(int languageId,
        AquiferDbContext dbContext,
        CancellationToken cancellationToken)
    {
        var bibles = await dbContext.Bibles.Where(x => x.LanguageId == languageId)
            .Select(bible => new BibleWithBooksMetadataResponse
            {
                Name = bible.Name,
                Abbreviation = bible.Abbreviation,
                SerializedLicenseInfo = bible.LicenseInfo,
                Id = bible.Id,
                LanguageId = bible.LanguageId,
                Books = bible.BibleBookContents.OrderBy(book => book.BookId).Select(book =>
                    new BibleBookMetadataResponse
                    {
                        BookCode = BookCodes.CodeFromId(book.BookId),
                        DisplayName = book.DisplayName,
                        TextSize = book.TextSize,
                        AudioSize = book.AudioSize,
                        ChapterCount = book.ChapterCount
                    })
            }).ToListAsync(cancellationToken);

        return TypedResults.Ok(bibles);
    }

    private async Task<Results<Ok<BibleBookResponse>, NotFound>> GetBibleBookDetails(
        int bibleId,
        string bookCode,
        AquiferDbContext dbContext,
        CancellationToken cancellationToken)
    {
        var bookCodeEnum = BookCodes.IdFromCode(bookCode);
        if (bookCodeEnum == BookId.None)
        {
            return TypedResults.NotFound();
        }

        var book = await dbContext.BibleBookContents
            .SingleOrDefaultAsync(b => b.BibleId == bibleId && b.BookId == bookCodeEnum, cancellationToken);

        if (book == null)
        {
            return TypedResults.NotFound();
        }

        var response = new BibleBookResponse
        {
            AudioSize = book.AudioSize,
            AudioUrls = JsonUtilities.DefaultDeserialize(book.AudioUrls),
            BookCode = BookCodes.CodeFromId(book.BookId),
            ChapterCount = book.ChapterCount,
            DisplayName = book.DisplayName,
            TextSize = book.TextSize,
            TextUrl = book.TextUrl
        };

        return TypedResults.Ok(response);
    }
}