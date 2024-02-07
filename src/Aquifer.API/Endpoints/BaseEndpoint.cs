using System.Linq.Expressions;
using FastEndpoints;

public abstract class BaseEndpoint<TRequest, TResponse> : Endpoint<TRequest, TResponse>
    where TRequest : class
    where TResponse : class
{
    protected T? AddEntityNotFoundError<T>(Expression<Func<TRequest, object?>> property)
    {
        AddError(property, "No record found by {PropertyName}.");
        return default;
    }
}