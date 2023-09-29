using Aquifer.Data.Entities;

namespace Aquifer.API.Common;

public static class Constants
{
    // this represents the resource types that we allow to be accessed as the starting point when viewing content
    public static readonly ResourceEntityType[] RootResourceTypes = { ResourceEntityType.CBBTER };
}