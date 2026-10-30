using Microsoft.EntityFrameworkCore;
using StudentCourseEFCore.Data;
using StudentCourseEFCore.Models;

namespace StudentCourseEFCore.Repositories;

public class AuditRepository
{
    private readonly AppDbContext _db;
    public AuditRepository(AppDbContext db) => _db = db;

    public Task<List<AuditLog>> GetRecentAsync(int count) =>
        _db.AuditLogs.AsNoTracking()
           .OrderByDescending(a => a.Id)
           .Take(count)
           .ToListAsync();

    public Task<List<AuditLog>> GetByEntityAsync(string entityName) =>
        _db.AuditLogs.AsNoTracking()
           .Where(a => a.EntityName == entityName)
           .OrderByDescending(a => a.Id)
           .ToListAsync();
}