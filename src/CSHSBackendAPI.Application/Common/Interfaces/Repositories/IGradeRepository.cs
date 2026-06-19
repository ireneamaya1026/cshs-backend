using CSHSBackendAPI.Domain.Entities;

namespace CSHSBackendAPI.Application.Common.Interfaces.Repositories;

public interface IGradeRepository
{
    // Basic Ed
    Task<IEnumerable<BasicEdGrade>> GetBasicEdGradesAsync(long? subjectLoadId, string? period);
    Task<BasicEdGrade?> GetBasicEdGradeByIdAsync(long id);
    Task<BasicEdGrade?> GetBasicEdGradeByUniqueKeyAsync(long studentId, long subjectLoadId, string period);
    Task<BasicEdGrade> CreateBasicEdGradeAsync(BasicEdGrade grade);
    Task UpdateBasicEdGradeAsync(BasicEdGrade grade);

    // College
    Task<IEnumerable<CollegeGrade>> GetCollegeGradesAsync(long? subjectLoadId, string? semester);
    Task<CollegeGrade?> GetCollegeGradeByIdAsync(long id);
    Task<CollegeGrade?> GetCollegeGradeByUniqueKeyAsync(long studentId, long subjectLoadId, string semester);
    Task<CollegeGrade> CreateCollegeGradeAsync(CollegeGrade grade);
    Task UpdateCollegeGradeAsync(CollegeGrade grade);

    // Activities
    Task<IEnumerable<GradeActivity>> GetActivitiesAsync(long subjectLoadId, string? period);
    Task<GradeActivity> CreateActivityAsync(GradeActivity activity);

    // Scores
    Task<StudentActivityScore?> GetScoreAsync(long activityId, long studentId);
    Task<StudentActivityScore> CreateScoreAsync(StudentActivityScore score);
    Task UpdateScoreAsync(StudentActivityScore score);
}