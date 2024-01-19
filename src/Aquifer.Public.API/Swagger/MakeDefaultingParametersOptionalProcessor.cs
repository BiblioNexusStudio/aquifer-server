using System.ComponentModel;
using System.Reflection;
using NSwag.Generation.AspNetCore;
using NSwag.Generation.Processors;
using NSwag.Generation.Processors.Contexts;

public class MakeDefaultingParametersOptionalProcessor : IOperationProcessor
{
    public bool Process(OperationProcessorContext context)
    {
        var dtoType = ((AspNetCoreOperationProcessorContext)context).ApiDescription.ParameterDescriptions.FirstOrDefault()?.Type;

        if (dtoType is not null)
        {
            foreach (var parameter in context.OperationDescription.Operation.Parameters)
            {
                var property = dtoType.GetProperty(parameter.Name, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);

                if (property != null && CheckHasDefaultValue(property))
                {
                    parameter.IsRequired = false;
                }
            }
        }
        return true;
    }

    private bool CheckHasDefaultValue(PropertyInfo property)
    {
        var attribute = property.GetCustomAttributes(typeof(DefaultValueAttribute), false)
            .FirstOrDefault() as DefaultValueAttribute;

        return attribute != null;
    }
}
