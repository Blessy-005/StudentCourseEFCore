# Audit Logging

Every insert, update and delete made through `AppDbContext` is recorded in the `AuditLogs` table.

| Column | Meaning |
|---|---|
| EntityName | Entity type, e.g. Student |
| EntityId | Primary key, e.g. Id=3 |
| Action | Added, Modified or Deleted |
| OldValues / NewValues | JSON of the values; updates store only changed fields |
| ChangedBy | `AuditContext.CurrentUser` (defaults to the Windows user) |
| Timestamp | UTC time of the change |

## How it works
`AppDbContext.SaveChanges/SaveChangesAsync` inspects the change tracker before saving,
then writes audit rows after the save so inserted records have their real keys.
`AuditLog` itself is never audited. New entities are audited automatically.

## Viewing
Main menu > Audit Log: recent changes, or filter by entity.