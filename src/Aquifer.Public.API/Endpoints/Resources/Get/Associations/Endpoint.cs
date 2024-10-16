using Aquifer.Common.Utilities;
using Aquifer.Data;
using Dapper;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;

namespace Aquifer.Public.API.Endpoints.Resources.Get.Associations;

public class Endpoint(AquiferDbContext dbContext)
    : Endpoint<Request, Response>
{
    public override void Configure()
    {
        Get("/resources/{ResourceId}/associations");
        Description(d => d.ProducesProblemFE(404));
        Summary(s =>
        {
            s.Summary = "Get specific resource associations.";
            s.Description =
                "For a given resource id, return the associations for that resource in their given language. This can be passages and resource items.";
        });
    }

    public override async Task HandleAsync(Request req, CancellationToken ct)
    {
        var dbConnection = dbContext.Database.GetDbConnection();

        const string associatedResourceQuery = """
                                               SELECT RC2.Id AS AssociatedResourceContentId, RCV.DisplayName
                                               FROM ResourceContents RC
                                                   INNER JOIN AssociatedResources AR ON AR.ResourceId = RC.ResourceId
                                                   INNER JOIN ResourceContents RC2 ON AR.AssociatedResourceId = RC2.ResourceId AND RC2.LanguageId = RC.LanguageId
                                                   INNER JOIN ResourceContentVersions RCV ON RC2.Id = RCV.ResourceContentId AND RCV.IsPublished = 1
                                               WHERE
                                                RC.Id = @ResourceId
                                               """;

        var associatedResources = await dbConnection.QueryAsync<AssociatedResourceContent>(associatedResourceQuery, new { req.ResourceId });

        var resourceId = await dbContext.ResourceContents.Where(rc => rc.Id == req.ResourceId)
            .Select(rc => rc.ResourceId)
            .FirstOrDefaultAsync(ct);

        var assocatedVerseResources = await dbContext.VerseResources.Where(vr => vr.ResourceId == resourceId)
            .Select(vr => new VerseReference { VerseId = vr.VerseId })
            .ToListAsync(ct);

        var assocatedPassageReferences = await dbContext.PassageResources.Where(vr => vr.ResourceId == resourceId)
            .Select(pr => new PassageReference
            {
                StartVerseId = pr.Passage.StartVerseId,
                EndVerseId = pr.Passage.EndVerseId
            })
            .ToListAsync(ct);

        var response = new Response
        {
            ResourceItemAssociations =
                associatedResources.Select(ar => new ResourceItemAssociation
                {
                    ContentId = ar.AssociatedResourceContentId,
                    DisplayName = ar.DisplayName
                }).ToList(),
            PassageAssociations = assocatedVerseResources.Select(vr => new PassageAssociation
            {
                StartBookCode = vr.BookCode,
                EndBookCode = vr.BookCode,
                StartChapter = vr.Chapter,
                StartVerse = vr.Verse,
                EndChapter = vr.Chapter,
                EndVerse = vr.Verse
            }).Concat(assocatedPassageReferences.Select(apr => new PassageAssociation
            {
                StartBookCode = apr.StartBookCode,
                EndBookCode = apr.EndBookCode,
                StartChapter = apr.StartChapter,
                StartVerse = apr.StartVerse,
                EndChapter = apr.EndChapter,
                EndVerse = apr.EndVerse
            })).ToList()
        };

        await SendOkAsync(response, ct);
    }

    private class AssociatedResourceContent
    {
        public int AssociatedResourceContentId { get; set; }
        public string DisplayName { get; set; }
    }

    private class VerseReference
    {
        public required int VerseId { get; init; }

        private (Data.Enums.BookId BookId, int Chapter, int Verse) TranslatedVerse =>
            BibleUtilities.TranslateVerseId(VerseId);

        public string BookCode => BibleBookCodeUtilities.CodeFromId(TranslatedVerse.BookId);
        public int Chapter => TranslatedVerse.Chapter;
        public int Verse => TranslatedVerse.Verse;
    }

    private class PassageReference
    {
        public required int StartVerseId { get; init; }
        public required int EndVerseId { get; init; }

        private (Data.Enums.BookId BookId, int Chapter, int Verse) StartTranslatedVerse =>
            BibleUtilities.TranslateVerseId(StartVerseId);

        public string StartBookCode => BibleBookCodeUtilities.CodeFromId(StartTranslatedVerse.BookId);
        public int StartChapter => StartTranslatedVerse.Chapter;
        public int StartVerse => StartTranslatedVerse.Verse;

        private (Data.Enums.BookId BookId, int Chapter, int Verse) EndTranslatedVerse =>
            BibleUtilities.TranslateVerseId(EndVerseId);

        public string EndBookCode => BibleBookCodeUtilities.CodeFromId(EndTranslatedVerse.BookId);
        public int EndChapter => EndTranslatedVerse.Chapter;
        public int EndVerse => EndTranslatedVerse.Verse;
    }
}