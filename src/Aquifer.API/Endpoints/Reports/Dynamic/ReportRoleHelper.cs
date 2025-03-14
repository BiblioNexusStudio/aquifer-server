using Aquifer.Data.Entities;

namespace Aquifer.API.Endpoints.Reports.Dynamic;

public static class ReportRoleHelper
{
    public static bool RoleIsAllowedForReport(string? allowedRoles, UserRole userRole)
    {
        return allowedRoles != null && Array.ConvertAll(allowedRoles.Split(','), int.Parse).Contains((int)userRole);
    }
}