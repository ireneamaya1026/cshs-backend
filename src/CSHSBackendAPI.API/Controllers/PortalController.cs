using CSHSBackendAPI.Application.Announcements.DTOs;
using CSHSBackendAPI.Application.Attendance.DTOs;
using CSHSBackendAPI.Application.Common.Models;
using CSHSBackendAPI.Application.Portal.DTOs;
using CSHSBackendAPI.Application.Portal.Queries.GetMyAnnouncements;
using CSHSBackendAPI.Application.Portal.Queries.GetMyAttendance;
using CSHSBackendAPI.Application.Portal.Queries.GetMyGrades;
using CSHSBackendAPI.Application.Portal.Queries.GetMyProfile;
using CSHSBackendAPI.Application.Portal.Queries.GetMySchedule;
using CSHSBackendAPI.Application.Students.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CSHSBackendAPI.API.Controllers;

[ApiController]
[Route("api/portal")]
[Authorize(Roles = "student")]
public class PortalController : ControllerBase
{
    private readonly GetMyProfileQueryHandler _profileHandler;
    private readonly GetMyGradesQueryHandler _gradesHandler;
    private readonly GetMyAttendanceQueryHandler _attendanceHandler;
    private readonly GetMyScheduleQueryHandler _scheduleHandler;
    private readonly GetMyAnnouncementsQueryHandler _announcementsHandler;

    public PortalController(
        GetMyProfileQueryHandler profileHandler,
        GetMyGradesQueryHandler gradesHandler,
        GetMyAttendanceQueryHandler attendanceHandler,
        GetMyScheduleQueryHandler scheduleHandler,
        GetMyAnnouncementsQueryHandler announcementsHandler)
    {
        _profileHandler = profileHandler;
        _gradesHandler = gradesHandler;
        _attendanceHandler = attendanceHandler;
        _scheduleHandler = scheduleHandler;
        _announcementsHandler = announcementsHandler;
    }

    [HttpGet("me")]
    public async Task<IActionResult> Me()
    {
        var studentId = long.Parse(User.FindFirst("sub")!.Value);
        var result = await _profileHandler.Handle(studentId);
        return Ok(ApiResponse<MyProfileDto>.Ok(result));
    }

    [HttpGet("grades")]
    public async Task<IActionResult> Grades()
    {
        var studentId = long.Parse(User.FindFirst("sub")!.Value);
        var result = await _gradesHandler.Handle(studentId);
        return Ok(ApiResponse<IEnumerable<StudentGradeDto>>.Ok(result));
    }

    [HttpGet("attendance")]
    public async Task<IActionResult> Attendance([FromQuery] string school_year)
    {
        var studentId = long.Parse(User.FindFirst("sub")!.Value);
        var result = await _attendanceHandler.Handle(studentId, school_year);
        return Ok(ApiResponse<AttendanceSummaryDto>.Ok(result));
    }

    [HttpGet("schedule")]
    public async Task<IActionResult> Schedule()
    {
        var studentId = long.Parse(User.FindFirst("sub")!.Value);
        var result = await _scheduleHandler.Handle(studentId);
        return Ok(ApiResponse<IEnumerable<MyScheduleDto>>.Ok(result));
    }

    [HttpGet("announcements")]
    public async Task<IActionResult> Announcements()
    {
        var studentId = long.Parse(User.FindFirst("sub")!.Value);
        var result = await _announcementsHandler.Handle(studentId);
        return Ok(ApiResponse<IEnumerable<AnnouncementDto>>.Ok(result));
    }
}