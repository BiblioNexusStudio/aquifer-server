using Aquifer.Data.Entities;

namespace Aquifer.Common.Messages.Models;

public enum TranslationOrigin
{
    /// <summary>
    /// Do not use. Exists to handle accidental default assignment (likely through serialization/deserialization).
    /// </summary>
    None = 0,

    /// <summary>
    /// Triggered by starting a project.
    /// All Resource Contents in Draft status in the project will be considered for translation, but
    /// only Resource Contents in the <see cref="ResourceContentStatus.TranslationAwaitingAiDraft"/> status will be translated,
    /// and they will be transitioned to the <see cref="ResourceContentStatus.TranslationAiDraftComplete"/> status after translation.
    /// After all Resource Contents have been translated then all will be assigned to the Project's Company Lead.
    /// </summary>
    Project,

    /// <summary>
    /// Triggered by Publishers (or others with permission) manually creating a translation for an individual resource content item.
    /// Only Resource Contents in the <see cref="ResourceContentStatus.TranslationAwaitingAiDraft"/> status will be translated,
    /// and they will be transitioned to the <see cref="ResourceContentStatus.TranslationAiDraftComplete"/> status after translation.
    /// </summary>
    CreateTranslation,

    /// <summary>
    /// Triggered by Community Reviewers manually creating a translation for an individual resource content item.
    /// Only Resource Contents in the <see cref="ResourceContentStatus.TranslationAwaitingAiDraft"/> status will be translated,
    /// and they will be transitioned to the <see cref="ResourceContentStatus.TranslationAiDraftComplete"/> status after translation.
    /// </summary>
    CommunityReviewer,

    /// <summary>
    /// Not triggered by a user flow; manually dev triggered only.
    /// Only translates resource content. Skips all resource content version assignments.
    /// Resource Contents in the <see cref="ResourceContentStatus.TranslationAwaitingAiDraft"/> will be transitioned to
    /// <see cref="ResourceContentStatus.TranslationAiDraftComplete"/> after translation.
    /// Will also translate resource contents that aren't in the <see cref="ResourceContentStatus.TranslationAwaitingAiDraft"/> status
    /// but the status will remain unchanged after translation (will not transition to
    /// <see cref="ResourceContentStatus.TranslationAiDraftComplete"/>).
    /// </summary>
    BasicTranslationOnly,
}