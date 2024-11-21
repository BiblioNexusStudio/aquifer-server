using Aquifer.Data.Entities;
using Aquifer.Data.Exceptions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Aquifer.Data.EventHandlers;

public static class BadTranslationPairHandler
{
    private static readonly IReadOnlyList<string> s_KeyBlacklist = ["span", "div"];

    public static void Handle(IEnumerable<EntityEntry> entityEntries)
    {
        entityEntries.Where(entry => entry is { Entity: TranslationPairEntity, State: EntityState.Modified or EntityState.Added })
            .Select(x => x.Entity as TranslationPairEntity)
            .ToList()
            .ForEach(x =>
            {
                if (s_KeyBlacklist.Contains(x?.Key))
                {
                    throw new TranslationPairKeyNotAllowedException();
                }
            });
    }
}