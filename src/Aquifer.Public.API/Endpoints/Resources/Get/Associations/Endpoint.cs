using System.Data.Common;
using Aquifer.Common.Utilities;
using Aquifer.Data;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;

namespace Aquifer.Public.API.Endpoints.Resources.Get.Associations;

public class Endpoint(AquiferDbReadOnlyContext dbContext) : Endpoint<Request, Response>
{
    public override void Configure()
    {
        Get("/resources/{ContentId}/associations");
        Description(d => d.WithTags("Resources"));
        Summary(s =>
        {
            s.Summary = "Get resource content associations.";
            s.Description =
                "For a given resource id, return the associations for that resource in its given language. Resource associations are language dependent. In general, all resource associations will be available in English.";
        });
    }

    public override async Task HandleAsync(Request req, CancellationToken ct)
    {
        var dbConnection = dbContext.Database.GetDbConnection();
        var verses = await GetAssociatedVerseReferencesAsync(dbConnection, req, ct);
        var passages = await GetAssociatedPassageReferencesAsync(dbConnection, req, ct);
        var associatedResources = await GetResourceReferencesAsync(dbConnection, req, ct);

        var response = new Response
        {
            ResourceAssociations = associatedResources.Select(ar => new ResourceAssociation
                {
                    ContentId = ar.ContentId,
                    DisplayName = ar.DisplayName,
                    ReferenceId = ar.ReferenceId,
                })
                .ToList(),
            PassageAssociations = verses.Select(vr => new PassageAssociation
                {
                    StartVerseId = vr.VerseId,
                    EndVerseId = vr.VerseId,
                })
                .Concat(
                    passages.Select(pr => new PassageAssociation
                    {
                        StartVerseId = pr.StartVerseId,
                        EndVerseId = pr.EndVerseId,
                    }))
                .ToList(),
        };

        foreach (var passageAssociation in response.PassageAssociations)
        {
            var startTranslatedVerse = BibleUtilities.TranslateVerseId(passageAssociation.StartVerseId);
            passageAssociation.StartBookCode = BibleBookCodeUtilities.CodeFromId(startTranslatedVerse.bookId);
            passageAssociation.StartChapter = startTranslatedVerse.chapter;
            passageAssociation.StartVerse = startTranslatedVerse.verse;

            var endTranslatedVerse = BibleUtilities.TranslateVerseId(passageAssociation.EndVerseId);
            passageAssociation.EndBookCode = BibleBookCodeUtilities.CodeFromId(endTranslatedVerse.bookId);
            passageAssociation.EndChapter = endTranslatedVerse.chapter;
            passageAssociation.EndVerse = endTranslatedVerse.verse;
        }

        await SendOkAsync(response, ct);
    }

    private static async Task<IReadOnlyList<VerseReference>> GetAssociatedVerseReferencesAsync(
        DbConnection dbConnection,
        Request req,
        CancellationToken ct)
    {
        const string query = """
            SELECT VR.VerseId
            FROM VerseResources VR
            INNER JOIN Resources R ON VR.ResourceId = R.Id
            INNER JOIN ResourceContents RC ON R.Id = RC.ResourceId AND RC.Id = @ContentId
            """;

        return (await dbConnection.QueryWithRetriesAsync<VerseReference>(
            query,
            new
            {
                req.ContentId,
            },
            cancellationToken: ct)).ToList();
    }

    private static async Task<IReadOnlyList<PassageReference>> GetAssociatedPassageReferencesAsync(
        DbConnection dbConnection,
        Request req,
        CancellationToken ct)
    {
        const string query = """
            SELECT P.StartVerseId, P.EndVerseId
            FROM Passages P
                    INNER JOIN PassageResources PR ON P.Id = PR.PassageId
                     INNER JOIN Resources R ON PR.ResourceId = R.Id
                     INNER JOIN ResourceContents RC ON R.Id = RC.ResourceId AND RC.Id = @ContentId
            """;

        return (await dbConnection.QueryWithRetriesAsync<PassageReference>(
            query,
            new
            {
                req.ContentId,
            },
            cancellationToken: ct)).ToList();
    }

    private static async Task<IReadOnlyList<ResourceReference>> GetResourceReferencesAsync(
        DbConnection dbConnection,
        Request req,
        CancellationToken ct)
    {
        const string query = """
            SELECT RC2.Id AS ContentId, RCV.DisplayName, RC2.ResourceId AS ReferenceId
            FROM ResourceContents RC
                INNER JOIN AssociatedResources AR ON AR.ResourceId = RC.ResourceId
                INNER JOIN ResourceContents RC2 ON AR.AssociatedResourceId = RC2.ResourceId AND RC2.LanguageId = RC.LanguageId
                INNER JOIN ResourceContentVersions RCV ON RC2.Id = RCV.ResourceContentId AND RCV.IsPublished = 1
            WHERE
             RC.Id = @ContentId
            """;

        return (await dbConnection.QueryWithRetriesAsync<ResourceReference>(
            query,
            new
            {
                req.ContentId,
            },
            cancellationToken: ct)).ToList();
    }

    private record ResourceReference(int ContentId, string DisplayName, int ReferenceId);

    private record VerseReference(int VerseId);

    private record PassageReference(int StartVerseId, int EndVerseId);
}