namespace StudentCourseEFCore.Helper;

public static class AuditContext
{
    // Defaults to the Windows user. Set it after login if your app has users.
    public static string CurrentUser { get; set; } = Environment.UserName;
}