using Aquifer.Data.Entities;
using Aquifer.Public.API.Endpoints.Resources.Collections.List;
using FluentValidation.TestHelper;

namespace Aquifer.Public.API.UnitTests.Endpoints.Resources.Collections.List;

public sealed class ValidatorTests
{
    private readonly Validator _validator = new();

    public static TheoryData<Request, string> GetInvalidRequestData => new()
    {
        {
            new Request
            {
                ResourceType = (ResourceType)int.MaxValue,
                Offset = 0,
                Limit = 1,
            },
            nameof(Request.ResourceType)
        },
        {
            new Request
            {
                ResourceType = ResourceType.None,
                Offset = -1,
                Limit = 1,
            },
            nameof(Request.Offset)
        },
        {
            new Request
            {
                ResourceType = ResourceType.None,
                Offset = 0,
                Limit = 0,
            },
            nameof(Request.Limit)
        },
        {
            new Request
            {
                ResourceType = ResourceType.None,
                Offset = 0,
                Limit = 101,
            },
            nameof(Request.Limit)
        },
    };

    public static TheoryData<Request> GetValidRequestData =>
    [
        new Request
        {
            ResourceType = ResourceType.None,
            Offset = 0,
            Limit = 1,
        },
        new Request
        {
            ResourceType = ResourceType.None,
            Offset = 0,
            Limit = 100,
        },
        new Request
        {
            ResourceType = ResourceType.None,
            Offset = int.MaxValue,
            Limit = 1,
        },
        new Request
        {
            ResourceType = ResourceType.Dictionary,
            Offset = int.MaxValue,
            Limit = 1,
        },
    ];

    [Theory]
    [MemberData(nameof(GetInvalidRequestData))]
    public void Validator_InvalidRequestProperty_ShouldReturnValidationError(Request request, string expectedInvalidPropertyName)
    {
        _validator
            .TestValidate(request)
            .ShouldHaveValidationErrorFor(expectedInvalidPropertyName)
            .Only();
    }

    [Theory]
    [MemberData(nameof(GetValidRequestData))]
    public void Validator_ValidRequestProperty_ShouldReturnValid(Request request)
    {
        _validator
            .TestValidate(request)
            .ShouldNotHaveAnyValidationErrors();
    }
}