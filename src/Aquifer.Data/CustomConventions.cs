﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Aquifer.Data;

public static class SqlDefaultValueAttributeConvention
{
    public static void Apply(ModelBuilder builder)
    {
        ConventionBehaviors
            .SetSqlValueForPropertiesWithAttribute<SqlDefaultValueAttribute>(builder, x => x.DefaultValue);
    }
}

public static class ConventionBehaviors
{
    public static void SetSqlValueForPropertiesWithAttribute<TAttribute>(
        ModelBuilder builder,
        Func<TAttribute, string> lambda) where TAttribute : class
    {
        SetPropertyValue<TAttribute>(builder).ForEach(x => x.Item1.SetDefaultValueSql(lambda(x.Item2)));
    }

    private static List<Tuple<IMutableProperty, TAttribute>> SetPropertyValue<TAttribute>(ModelBuilder builder)
        where TAttribute : class
    {
        var propsToModify = new List<Tuple<IMutableProperty, TAttribute>>();
        foreach (var entity in builder.Model.GetEntityTypes())
        {
            var properties = entity.GetProperties();
            foreach (var property in properties)
            {
                if (property.PropertyInfo
                        ?
                        .GetCustomAttributes(typeof(TAttribute), false)
                        .FirstOrDefault() is TAttribute attribute)
                {
                    propsToModify.Add(new Tuple<IMutableProperty, TAttribute>(property, attribute));
                }
            }
        }

        return propsToModify;
    }
}