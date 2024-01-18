﻿using NJsonSchema;
using NJsonSchema.Generation;

namespace Aquifer.Public.API.OpenApi;

public class EnumSchemaProcessor : ISchemaProcessor
{
    public void Process(SchemaProcessorContext context)
    {
        if (!context.ContextualType.Type.IsEnum) return;

        context.Schema.Enumeration.Clear();
        context.Schema.Type = JsonObjectType.String;
        context.Schema.Format = null;
        Enum.GetNames(context.ContextualType.Type)
            .ToList()
            .ForEach(name => context.Schema.Enumeration.Add(name));
    }
}