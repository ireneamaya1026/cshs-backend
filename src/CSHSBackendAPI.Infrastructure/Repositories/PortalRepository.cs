using CSHSBackendAPI.Application.Common.Interfaces.Repositories;
using CSHSBackendAPI.Domain.Entities;
using CSHSBackendAPI.Domain.Enums;
using CSHSBackendAPI.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace CSHSBackendAPI.Infrastructure.Repositories;

public class PortalRepository : IPortalRepository
{
    private readonly AppDbContext _context;

    public PortalRepository(AppDbContext context) => _context = context;

    public async Task<Student?> GetStudentByIdAsync(long studentId) =>
        await _context.Students
            .Include(s => s.Campus)
            .FirstOrDefaultAsync(s => s.Id == studentId);

    public async Task<IEnumerable<BasicEdGrade>> GetPostedGradesAsync(long studentId) =>
        await _context.BasicEdGrades
            .Where(g => g.StudentId == studentId && g.Status == GradeStatus.Posted)
            .OrderBy(g => g.SchoolYear)
            .ThenBy(g => g.Period)
            .ToListAsync();

    public async Task<IEnumerable<CollegeGrade>> GetPostedCollegeGradesAsync(long studentId) =>
        await _context.CollegeGrades
            .Where(g => g.StudentId == studentId && g.Status == GradeStatus.Posted)
            .OrderBy(g => g.SchoolYear)
            .ThenBy(g => g.Semester)
            .ToListAsync();

    public async Task<IEnumerable<SubjectLoad>> GetScheduleAsync(
        string gradeLevel, string? section, string schoolYear)
    {
        var query = _context.SubjectLoads
            .Where(s => s.GradeLevel == gradeLevel &&
                       s.SchoolYear == schoolYear &&
                       s.IsActive);

        if (!string.IsNullOrEmpty(section))
            query = query.Where(s => s.Section == section);

        return await query.ToListAsync();
    }
}