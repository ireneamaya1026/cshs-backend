using CSHSBackendAPI.Application.Common.Models;
using CSHSBackendAPI.Application.Grades.Commands.ApproveGrade;
using CSHSBackendAPI.Application.Grades.Commands.CreateActivity;
using CSHSBackendAPI.Application.Grades.Commands.PostGrade;
using CSHSBackendAPI.Application.Grades.Commands.RejectGrade;
using CSHSBackendAPI.Application.Grades.Commands.SaveScores;
using CSHSBackendAPI.Application.Grades.Commands.SubmitGrade;
using CSHSBackendAPI.Application.Grades.Commands.UpsertBasicEdGrade;
using CSHSBackendAPI.Application.Grades.Commands.UpsertCollegeGrade;
using CSHSBackendAPI.Application.Grades.DTOs;
using CSHSBackendAPI.Application.Grades.Queries.GetActivities;
using CSHSBackendAPI.Application.Grades.Queries.GetBasicEdGrades;
using CSHSBackendAPI.Application.Grades.Queries.GetCollegeGrades;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CSHSBackendAPI.API.Controllers;

[ApiController]
[Route("api/grades")]
[Authorize]
public class GradesController : ControllerBase
{
    private readonly GetBasicEdGradesQueryHandler _getBasicHandler;
    private readonly GetCollegeGradesQueryHandler _getCollegeHandler;
    private readonly GetActivitiesQueryHandler _getActivitiesHandler;
    private readonly UpsertBasicEdGradeCommandHandler _upsertBasicHandler;
    private readonly UpsertCollegeGradeCommandHandler _upsertCollegeHandler;
    private readonly SubmitGradeCommandHandler _submitHandler;
    private readonly ApproveGradeCommandHandler _approveHandler;
    private readonly PostGradeCommandHandler _postHandler;
    private readonly RejectGradeCommandHandler _rejectHandler;
    private readonly CreateActivityCommandHandler _createActivityHandler;
    private readonly SaveScoresCommandHandler _saveScoresHandler;

    public GradesController(
        GetBasicEdGradesQueryHandler getBasicHandler,
        GetCollegeGradesQueryHandler getCollegeHandler,
        GetActivitiesQueryHandler getActivitiesHandler,
        UpsertBasicEdGradeCommandHandler upsertBasicHandler,
        UpsertCollegeGradeCommandHandler upsertCollegeHandler,
        SubmitGradeCommandHandler submitHandler,
        ApproveGradeCommandHandler approveHandler,
        PostGradeCommandHandler postHandler,
        RejectGradeCommandHandler rejectHandler,
        CreateActivityCommandHandler createActivityHandler,
        SaveScoresCommandHandler saveScoresHandler)
    {
        _getBasicHandler = getBasicHandler;
        _getCollegeHandler = getCollegeHandler;
        _getActivitiesHandler = getActivitiesHandler;
        _upsertBasicHandler = upsertBasicHandler;
        _upsertCollegeHandler = upsertCollegeHandler;
        _submitHandler = submitHandler;
        _approveHandler = approveHandler;
        _postHandler = postHandler;
        _rejectHandler = rejectHandler;
        _createActivityHandler = createActivityHandler;
        _saveScoresHandler = saveScoresHandler;
    }

    // ── Basic Ed ────────────────────────────────────────────────

    // GET /api/grades/basic
    [HttpGet("basic")]
    [Authorize(Roles = "technical_admin,admin,teacher,principal_basic,registrar_basic")]
    public async Task<IActionResult> GetBasicEdGrades(
        [FromQuery] long? subject_load_id,
        [FromQuery] string? period)
    {
        var result = await _getBasicHandler.Handle(subject_load_id, period);
        return Ok(ApiResponse<IEnumerable<BasicEdGradeDto>>.Ok(result));
    }

    // POST /api/grades/basic
    [HttpPost("basic")]
    [Authorize(Roles = "technical_admin,teacher")]
    public async Task<IActionResult> UpsertBasicEdGrade([FromBody] UpsertBasicEdGradeRequest request)
    {
        var teacherId = long.Parse(User.FindFirst("sub")!.Value);
        var teacherName = User.FindFirst("name")?.Value ?? string.Empty;
        var result = await _upsertBasicHandler.Handle(request, teacherId, teacherName);
        return Ok(ApiResponse<BasicEdGradeDto>.Ok(result, "Grade saved successfully."));
    }

    // PATCH /api/grades/basic/:id/submit
    [HttpPatch("basic/{id}/submit")]
    [Authorize(Roles = "technical_admin,teacher")]
    public async Task<IActionResult> SubmitBasicEdGrade(long id)
    {
        await _submitHandler.HandleBasicEd(id);
        return Ok(ApiResponse<object>.Ok("Grade submitted for approval."));
    }

    // PATCH /api/grades/basic/:id/approve
    [HttpPatch("basic/{id}/approve")]
    [Authorize(Roles = "technical_admin,principal_basic")]
    public async Task<IActionResult> ApproveBasicEdGrade(long id)
    {
        var approvedById = long.Parse(User.FindFirst("sub")!.Value);
        await _approveHandler.HandleBasicEd(id, approvedById);
        return Ok(ApiResponse<object>.Ok("Grade approved."));
    }

    // PATCH /api/grades/basic/:id/post
    [HttpPatch("basic/{id}/post")]
    [Authorize(Roles = "technical_admin,registrar_basic")]
    public async Task<IActionResult> PostBasicEdGrade(long id)
    {
        await _postHandler.HandleBasicEd(id);
        return Ok(ApiResponse<object>.Ok("Grade posted successfully."));
    }

    // PATCH /api/grades/basic/:id/reject
    [HttpPatch("basic/{id}/reject")]
    [Authorize(Roles = "technical_admin,principal_basic")]
    public async Task<IActionResult> RejectBasicEdGrade(long id, [FromBody] GradeActionRequest request)
    {
        await _rejectHandler.HandleBasicEd(id, request);
        return Ok(ApiResponse<object>.Ok("Grade rejected."));
    }

    // ── College ─────────────────────────────────────────────────

    // GET /api/grades/college
    [HttpGet("college")]
    [Authorize(Roles = "technical_admin,admin,teacher,program_head,registrar_college")]
    public async Task<IActionResult> GetCollegeGrades(
        [FromQuery] long? subject_load_id,
        [FromQuery] string? semester)
    {
        var result = await _getCollegeHandler.Handle(subject_load_id, semester);
        return Ok(ApiResponse<IEnumerable<CollegeGradeDto>>.Ok(result));
    }

    // POST /api/grades/college
    [HttpPost("college")]
    [Authorize(Roles = "technical_admin,teacher")]
    public async Task<IActionResult> UpsertCollegeGrade([FromBody] UpsertCollegeGradeRequest request)
    {
        var teacherId = long.Parse(User.FindFirst("sub")!.Value);
        var teacherName = User.FindFirst("name")?.Value ?? string.Empty;
        var result = await _upsertCollegeHandler.Handle(request, teacherId, teacherName);
        return Ok(ApiResponse<CollegeGradeDto>.Ok(result, "Grade saved successfully."));
    }

    // PATCH /api/grades/college/:id/submit
    [HttpPatch("college/{id}/submit")]
    [Authorize(Roles = "technical_admin,teacher")]
    public async Task<IActionResult> SubmitCollegeGrade(long id)
    {
        await _submitHandler.HandleCollege(id);
        return Ok(ApiResponse<object>.Ok("Grade submitted for approval."));
    }

    // PATCH /api/grades/college/:id/approve
    [HttpPatch("college/{id}/approve")]
    [Authorize(Roles = "technical_admin,program_head")]
    public async Task<IActionResult> ApproveCollegeGrade(long id)
    {
        var approvedById = long.Parse(User.FindFirst("sub")!.Value);
        await _approveHandler.HandleCollege(id, approvedById);
        return Ok(ApiResponse<object>.Ok("Grade approved."));
    }

    // PATCH /api/grades/college/:id/post
    [HttpPatch("college/{id}/post")]
    [Authorize(Roles = "technical_admin,registrar_college")]
    public async Task<IActionResult> PostCollegeGrade(long id)
    {
        await _postHandler.HandleCollege(id);
        return Ok(ApiResponse<object>.Ok("Grade posted successfully."));
    }

    // PATCH /api/grades/college/:id/reject
    [HttpPatch("college/{id}/reject")]
    [Authorize(Roles = "technical_admin,program_head")]
    public async Task<IActionResult> RejectCollegeGrade(long id, [FromBody] GradeActionRequest request)
    {
        await _rejectHandler.HandleCollege(id, request);
        return Ok(ApiResponse<object>.Ok("Grade rejected."));
    }

    // ── Activities & Scores ─────────────────────────────────────

    // GET /api/grades/activities
    [HttpGet("activities")]
    [Authorize(Roles = "technical_admin,teacher,principal_basic")]
    public async Task<IActionResult> GetActivities(
        [FromQuery] long subject_load_id,
        [FromQuery] string? period)
    {
        var result = await _getActivitiesHandler.Handle(subject_load_id, period);
        return Ok(ApiResponse<IEnumerable<ActivityDto>>.Ok(result));
    }

    // POST /api/grades/activities
    [HttpPost("activities")]
    [Authorize(Roles = "technical_admin,teacher")]
    public async Task<IActionResult> CreateActivity([FromBody] CreateActivityRequest request)
    {
        var result = await _createActivityHandler.Handle(request);
        return Ok(ApiResponse<ActivityDto>.Ok(result, "Activity created successfully."));
    }

    // POST /api/grades/scores
    [HttpPost("scores")]
    [Authorize(Roles = "technical_admin,teacher")]
    public async Task<IActionResult> SaveScores([FromBody] SaveScoresRequest request)
    {
        var recordedById = long.Parse(User.FindFirst("sub")!.Value);
        await _saveScoresHandler.Handle(request, recordedById);
        return Ok(ApiResponse<object>.Ok("Scores saved successfully."));
    }
}