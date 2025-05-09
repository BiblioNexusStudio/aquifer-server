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
    /// All Resource Contents in Draft status in the project will be considered for translation/aquiferization, but only Resource
    /// Contents in the <see cref="ResourceContentStatus.TranslationAwaitingAiDraft" /> or
    /// <see cref="ResourceContentStatus.AquiferizeAwaitingAiDraft" /> status will be translated, and they will be transitioned to the
    /// <see cref="ResourceContentStatus.TranslationAiDraftComplete" /> or <see cref="ResourceContentStatus.AquiferizeAiDraftComplete" />
    /// status after translation/aquiferization. After all Resource Contents have been translated then all will be assigned to the Project's
    /// Company Lead.
    /// </summary>
    Project,

    /// <summary>
    /// Triggered by Publishers (or others with permission) manually creating a translation for an individual resource content item.
    /// Only Resource Contents in the <see cref="ResourceContentStatus.TranslationAwaitingAiDraft" /> status will be translated,
    /// and they will be transitioned to the <see cref="ResourceContentStatus.TranslationAiDraftComplete" /> status after translation.
    /// </summary>
    CreateTranslation,

    /// <summary>
    /// Triggered by Community Reviewers manually creating a translation for an individual resource content item.
    /// Only Resource Contents in the <see cref="ResourceContentStatus.TranslationAwaitingAiDraft" /> status will be translated,
    /// and they will be transitioned to the <see cref="ResourceContentStatus.TranslationAiDraftComplete" /> status after translation.
    /// </summary>
    CommunityReviewer,

    /// <summary>
    /// Not triggered by a user flow; manually dev triggered only.
    /// Only translates/aquiferizes resource content. Skips all resource content version assignments.
    /// Resource Contents in the <see cref="ResourceContentStatus.TranslationAwaitingAiDraft" /> or
    /// <see cref="ResourceContentStatus.AquiferizeAwaitingAiDraft" /> will be transitioned to
    /// <see cref="ResourceContentStatus.TranslationAiDraftComplete" /> or <see cref="ResourceContentStatus.AquiferizeAiDraftComplete" /> after
    /// translation/aquiferization. Will also translate resource contents that aren't in the
    /// <see cref="ResourceContentStatus.TranslationAwaitingAiDraft" />
    /// or <see cref="ResourceContentStatus.AquiferizeAwaitingAiDraft" /> status but the status will remain unchanged after translation (will
    /// not transition to <see cref="ResourceContentStatus.TranslationAiDraftComplete" /> or
    /// <see cref="ResourceContentStatus.AquiferizeAiDraftComplete" />).
    /// </summary>
    BasicTranslationOnly,

    /// <summary>
    /// Triggered by Publishers (or others with permission) when creating a draft of an English resource for aquiferization.
    /// The Resource Content should be in <see cref="ResourceContentStatus.AquiferizeAwaitingAiDraft" /> status by the time it reaches the
    /// message handler. It will be set to <see cref="ResourceContentStatus.AquiferizeAiDraftComplete" /> status after aquiferization.
    /// </summary>
    CreateAquiferization,

    /// <summary>
    /// Not triggered by a user flow; manually dev triggered only.
    /// Translates all resources in a parent resource into a target language where those resources
    /// don't yet have any resource contents in that target language.
    /// Each translated ResourceContentVersion will be published and the ResourceContent will be marked as Complete.
    /// </summary>
    Language,
}