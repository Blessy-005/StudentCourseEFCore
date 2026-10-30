using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using StudentCourseEFCore.Helper;
using StudentCourseEFCore.Models;

namespace StudentCourseEFCore.Data;

internal class AuditEntry
{
    public AuditEntry(EntityEntry entry) => Entry = entry;

    public EntityEntry Entry { get; }
    public string EntityName { get; set; } = string.Empty;
    public string Action { get; set; } = string.Empty;
    public string? KeyText { get; set; }
    public Dictionary<string, object?> OldValues { get; } = new();
    public Dictionary<string, object?> NewValues { get; } = new();

    public bool HasChanges => OldValues.Count > 0 || NewValues.Count > 0;

    public string ComputeKey() => string.Join(", ",
        Entry.Properties.Where(p => p.Metadata.IsPrimaryKey())
                        .Select(p => $"{p.Metadata.Name}={p.CurrentValue}"));

    public AuditLog ToAuditLog() => new()
    {
        EntityName = EntityName,
        EntityId = KeyText ?? ComputeKey(),   // inserts get their real key after save
        Action = Action,
        OldValues = OldValues.Count == 0 ? null : JsonSerializer.Serialize(OldValues),
        NewValues = NewValues.Count == 0 ? null : JsonSerializer.Serialize(NewValues),
        ChangedBy = AuditContext.CurrentUser,
        Timestamp = DateTime.UtcNow
    };
}