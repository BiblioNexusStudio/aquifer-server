using Aquifer.Data.Entities;

namespace Aquifer.API.Endpoints.Reports.Dynamic;

public static class ReportRoleHelper
{
    public static bool RoleIsAllowedForReport(string? allowedRoles, UserRole role)
    {
        return allowedRoles != null && Array.ConvertAll(allowedRoles.Split(','), int.Parse).Any(r => r == (int)role);
    }
}