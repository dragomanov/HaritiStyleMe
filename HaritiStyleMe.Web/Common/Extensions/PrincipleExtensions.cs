namespace HaritiStyleMe.Web.Common.Extensions
{
    using System.Security.Principal;

    public static class PrincipalExtensions
    {
        public static bool IsUser(this IPrincipal principal)
        {
            return principal.Identity.IsAuthenticated;
        }

        public static bool IsEmployee(this IPrincipal principal)
        {
            return principal.IsInRole(RolesConfig.employeeRole);
        }

        public static bool IsAdmin(this IPrincipal principal)
        {
            return principal.IsInRole(RolesConfig.adminRole);
        }
    }
}