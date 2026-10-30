using StudentCourseEFCore.Models;
using StudentCourseEFCore.Repositories;

namespace StudentCourseEFCore.Services;

public class AuditService
{
    private readonly AuditRepository _repository;
    public AuditService(AuditRepository repository) => _repository = repository;

    public Task<List<AuditLog>> GetRecentAsync(int count = 20) => _repository.GetRecentAsync(count);

    public Task<List<AuditLog>> GetByEntityAsync(string entityName) =>
        _repository.GetByEntityAsync(entityName.Trim());
}