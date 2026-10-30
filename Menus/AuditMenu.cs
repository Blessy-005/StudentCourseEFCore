using StudentCourseEFCore.Models;
using StudentCourseEFCore.Services;

namespace StudentCourseEFCore.Menus;

public class AuditMenu
{
    private readonly AuditService _service;
    public AuditMenu(AuditService service) => _service = service;

    public async Task RunAsync()
    {
        while (true)
        {
            Console.WriteLine("\n=== Audit Log ===");
            Console.WriteLine("1. View recent changes (last 20)");
            Console.WriteLine("2. View changes by entity");
            Console.WriteLine("0. Back");
            Console.Write("Choose: ");

            switch (Console.ReadLine()?.Trim())
            {
                case "1":
                    Print(await _service.GetRecentAsync(20));
                    break;
                case "2":
                    Console.Write("Entity (Student, Course, Department, Enrollment): ");
                    Print(await _service.GetByEntityAsync(Console.ReadLine() ?? ""));
                    break;
                case "0":
                    return;
                default:
                    Console.WriteLine("Invalid choice.");
                    break;
            }
        }
    }

    private static void Print(List<AuditLog> logs)
    {
        if (logs.Count == 0) { Console.WriteLine("No audit records found."); return; }

        foreach (var l in logs)
        {
            Console.WriteLine($"\n[{l.Timestamp.ToLocalTime():yyyy-MM-dd HH:mm:ss}] {l.Action,-8} {l.EntityName} ({l.EntityId}) by {l.ChangedBy}");
            if (l.OldValues != null) Console.WriteLine($"   Old: {l.OldValues}");
            if (l.NewValues != null) Console.WriteLine($"   New: {l.NewValues}");
        }
    }
}