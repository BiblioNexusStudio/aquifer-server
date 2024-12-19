using Aquifer.Public.API.Endpoints.Resources.Collections.Get;
using FluentValidation.TestHelper;

namespace Aquifer.Public.API.UnitTests.Endpoints.Resources.Collections.Get;

public sealed class ValidatorTests
{
    private readonly Validator _validator = new();

    public static TheoryData<Request, string> GetInvalidRequestData => new()
    {
        { new Request { Code = "", LanguageCodes = null, LanguageIds = null }, nameof(Request.Code) },
        { new Request { Code = " ", LanguageCodes = null, LanguageIds = null }, nameof(Request.Code) },
        { new Request { Code = "TyndaleBibleDictionary", LanguageCodes = [""], LanguageIds = null }, $"{nameof(Request.LanguageCodes)}[0]" },
        { new Request { Code = "TyndaleBibleDictionary", LanguageCodes = [" "], LanguageIds = null }, $"{nameof(Request.LanguageCodes)}[0]" },
        { new Request { Code = "TyndaleBibleDictionary", LanguageCodes = ["InvalidLength"], LanguageIds = null }, $"{nameof(Request.LanguageCodes)}[0]" },
        { new Request { Code = "TyndaleBibleDictionary", LanguageCodes = null, LanguageIds = [-1] }, $"{nameof(Request.LanguageIds)}[0]" },
        { new Request { Code = "TyndaleBibleDictionary", LanguageCodes = null, LanguageIds = [int.MaxValue] }, $"{nameof(Request.LanguageIds)}[0]" },
        { new Request { Code = "TyndaleBibleDictionary", LanguageCodes = [], LanguageIds = [] }, "" },
        { new Request { Code = "TyndaleBibleDictionary", LanguageCodes = ["eng"], LanguageIds = [1] }, "" },
    };

    [Theory]
    [MemberData(nameof(GetInvalidRequestData))]
    public void Validator_InvalidRequestProperty_ShouldReturnValidationError(Request request, string expectedInvalidPropertyName)
    {
        var validationResult = _validator.TestValidate(request);

        if (string.IsNullOrEmpty(expectedInvalidPropertyName))
        {
            validationResult.ShouldHaveAnyValidationError();
        }
        else
        {
            validationResult
                .ShouldHaveValidationErrorFor(expectedInvalidPropertyName)
                .Only();
        }
    }

    public static TheoryData<Request> GetValidRequestData =>
    [
        new() { Code = "TyndaleBibleDictionary", LanguageCodes = null, LanguageIds = null },
        new() { Code = "TyndaleBibleDictionary", LanguageCodes = ["eng"], LanguageIds = null },
        new() { Code = "TyndaleBibleDictionary", LanguageCodes = ["eng", "tpi"], LanguageIds = null },
        new() { Code = "TyndaleBibleDictionary", LanguageCodes = null, LanguageIds = [1] },
        new() { Code = "TyndaleBibleDictionary", LanguageCodes = null, LanguageIds = [1, 2] },
    ];

    [Theory]
    [MemberData(nameof(GetValidRequestData))]
    public void Validator_ValidRequestProperty_ShouldReturnValid(Request request)
    {
        _validator
            .TestValidate(request)
            .ShouldNotHaveAnyValidationErrors();
    }
}