using Aquifer.Data.Entities;
using Aquifer.Data.Exceptions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Aquifer.Data.EventHandlers;

public static class BadTranslationPairHandler
{
    private static readonly IReadOnlySet<string> s_KeyBlacklist = new HashSet<string>
    {
        "span",
        "div"
    };

    public static void Handle(IEnumerable<EntityEntry> entityEntries)
    {
        entityEntries.Where(entry => entry is { Entity: TranslationPairEntity, State: EntityState.Modified or EntityState.Added })
            .Select(x => x.Entity as TranslationPairEntity)
            .ToList()
            .ForEach(x =>
            {
                if (x is null)
                {
                    return;
                }

                if (x.Key.Length < 3 || s_KeyBlacklist.Contains(x.Key))
                {
                    throw new TranslationPairKeyNotAllowedException(
                        $"The key ${x.Key} is not allowed. Keys 2 or fewer characters and some specific keys are not allowed.");
                }
            });
    }
}