using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;
using FastEndpoints;

namespace Aquifer.API.Endpoints;

public abstract class BaseEndpoint<TRequest, TResponse> : Endpoint<TRequest, TResponse>
    where TRequest : class
    where TResponse : class
{
    [DoesNotReturn]
    protected void ThrowEntityNotFoundError(Expression<Func<TRequest, object?>> property)
    {
        ThrowError(property, "No record found.");
    }
}