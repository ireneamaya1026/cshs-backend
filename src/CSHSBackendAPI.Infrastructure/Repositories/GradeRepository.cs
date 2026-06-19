using CSHSBackendAPI.Application.Common.Interfaces.Repositories;
using CSHSBackendAPI.Domain.Entities;
using CSHSBackendAPI.Domain.Enums;
using CSHSBackendAPI.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace CSHSBackendAPI.Infrastructure.Repositories;

public class GradeRepository : IGradeRepository
{
    private readonly AppDbContext _context;

    public GradeRepository(AppDbContext context) => _context = context;

    // ── Basic Ed ────────────────────────────────────────────────
    public async Task<IEnumerable<BasicEdGrade>> GetBasicEdGradesAsync(
        long? subjectLoadId, string? period)
    {
        var query = _context.BasicEdGrades.AsQueryable();

        if (subjectLoadId.HasValue)
            query = query.Where(g => g.SubjectLoadId == subjectLoadId);

        if (!string.IsNullOrEmpty(period) &&
            Enum.TryParse<GradePeriod>(period, true, out var gradePeriod))
            query = query.Where(g => g.Period == gradePeriod);

        return await query
            .OrderBy(g => g.StudentId)
            .ToListAsync();
    }

    public async Task<BasicEdGrade?> GetBasicEdGradeByIdAsync(long id) =>
        await _context.BasicEdGrades.FirstOrDefaultAsync(g => g.Id == id);

    public async Task<BasicEdGrade?> GetBasicEdGradeByUniqueKeyAsync(
        long studentId, long subjectLoadId, string period)
    {
        if (!Enum.TryParse<GradePeriod>(period, true, out var gradePeriod))
            return null;

        return await _context.BasicEdGrades
            .FirstOrDefaultAsync(g =>
                g.StudentId == studentId &&
                g.SubjectLoadId == subjectLoadId &&
                g.Period == gradePeriod);
    }

    public async Task<BasicEdGrade> CreateBasicEdGradeAsync(BasicEdGrade grade)
    {
        _context.BasicEdGrades.Add(grade);
        await _context.SaveChangesAsync();
        return grade;
    }

    public async Task UpdateBasicEdGradeAsync(BasicEdGrade grade)
    {
        _context.BasicEdGrades.Update(grade);
        await _context.SaveChangesAsync();
    }

    // ── College ─────────────────────────────────────────────────
    public async Task<IEnumerable<CollegeGrade>> GetCollegeGradesAsync(
        long? subjectLoadId, string? semester)
    {
        var query = _context.CollegeGrades.AsQueryable();

        if (subjectLoadId.HasValue)
            query = query.Where(g => g.SubjectLoadId == subjectLoadId);

        if (!string.IsNullOrEmpty(semester))
            query = query.Where(g => g.Semester == semester);

        return await query
            .OrderBy(g => g.StudentId)
            .ToListAsync();
    }

    public async Task<CollegeGrade?> GetCollegeGradeByIdAsync(long id) =>
        await _context.CollegeGrades.FirstOrDefaultAsync(g => g.Id == id);

    public async Task<CollegeGrade?> GetCollegeGradeByUniqueKeyAsync(
        long studentId, long subjectLoadId, string semester) =>
        await _context.CollegeGrades
            .FirstOrDefaultAsync(g =>
                g.StudentId == studentId &&
                g.SubjectLoadId == subjectLoadId &&
                g.Semester == semester);

    public async Task<CollegeGrade> CreateCollegeGradeAsync(CollegeGrade grade)
    {
        _context.CollegeGrades.Add(grade);
        await _context.SaveChangesAsync();
        return grade;
    }

    public async Task UpdateCollegeGradeAsync(CollegeGrade grade)
    {
        _context.CollegeGrades.Update(grade);
        await _context.SaveChangesAsync();
    }

    // ── Activities ──────────────────────────────────────────────
    public async Task<IEnumerable<GradeActivity>> GetActivitiesAsync(
        long subjectLoadId, string? period)
    {
        var query = _context.GradeActivities
            .Where(a => a.SubjectLoadId == subjectLoadId && a.IsActive);

        if (!string.IsNullOrEmpty(period) &&
            Enum.TryParse<GradePeriod>(period, true, out var gradePeriod))
            query = query.Where(a => a.Period == gradePeriod);

        return await query.OrderBy(a => a.SortOrder).ToListAsync();
    }

    public async Task<GradeActivity> CreateActivityAsync(GradeActivity activity)
    {
        _context.GradeActivities.Add(activity);
        await _context.SaveChangesAsync();
        return activity;
    }

    // ── Scores ──────────────────────────────────────────────────
    public async Task<StudentActivityScore?> GetScoreAsync(long activityId, long studentId) =>
        await _context.StudentActivityScores
            .FirstOrDefaultAsync(s => s.ActivityId == activityId && s.StudentId == studentId);

    public async Task<StudentActivityScore> CreateScoreAsync(StudentActivityScore score)
    {
        _context.StudentActivityScores.Add(score);
        await _context.SaveChangesAsync();
        return score;
    }

    public async Task UpdateScoreAsync(StudentActivityScore score)
    {
        _context.StudentActivityScores.Update(score);
        await _context.SaveChangesAsync();
    }
}