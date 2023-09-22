namespace Aquifer.Data;

/// <summary>
///     Set a default value defined on the sql server
/// </summary>
[AttributeUsage(AttributeTargets.Property)]
public class SqlDefaultValueAttribute : Attribute
{
    /// <summary>
    ///     Set a default value defined on the sql server
    /// </summary>
    /// <param name="value">Default value to apply</param>
    public SqlDefaultValueAttribute(string value)
    {
        DefaultValue = value;
    }

    /// <summary>
    ///     Default value to apply
    /// </summary>
    public string DefaultValue { get; set; }
}