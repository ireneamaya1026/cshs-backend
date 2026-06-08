using CSHSBackendAPI.Application.Common.Interfaces.Repositories;
using CSHSBackendAPI.Domain.Entities;
using CSHSBackendAPI.Domain.Enums;
using CSHSBackendAPI.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace CSHSBackendAPI.Infrastructure.Repositories;

public class StudentRepository : IStudentRepository
{
    private readonly AppDbContext _context;

    public StudentRepository(AppDbContext context) => _context = context;

    public async Task<(IEnumerable<Student> Items, int Total)> GetAllAsync(
        string? gradeLevel, string? section,
        long? campusId, string? schoolYear,
        string? status, int page, int limit)
    {
        var query = _context.Students
            .Include(s => s.Campus)
            .AsQueryable();

        if (!string.IsNullOrEmpty(gradeLevel))
            query = query.Where(s => s.GradeLevel == gradeLevel);

        if (!string.IsNullOrEmpty(section))
            query = query.Where(s => s.Section == section);

        if (campusId.HasValue)
            query = query.Where(s => s.CampusId == campusId);

        if (!string.IsNullOrEmpty(schoolYear))
            query = query.Where(s => s.SchoolYear == schoolYear);

        if (!string.IsNullOrEmpty(status) &&
            Enum.TryParse<StudentStatus>(status, true, out var studentStatus))
            query = query.Where(s => s.Status == studentStatus);

        var total = await query.CountAsync();

        var items = await query
            .OrderBy(s => s.LastName)
            .ThenBy(s => s.FirstName)
            .Skip((page - 1) * limit)
            .Take(limit)
            .ToListAsync();

        return (items, total);
    }

    public async Task<Student?> GetByIdAsync(long id) =>
        await _context.Students
            .Include(s => s.Campus)
            .FirstOrDefaultAsync(s => s.Id == id);

    public async Task<IEnumerable<BasicEdGrade>> GetGradesAsync(long studentId) =>
        await _context.BasicEdGrades
            .Where(g => g.StudentId == studentId)
            .OrderBy(g => g.SchoolYear)
            .ThenBy(g => g.Period)
            .ToListAsync();

    public async Task<IEnumerable<AttendanceRecord>> GetAttendanceAsync(long studentId) =>
        await _context.AttendanceRecords
            .Where(a => a.StudentId == studentId)
            .OrderByDescending(a => a.Date)
            .ToListAsync();

    public async Task UpdateAsync(Student student)
    {
        _context.Students.Update(student);
        await _context.SaveChangesAsync();
    }
}