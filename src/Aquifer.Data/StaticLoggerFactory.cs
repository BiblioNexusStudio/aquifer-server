using Microsoft.Extensions.Logging;

namespace Aquifer.Data;

/// <summary>
/// Use only for logging in a static context where Dependency Injection is not possible.
/// For non-static inject an <see cref="ILogger" />.
/// </summary>
/// <example>
/// public static class ExampleClass
/// {
/// private static readonly ILogger s_logger = StaticLoggerFactory.CreateLogger(typeof(ExampleClass));
/// }
/// </example>
public static class StaticLoggerFactory
{
    private static ILoggerFactory? s_loggerFactory;

    public static ILoggerFactory LoggerFactory
    {
        private get => s_loggerFactory ??
            throw new InvalidOperationException($"{nameof(LoggerFactory)} must be initialized on app startup before accessing.");
        set => s_loggerFactory = value;
    }

    public static ILogger CreateLogger<T>()
    {
        return LoggerFactory.CreateLogger<T>();
    }

    public static ILogger CreateLogger(string categoryName)
    {
        return LoggerFactory.CreateLogger(categoryName);
    }

    public static ILogger CreateLogger(Type type)
    {
        return LoggerFactory.CreateLogger(type);
    }
}