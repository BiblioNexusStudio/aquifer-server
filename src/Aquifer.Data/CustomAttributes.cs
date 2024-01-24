namespace Aquifer.Data;

/// <summary>
///     Set a default value defined on the sql server
/// </summary>
/// <param name="value">Default value to apply</param>
[AttributeUsage(AttributeTargets.Property)]
public class SqlDefaultValueAttribute(string value) : Attribute
{
    /// <summary>
    ///     Default value to apply
    /// </summary>
    public string DefaultValue { get; set; } = value;
}