using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace Aquifer.Jobs.Managers;

/// <summary>
/// This base class is designed for use with a Manager using a single Timer Trigger Azure Function.
/// </summary>
/// <remarks>
/// Example usage:
/// <example>
/// <code>
/// public sealed class FooManager(ILogger&lt;FooManager&gt; _logger) : ManagerBase&lt;FooManager&gt;(_logger)
/// {
///     [Function(nameof(FooManager))]
///     [FixedDelayRetry(maxRetryCount: 1, delayInterval: _tenSecondDelayInterval)]
///     public override async Task RunAsync([TimerTrigger(CronSchedules.EveryTenMinutes)] TimerInfo timerInfo, CancellationToken ct)
///     {
///         await base.RunAsync(timerInfo, ct);
///     }
/// 
///     protected override async Task RunCoreAsync(CancellationToken cancellationToken)
///     {
///         Logger.LogInformation("Function implementation goes here");
///     }
/// }
/// </code>
/// </example>
/// </remarks>
[SuppressMessage("ReSharper", "StaticMemberInGenericType")]
public abstract class ManagerBase<T>(ILogger<T> _logger)
    where T : ManagerBase<T>
{
    private static readonly bool s_azureFunctionHasRetryPolicy;
    private static readonly string s_azureFunctionName;
    private static readonly string s_azureFunctionSchedule;

    protected ILogger<T> Logger { get; } = _logger;

    static ManagerBase()
    {
        var runMethod = typeof(T)
            .GetRuntimeMethod(
                nameof(RunAsync),
                typeof(ManagerBase<T>)
                    .GetMethod(nameof(RunAsync))
                    ?.GetParameters()
                    .Select(p => p.ParameterType)
                    .ToArray()
                    ?? [])
            ?? throw new InvalidOperationException($"Unable to find method {nameof(RunAsync)} in {typeof(T).FullName}.");
        var functionAttribute = runMethod.GetCustomAttribute<FunctionAttribute>()
            ?? throw new InvalidOperationException($"Method {nameof(RunAsync)} in {typeof(T).FullName} is missing a {nameof(FunctionAttribute)}.");
        s_azureFunctionName = functionAttribute.Name;

        var timerInfoParameter = runMethod
            .GetParameters()
            .SingleOrDefault(p => p.ParameterType == typeof(TimerInfo))
            ?? throw new InvalidOperationException($"Method {nameof(RunAsync)} in {typeof(T).FullName} is missing a parameter of type {nameof(TimerInfo)}.");

        var timerTriggerAttribute = timerInfoParameter
            .GetCustomAttribute<TimerTriggerAttribute>()
            ?? throw new InvalidOperationException($"Method {nameof(RunAsync)} in {typeof(T).FullName} is missing a {nameof(TimerTriggerAttribute)} on the \"{timerInfoParameter.Name}\" parameter.");

        s_azureFunctionSchedule = timerTriggerAttribute.Schedule;

        s_azureFunctionHasRetryPolicy =
            runMethod.GetCustomAttribute<ExponentialBackoffRetryAttribute>() is not null ||
            runMethod.GetCustomAttribute<FixedDelayRetryAttribute>() is not null;
    }

    /// <summary>
    /// Override this method in the derived class and use it as the primary Manager Azure Function method using function attributes.
    /// The main point in overriding is to attach the attributes to the method.
    /// This will provide consistent logging and exception handling.
    /// The derived type should simply call base.RunAsync() in the overriden method and then also override the RunCoreAsync() method for
    /// the actual implementation.
    /// </summary>
    /// <param name="timerInfo">The timer info for the next scheduled run.</param>
    /// <param name="ct">The cancellation token.</param>
    public virtual async Task RunAsync(
        TimerInfo timerInfo,
        CancellationToken ct)
    {
        Logger.LogInformation(
            "Timer Trigger function \"{FunctionName}\" was triggered at {Now} (IsPastDue: {IsPastDue}; Schedule: {Schedule}).",
            s_azureFunctionName,
            DateTime.UtcNow,
            timerInfo.IsPastDue,
            s_azureFunctionSchedule);

        try
        {
            await RunCoreAsync(ct);
        }
        catch (Exception ex)
        {
            Logger.LogError(
                ex, 
                "Timer Trigger function \"{FunctionName}\" encountered an error during execution (Function has retry policy: {HasRetryPolicy}).",
                s_azureFunctionName,
                s_azureFunctionHasRetryPolicy);
            throw;
        }

        Logger.LogInformation(
            "Timer trigger function \"{FunctionName}\" execution complete. Next run is scheduled for {NextRun}.",
            s_azureFunctionName,
            timerInfo.ScheduleStatus?.Next);
    }

    /// <summary>
    /// The actual implementation of the Azure Function.
    /// </summary>
    /// <param name="ct">The cancellation token.</param>
    /// <returns></returns>
    protected abstract Task RunCoreAsync(CancellationToken ct);

    protected static class Timings
    {
        public const string TenSecondDelayInterval = "00:00:10";
    }
}