using Aquifer.Data.Entities;
using System.ComponentModel;

namespace Aquifer.Public.API.Endpoints.Resources.Collections.List;

public sealed class Request
{
    public const int MaximumLimit = 100;

    /// <summary>
    /// The optional type of resource to search for, such as "Dictionary". If none specified, will default to None.
    /// </summary>
    [DefaultValue(ResourceType.None)]
    public ResourceType ResourceType { get; init; }

    /// <summary>
    /// The number of results to return.
    /// </summary>
    [DefaultValue(MaximumLimit)]
    public int Limit { get; init; } = MaximumLimit;

    /// <summary>
    /// Records to skip if paging through results.
    /// </summary>
    [DefaultValue(0)]
    public int Offset { get; init; }
}