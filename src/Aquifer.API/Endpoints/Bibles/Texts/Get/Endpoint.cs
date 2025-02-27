﻿using Aquifer.API.Helpers;
using Aquifer.Common.Services.Caching;
using Aquifer.Common.Utilities;
using Aquifer.Data;
using Aquifer.Data.Enums;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;

namespace Aquifer.API.Endpoints.Bibles.Texts.Get;

public class Endpoint(AquiferDbContext dbContext, ICachingVersificationService versificationService) : Endpoint<Request, Response>
{
    public override void Configure()
    {
        Get("/bibles/{BibleId}/texts");
        ResponseCache(EndpointHelpers.OneDayInSeconds);
        AllowAnonymous();
    }

    public override async Task HandleAsync(Request req, CancellationToken ct)
    {
        var response = await dbContext.BibleBookContents
            .Where(bbc => bbc.Bible.Enabled && bbc.Bible.Id == req.BibleId &&
                          ((int)bbc.BookId == req.BookNumber || bbc.Book.Code == req.BookCode))
            .Select(bbc => new Response
            {
                BibleId = bbc.Bible.Id,
                BibleName = bbc.Bible.Name,
                BibleAbbreviation = bbc.Bible.Abbreviation,
                BookCode = bbc.Book.Code,
                BookName = bbc.DisplayName,
                BookNumber = (int)bbc.BookId
            })
            .FirstOrDefaultAsync(ct);

        if (response is not null)
        {
            response.Chapters = await dbContext.BibleTexts
                .Where(bt => bt.BibleId == response.BibleId && (int)bt.BookId == response.BookNumber &&
                             bt.ChapterNumber >= req.StartChapter && bt.ChapterNumber <= req.EndChapter &&
                             (bt.ChapterNumber != req.StartChapter || bt.VerseNumber >= req.StartVerse) &&
                             (bt.ChapterNumber != req.EndChapter || bt.VerseNumber <= req.EndVerse))
                .OrderBy(bt => bt.ChapterNumber)
                .GroupBy(bt => bt.ChapterNumber)
                .Select(bt => new ResponseChapters
                {
                    Number = bt.Key,
                    Verses = bt
                        .OrderBy(bti => bti.VerseNumber)
                        .Select(bti => new ResponseChapterVerses
                        {
                            Number = bti.VerseNumber,
                            Text = bti.Text
                        }).ToList()
                })
                .ToListAsync(ct);
            
            await MapVersificationDifferencesToResponseVersesAsync(req, response, ct);
        }

        await (response is null ? SendNotFoundAsync(ct) : SendOkAsync(response, ct));
    }

    private static string FormatBaseBookChapterVerseMapping(int mappedVerseId)
    {
        var ( targetBookId, targetChapter, targetVerse ) = BibleUtilities.TranslateVerseId(mappedVerseId);
        var targetBookName = BibleBookCodeUtilities.FullNameFromId(targetBookId);
        
        return $"{targetBookName} {targetChapter}:{targetVerse}";
    }

    private static IReadOnlyList<int> GetVerseIds(Response response, BookId bookId)
    {
        return [.. response.Chapters
            .SelectMany(chapter => chapter.Verses
                .Select(verse => BibleUtilities.GetVerseId(bookId, chapter.Number, verse.Number)))];
    }

    private async Task MapVersificationDifferencesToResponseVersesAsync(Request req, Response response, CancellationToken ct)
    {
        if (!response.Chapters.Any())
        {
            return;
        }

        var bookId = BibleBookCodeUtilities.IdFromCode(response.BookCode);
        var verseIds = GetVerseIds(response, bookId);
        
        if (!verseIds.Any())
        {
            return;
        }
        
        var versificationMap = await VersificationUtilities.ConvertVersificationRangeAsync(
            req.BibleId,
            verseIds.Min(),
            verseIds.Max(),
            0, // Target Bible ID is 1 (BSB)? 0 (EVB)?
            versificationService,
            ct);
        
        var baseBibleVerseMappings = versificationMap
            .Where(mapping => mapping.Value.HasValue && mapping.Value.Value != mapping.Key)
            .ToDictionary(
                mapping => mapping.Key,
                mapping => FormatBaseBookChapterVerseMapping(mapping.Value!.Value));
        
        response.Chapters = [.. response.Chapters.Select(chapter =>
        {
            chapter.Verses = [.. chapter.Verses.Select(verse =>
            {
                var verseId = BibleUtilities.GetVerseId(bookId, chapter.Number, verse.Number);
                verse.BaseMapping = baseBibleVerseMappings.GetValueOrDefault(verseId)!;
                
                return verse;
            })];
            
            return chapter;
        })];
    }
}