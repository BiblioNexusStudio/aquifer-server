using FastEndpoints;

namespace Aquifer.API.Endpoints.Users;

public class Test : EndpointWithoutRequest<TestModel>
{
    public override void Configure()
    {
        Post("/users/test", "/admin/users/tests");
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        await SendAsync(new TestModel(), 200, ct);
    }
}

public class TestModel
{
    public string Value { get; set; } = "Hello world";
}