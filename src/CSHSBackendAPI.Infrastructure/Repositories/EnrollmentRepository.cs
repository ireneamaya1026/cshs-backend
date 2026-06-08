using CSHSBackendAPI.Application.Common.Interfaces.Repositories;
using CSHSBackendAPI.Domain.Entities;
using CSHSBackendAPI.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace CSHSBackendAPI.Infrastructure.Repositories;

public class EnrollmentRepository : IEnrollmentRepository
{
    private readonly AppDbContext _context;

    public EnrollmentRepository(AppDbContext context) => _context = context;

    public async Task<(IEnumerable<Enrollment> Items, int Total)> GetAllAsync(
        string? schoolYear, string? currentStep,
        long? campusId, string? department,
        int page, int limit)
    {
        var query = _context.Enrollments
            .Include(e => e.Campus)
            .AsQueryable();

        if (!string.IsNullOrEmpty(schoolYear))
            query = query.Where(e => e.SchoolYear == schoolYear);

        if (!string.IsNullOrEmpty(currentStep))
            query = query.Where(e => e.CurrentStep == currentStep);

        if (campusId.HasValue)
            query = query.Where(e => e.CampusId == campusId);

        if (!string.IsNullOrEmpty(department))
            query = query.Where(e => e.Department.ToString() == department);

        var total = await query.CountAsync();

        var items = await query
            .OrderByDescending(e => e.CreatedAt)
            .Skip((page - 1) * limit)
            .Take(limit)
            .ToListAsync();

        return (items, total);
    }

    public async Task<Enrollment?> GetByIdAsync(long id) =>
        await _context.Enrollments
            .Include(e => e.Campus)
            .Include(e => e.StageHistories.OrderBy(h => h.RecordedAt))
            .FirstOrDefaultAsync(e => e.Id == id);

    public async Task<string> GenerateReferenceNoAsync(string schoolYear)
    {
        var year = schoolYear.Replace("-", "");
        var count = await _context.Enrollments
            .CountAsync(e => e.SchoolYear == schoolYear);
        return $"ENROLL-{year}-{(count + 1):D4}";
    }

    public async Task<Enrollment> CreateAsync(Enrollment enrollment)
    {
        _context.Enrollments.Add(enrollment);
        await _context.SaveChangesAsync();
        return enrollment;
    }

    public async Task UpdateAsync(Enrollment enrollment)
    {
        _context.Enrollments.Update(enrollment);
        await _context.SaveChangesAsync();
    }

    public async Task AddStageHistoryAsync(EnrollmentStageHistory history)
    {
        // Append only — never update or delete
        _context.EnrollmentStageHistories.Add(history);
        await _context.SaveChangesAsync();
    }
}