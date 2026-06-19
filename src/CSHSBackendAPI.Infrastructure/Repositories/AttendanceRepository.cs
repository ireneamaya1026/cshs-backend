using CSHSBackendAPI.Application.Common.Interfaces.Repositories;
using CSHSBackendAPI.Domain.Entities;
using CSHSBackendAPI.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace CSHSBackendAPI.Infrastructure.Repositories;

public class AttendanceRepository : IAttendanceRepository
{
    private readonly AppDbContext _context;

    public AttendanceRepository(AppDbContext context) => _context = context;

    public async Task<IEnumerable<AttendanceRecord>> GetAsync(
        long? subjectLoadId, DateOnly? date, string? section)
    {
        var query = _context.AttendanceRecords.AsQueryable();

        if (subjectLoadId.HasValue)
            query = query.Where(a => a.SubjectLoadId == subjectLoadId);

        if (date.HasValue)
            query = query.Where(a => a.Date == date.Value);

        if (!string.IsNullOrEmpty(section))
            query = query.Where(a => a.Section == section);

        return await query.OrderBy(a => a.StudentId).ToListAsync();
    }

    public async Task<IEnumerable<AttendanceRecord>> GetByStudentAsync(
        long studentId, string schoolYear) =>
        await _context.AttendanceRecords
            .Where(a => a.StudentId == studentId && a.SchoolYear == schoolYear)
            .OrderBy(a => a.Date)
            .ToListAsync();

    public async Task<AttendanceRecord?> GetExistingAsync(
        long studentId, DateOnly date, long? subjectLoadId) =>
        await _context.AttendanceRecords
            .FirstOrDefaultAsync(a =>
                a.StudentId == studentId &&
                a.Date == date &&
                a.SubjectLoadId == subjectLoadId);

    public async Task<AttendanceRecord> CreateAsync(AttendanceRecord record)
    {
        _context.AttendanceRecords.Add(record);
        await _context.SaveChangesAsync();
        return record;
    }

    public async Task UpdateAsync(AttendanceRecord record)
    {
        _context.AttendanceRecords.Update(record);
        await _context.SaveChangesAsync();
    }
}