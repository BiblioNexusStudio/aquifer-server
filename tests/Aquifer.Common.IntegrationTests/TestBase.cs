namespace Aquifer.Common.IntegrationTests;

/// <summary>
/// This class allows for injecting a <typeparamref name="TAppFixture" /> into the constructor of a test class by inheriting from
/// this base class.
/// Example:
/// <example>
///     <code>
/// public sealed class FooTests(App _app) : TestBase&lt;App&gt; { }
/// </code>
/// </example>
/// </summary>
/// <typeparam name="TAppFixture"></typeparam>
public abstract class TestBase<TAppFixture> : IClassFixture<TAppFixture> where TAppFixture : class
{
}