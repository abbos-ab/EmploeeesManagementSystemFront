namespace EmployesManagementSystemFront.Authorization
{
    public static class Roles
    {
        public const string SuperAdmin = "SuperAdmin";
        public const string Admin = "Admin";
        public const string User = "User";
    }

    public static class Policies
    {
        public const string RequireSuperAdminRole = "RequireSuperAdminRole";
        public const string RequireAdminRole = "RequireAdminRole";
        public const string RequireUserRole = "RequireUserRole";
    }
}
