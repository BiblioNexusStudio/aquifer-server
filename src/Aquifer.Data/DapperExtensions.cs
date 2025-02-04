using System.ComponentModel;
using System.Data;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore.SqlServer.Storage.Internal;
using Microsoft.Extensions.Logging;
using Polly;
using Polly.Contrib.WaitAndRetry;
using Polly.Retry;

namespace Aquifer.Data;

/// <summary>
/// Recreates Dapper extension methods with a built-in retry policy for transient errors and also
/// provides a simplified interface for using cancellation tokens without <see cref="CommandDefinition"/>.
/// Methods are ordered according to the original source: https://github.com/DapperLib/Dapper/blob/main/Dapper/SqlMapper.Async.cs
/// Note that some less common Dapper extension methods are not yet reimplemented here.
/// </summary>
public static class DapperExtensions
{
    private static readonly ILogger s_logger = StaticLoggerFactory.CreateLogger(typeof(DapperExtensions));

    /// <summary>
    /// Retry up to two times but only for transient failures with logging on retry.
    /// </summary>
    private static readonly AsyncRetryPolicy s_retryPolicy = Policy
#pragma warning disable EF1001 // Internal EF Core API usage.
        .Handle<SqlException>(SqlServerTransientExceptionDetector.ShouldRetryOn) // use the transient exception detector from EF Core
        .Or<TimeoutException>()
        .OrInner<Win32Exception>(SqlServerTransientExceptionDetector.ShouldRetryOn)
#pragma warning restore EF1001 // Internal EF Core API usage.
        .WaitAndRetryAsync(
            Backoff.DecorrelatedJitterBackoffV2(medianFirstRetryDelay: TimeSpan.FromMilliseconds(50), retryCount: 3),
            LogRetry);

    /// <summary>
    /// Execute a query asynchronously using Task.
    /// </summary>
    /// <typeparam name="T">The type of results to return.</typeparam>
    /// <param name="cnn">The connection to query on.</param>
    /// <param name="sql">The SQL to execute for the query.</param>
    /// <param name="param">The parameters to pass, if any.</param>
    /// <param name="transaction">The transaction to use, if any.</param>
    /// <param name="commandTimeout">The command timeout (in seconds).</param>
    /// <param name="commandType">The type of command to execute.</param>
    /// <param name="flags">The behavior flags for this command (defaults to <see cref="CommandFlags.Buffered"/>.</param>
    /// <param name="cancellationToken">The cancellation token, if any.</param>
    /// <returns>
    /// A sequence of data of <typeparamref name="T"/>; if a basic type (int, string, etc) is queried then the data from the first column is assumed, otherwise an instance is
    /// created per row, and a direct column-name===member-name mapping is assumed (case insensitive).
    /// </returns>
    public static async Task<IEnumerable<T>> QueryWithRetriesAsync<T>(
        this IDbConnection cnn,
        string sql,
        object? param = null,
        IDbTransaction? transaction = null,
        int? commandTimeout = null,
        CommandType? commandType = null,
        CommandFlags flags = CommandFlags.Buffered,
        CancellationToken cancellationToken = default)
    {
        return await s_retryPolicy.ExecuteAsync(async () => await cnn.QueryAsync<T>(
            new CommandDefinition(sql, param, transaction, commandTimeout, commandType, flags, cancellationToken)));
    }

    /// <summary>
    /// Execute a single-row query asynchronously using Task.
    /// </summary>
    /// <typeparam name="T">The type of result to return.</typeparam>
    /// <param name="cnn">The connection to query on.</param>
    /// <param name="sql">The SQL to execute for the query.</param>
    /// <param name="param">The parameters to pass, if any.</param>
    /// <param name="transaction">The transaction to use, if any.</param>
    /// <param name="commandTimeout">The command timeout (in seconds).</param>
    /// <param name="commandType">The type of command to execute.</param>
    /// <param name="flags">The behavior flags for this command (defaults to <see cref="CommandFlags.None"/>.</param>
    /// <param name="cancellationToken">The cancellation token, if any.</param>
    public static async Task<T> QueryFirstWithRetriesAsync<T>(
        this IDbConnection cnn,
        string sql,
        object? param = null,
        IDbTransaction? transaction = null,
        int? commandTimeout = null,
        CommandType? commandType = null,
        CommandFlags flags = CommandFlags.None,
        CancellationToken cancellationToken = default)
    {
        return await s_retryPolicy.ExecuteAsync(async () => await cnn.QueryFirstAsync<T>(
            new CommandDefinition(sql, param, transaction, commandTimeout, commandType, flags, cancellationToken)));
    }

    /// <summary>
    /// Execute a single-row query asynchronously using Task.
    /// </summary>
    /// <typeparam name="T">The type of result to return.</typeparam>
    /// <param name="cnn">The connection to query on.</param>
    /// <param name="sql">The SQL to execute for the query.</param>
    /// <param name="param">The parameters to pass, if any.</param>
    /// <param name="transaction">The transaction to use, if any.</param>
    /// <param name="commandTimeout">The command timeout (in seconds).</param>
    /// <param name="commandType">The type of command to execute.</param>
    /// <param name="flags">The behavior flags for this command (defaults to <see cref="CommandFlags.None"/>.</param>
    /// <param name="cancellationToken">The cancellation token, if any.</param>
    public static async Task<T?> QueryFirstOrDefaultWithRetriesAsync<T>(
        this IDbConnection cnn,
        string sql,
        object? param = null,
        IDbTransaction? transaction = null,
        int? commandTimeout = null,
        CommandType? commandType = null,
        CommandFlags flags = CommandFlags.None,
        CancellationToken cancellationToken = default)
    {
        return await s_retryPolicy.ExecuteAsync(async () => await cnn.QueryFirstOrDefaultAsync<T>(
            new CommandDefinition(sql, param, transaction, commandTimeout, commandType, flags, cancellationToken)));
    }

    /// <summary>
    /// Execute a single-row query asynchronously using Task.
    /// </summary>
    /// <typeparam name="T">The type of result to return.</typeparam>
    /// <param name="cnn">The connection to query on.</param>
    /// <param name="sql">The SQL to execute for the query.</param>
    /// <param name="param">The parameters to pass, if any.</param>
    /// <param name="transaction">The transaction to use, if any.</param>
    /// <param name="commandTimeout">The command timeout (in seconds).</param>
    /// <param name="commandType">The type of command to execute.</param>
    /// <param name="flags">The behavior flags for this command (defaults to <see cref="CommandFlags.None"/>.</param>
    /// <param name="cancellationToken">The cancellation token, if any.</param>
    public static async Task<T> QuerySingleWithRetriesAsync<T>(
        this IDbConnection cnn,
        string sql,
        object? param = null,
        IDbTransaction? transaction = null,
        int? commandTimeout = null,
        CommandType? commandType = null,
        CommandFlags flags = CommandFlags.None,
        CancellationToken cancellationToken = default)
    {
        return await s_retryPolicy.ExecuteAsync(async () => await cnn.QuerySingleAsync<T>(
            new CommandDefinition(sql, param, transaction, commandTimeout, commandType, flags, cancellationToken)));
    }

    /// <summary>
    /// Execute a single-row query asynchronously using Task.
    /// </summary>
    /// <typeparam name="T">The type to return.</typeparam>
    /// <param name="cnn">The connection to query on.</param>
    /// <param name="sql">The SQL to execute for the query.</param>
    /// <param name="param">The parameters to pass, if any.</param>
    /// <param name="transaction">The transaction to use, if any.</param>
    /// <param name="commandTimeout">The command timeout (in seconds).</param>
    /// <param name="commandType">The type of command to execute.</param>
    /// <param name="flags">The behavior flags for this command (defaults to <see cref="CommandFlags.None"/>.</param>
    /// <param name="cancellationToken">The cancellation token, if any.</param>
    public static async Task<T?> QuerySingleOrDefaultWithRetriesAsync<T>(
        this IDbConnection cnn,
        string sql,
        object? param = null,
        IDbTransaction? transaction = null,
        int? commandTimeout = null,
        CommandType? commandType = null,
        CommandFlags flags = CommandFlags.None,
        CancellationToken cancellationToken = default)
    {
        return await s_retryPolicy.ExecuteAsync(async () => await cnn.QuerySingleOrDefaultAsync<T?>(
            new CommandDefinition(sql, param, transaction, commandTimeout, commandType, flags, cancellationToken)));
    }

    /// <summary>
    /// Execute a command asynchronously using Task.
    /// </summary>
    /// <param name="cnn">The connection to query on.</param>
    /// <param name="sql">The SQL to execute for this query.</param>
    /// <param name="param">The parameters to use for this query.</param>
    /// <param name="transaction">The transaction to use for this query.</param>
    /// <param name="commandTimeout">Number of seconds before command execution timeout.</param>
    /// <param name="commandType">Is it a stored proc or a batch?</param>
    /// <param name="flags">The behavior flags for this command (defaults to <see cref="CommandFlags.Buffered"/>.</param>
    /// <param name="cancellationToken">The cancellation token, if any.</param>
    /// <returns>The number of rows affected.</returns>
    public static async Task<int> ExecuteWithRetriesAsync(
        this IDbConnection cnn,
        string sql,
        object? param = null,
        IDbTransaction? transaction = null,
        int? commandTimeout = null,
        CommandType? commandType = null,
        CommandFlags flags = CommandFlags.Buffered,
        CancellationToken cancellationToken = default)
    {
        return await s_retryPolicy.ExecuteAsync(async () => await cnn.ExecuteAsync(
            new CommandDefinition(sql, param, transaction, commandTimeout, commandType, flags, cancellationToken)));
    }

    /// <summary>
    /// Execute a command that returns multiple result sets, and access each in turn.
    /// </summary>
    /// <param name="cnn">The connection to query on.</param>
    /// <param name="sql">The SQL to execute for this query.</param>
    /// <param name="param">The parameters to use for this query.</param>
    /// <param name="transaction">The transaction to use for this query.</param>
    /// <param name="commandTimeout">Number of seconds before command execution timeout.</param>
    /// <param name="commandType">Is it a stored proc or a batch?</param>
    /// <param name="flags">The behavior flags for this command (defaults to <see cref="CommandFlags.Buffered"/>.</param>
    /// <param name="cancellationToken">The cancellation token, if any.</param>
    public static async Task<SqlMapper.GridReader> QueryMultipleWithRetriesAsync(
        this IDbConnection cnn,
        string sql,
        object? param = null,
        IDbTransaction? transaction = null,
        int? commandTimeout = null,
        CommandType? commandType = null,
        CommandFlags flags = CommandFlags.Buffered,
        CancellationToken cancellationToken = default)
    {
        return await s_retryPolicy.ExecuteAsync(async () => await cnn.QueryMultipleAsync(
            new CommandDefinition(sql, param, transaction, commandTimeout, commandType, flags, cancellationToken)));
    }

    /// <summary>
    /// Execute parameterized SQL that selects a single value.
    /// </summary>
    /// <typeparam name="T">The type to return.</typeparam>
    /// <param name="cnn">The connection to execute on.</param>
    /// <param name="sql">The SQL to execute.</param>
    /// <param name="param">The parameters to use for this command.</param>
    /// <param name="transaction">The transaction to use for this command.</param>
    /// <param name="commandTimeout">Number of seconds before command execution timeout.</param>
    /// <param name="commandType">Is it a stored proc or a batch?</param>
    /// <param name="flags">The behavior flags for this command (defaults to <see cref="CommandFlags.Buffered"/>.</param>
    /// <param name="cancellationToken">The cancellation token, if any.</param>
    /// <returns>The first cell returned, as <typeparamref name="T"/>.</returns>
    public static async Task<T?> ExecuteScalarWithRetriesAsync<T>(
        this IDbConnection cnn,
        string sql,
        object? param = null,
        IDbTransaction? transaction = null,
        int? commandTimeout = null,
        CommandType? commandType = null,
        CommandFlags flags = CommandFlags.Buffered,
        CancellationToken cancellationToken = default)
    {
        return await s_retryPolicy.ExecuteAsync(async () => await cnn.ExecuteScalarAsync<T>(
            new CommandDefinition(sql, param, transaction, commandTimeout, commandType, flags, cancellationToken)));
    }

    public static async Task<IEnumerable<TReturn>> QueryWithRetriesAsync<TFirst, TSecond, TReturn>(
        this IDbConnection cnn,
        string sql,
        Func<TFirst, TSecond, TReturn> map,
        object? param = null,
        IDbTransaction? transaction = null,
        bool buffered = true,
        string splitOn = "Id",
        int? commandTimeout = null,
        CommandType? commandType = null,
        CancellationToken cancellationToken = default)
    {
        return await s_retryPolicy.ExecuteAsync(async () => await cnn.QueryAsync(
            new CommandDefinition(
                sql,
                param,
                transaction,
                commandTimeout,
                commandType,
                buffered
                    ? CommandFlags.Buffered
                    : CommandFlags.None,
                cancellationToken),
            map,
            splitOn));
    }

    public static async Task<IEnumerable<TReturn>> QueryWithRetriesAsync<TFirst, TSecond, TThird, TReturn>(
        this IDbConnection cnn,
        string sql,
        Func<TFirst, TSecond, TThird, TReturn> map,
        object? param = null,
        IDbTransaction? transaction = null,
        bool buffered = true,
        string splitOn = "Id",
        int? commandTimeout = null,
        CommandType? commandType = null,
        CancellationToken cancellationToken = default)
    {
        return await s_retryPolicy.ExecuteAsync(async () => await cnn.QueryAsync(
            new CommandDefinition(
                sql,
                param,
                transaction,
                commandTimeout,
                commandType,
                buffered
                    ? CommandFlags.Buffered
                    : CommandFlags.None,
                cancellationToken),
            map,
            splitOn));
    }

    public static async Task<IEnumerable<TReturn>> QueryWithRetriesAsync<TFirst, TSecond, TThird, TFourth, TReturn>(
        this IDbConnection cnn,
        string sql,
        Func<TFirst, TSecond, TThird, TFourth, TReturn> map,
        object? param = null,
        IDbTransaction? transaction = null,
        bool buffered = true,
        string splitOn = "Id",
        int? commandTimeout = null,
        CommandType? commandType = null,
        CancellationToken cancellationToken = default)
    {
        return await s_retryPolicy.ExecuteAsync(async () => await cnn.QueryAsync(
            new CommandDefinition(
                sql,
                param,
                transaction,
                commandTimeout,
                commandType,
                buffered
                    ? CommandFlags.Buffered
                    : CommandFlags.None,
                cancellationToken),
            map,
            splitOn));
    }

    public static async Task<IEnumerable<TReturn>> QueryWithRetriesAsync<TFirst, TSecond, TThird, TFourth, TFifth, TReturn>(
        this IDbConnection cnn,
        string sql,
        Func<TFirst, TSecond, TThird, TFourth, TFifth, TReturn> map,
        object? param = null,
        IDbTransaction? transaction = null,
        bool buffered = true,
        string splitOn = "Id",
        int? commandTimeout = null,
        CommandType? commandType = null,
        CancellationToken cancellationToken = default)
    {
        return await s_retryPolicy.ExecuteAsync(async () => await cnn.QueryAsync(
            new CommandDefinition(
                sql,
                param,
                transaction,
                commandTimeout,
                commandType,
                buffered
                    ? CommandFlags.Buffered
                    : CommandFlags.None,
                cancellationToken),
            map,
            splitOn));
    }

    public static async Task<IEnumerable<TReturn>> QueryWithRetriesAsync<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TReturn>(
        this IDbConnection cnn,
        string sql,
        Func<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TReturn> map,
        object? param = null,
        IDbTransaction? transaction = null,
        bool buffered = true,
        string splitOn = "Id",
        int? commandTimeout = null,
        CommandType? commandType = null,
        CancellationToken cancellationToken = default)
    {
        return await s_retryPolicy.ExecuteAsync(async () => await cnn.QueryAsync(
            new CommandDefinition(
                sql,
                param,
                transaction,
                commandTimeout,
                commandType,
                buffered
                    ? CommandFlags.Buffered
                    : CommandFlags.None,
                cancellationToken),
            map,
            splitOn));
    }

    public static async Task<IEnumerable<TReturn>> QueryWithRetriesAsync<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TSeventh, TReturn>(
        this IDbConnection cnn,
        string sql,
        Func<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TSeventh, TReturn> map,
        object? param = null,
        IDbTransaction? transaction = null,
        bool buffered = true,
        string splitOn = "Id",
        int? commandTimeout = null,
        CommandType? commandType = null,
        CancellationToken cancellationToken = default)
    {
        return await s_retryPolicy.ExecuteAsync(async () => await cnn.QueryAsync(
            new CommandDefinition(
                sql,
                param,
                transaction,
                commandTimeout,
                commandType,
                buffered
                    ? CommandFlags.Buffered
                    : CommandFlags.None,
                cancellationToken),
            map,
            splitOn));
    }

    private static void LogRetry(Exception exception, TimeSpan retryAfter, int retryCount, Context context)
    {
        s_logger.LogWarning(exception, "Gracefully handled a transient error during a Dapper DB operation. Retry after: {retryAfter}. Retry attempt: {retryCount}.", retryAfter, retryCount);
    }
}
