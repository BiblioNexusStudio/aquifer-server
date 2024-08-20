namespace Aquifer.Public.API.Endpoints.Resources.Get.ByLanguage;

public record Request : Get.Request
{
    /// <summary>
    ///     Language code (e.g. eng) that the given content id should be sent back in. For example,
    ///     if you have the English content id for a specific item, and you want to get the French version, pass "fra" as the value.
    ///     If no version exists in the requested language, a 404 will be returned.
    /// </summary>
    public string? LanguageCode { get; init; } = null!;
}