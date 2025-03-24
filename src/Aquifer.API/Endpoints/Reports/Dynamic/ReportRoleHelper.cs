namespace Aquifer.API.Endpoints.Reports.Dynamic;

public static class ReportRoleHelper
{
    public static bool RoleIsAllowedForReport(string? allowedRoles, List<string> roles)
    {
        return allowedRoles != null && allowedRoles.Split(',').Any(r => roles.Contains(r.ToString()));
    }
}