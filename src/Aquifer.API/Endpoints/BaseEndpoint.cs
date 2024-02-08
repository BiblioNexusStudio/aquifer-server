using System.Linq.Expressions;
using FastEndpoints;

namespace Aquifer.API.Endpoints;

public abstract class BaseEndpoint<TRequest, TResponse> : Endpoint<TRequest, TResponse>
    where TRequest : class
    where TResponse : class
{
    protected T? AddEntityNotFoundError<T>(Expression<Func<TRequest, object?>> property)
    {
        AddError(property, "No record found.");
        return default;
    }
}