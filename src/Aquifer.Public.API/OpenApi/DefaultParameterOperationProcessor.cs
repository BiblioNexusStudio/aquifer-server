using System.ComponentModel;
using System.Reflection;
using NSwag.Generation.AspNetCore;
using NSwag.Generation.Processors;
using NSwag.Generation.Processors.Contexts;

namespace Aquifer.Public.API.OpenApi;

public class DefaultParameterOperationProcessor : IOperationProcessor
{
    public bool Process(OperationProcessorContext context)
    {
        var dtoType = ((AspNetCoreOperationProcessorContext)context).ApiDescription.ParameterDescriptions
            .FirstOrDefault()?.Type;

        if (dtoType is null)
        {
            return true;
        }

        foreach (var parameter in context.OperationDescription.Operation.Parameters)
        {
            var property = dtoType.GetProperty(parameter.Name,
                BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
            if (HasDefaultValue(property))
            {
                parameter.IsRequired = false;
            }
        }

        return true;
    }

    private static bool HasDefaultValue(PropertyInfo? property)
    {
        return property?.GetCustomAttributes(typeof(DefaultValueAttribute), false)
            .FirstOrDefault() is DefaultValueAttribute;
    }
}