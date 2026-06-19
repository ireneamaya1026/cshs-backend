using CSHSBackendAPI.Application.Common.Models;
using CSHSBackendAPI.Application.Payments.Commands.RecordPayment;
using CSHSBackendAPI.Application.Payments.DTOs;
using CSHSBackendAPI.Application.Payments.Queries.GetPayments;
using CSHSBackendAPI.Application.Payments.Queries.GetPaymentSummary;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CSHSBackendAPI.API.Controllers;

[ApiController]
[Route("api/payments")]
[Authorize]
public class PaymentsController : ControllerBase
{
    private readonly GetPaymentsQueryHandler _getHandler;
    private readonly GetPaymentSummaryQueryHandler _summaryHandler;
    private readonly RecordPaymentCommandHandler _recordHandler;

    public PaymentsController(
        GetPaymentsQueryHandler getHandler,
        GetPaymentSummaryQueryHandler summaryHandler,
        RecordPaymentCommandHandler recordHandler)
    {
        _getHandler = getHandler;
        _summaryHandler = summaryHandler;
        _recordHandler = recordHandler;
    }

    [HttpGet]
    [Authorize(Roles = "technical_admin,admin,accounting")]
    public async Task<IActionResult> GetAll(
        [FromQuery] long? enrollment_id,
        [FromQuery] long? student_id,
        [FromQuery] DateOnly? date_from,
        [FromQuery] DateOnly? date_to)
    {
        var result = await _getHandler.Handle(enrollment_id, student_id, date_from, date_to);
        return Ok(ApiResponse<IEnumerable<PaymentDto>>.Ok(result));
    }

    [HttpPost]
    [Authorize(Roles = "technical_admin,admin,accounting")]
    public async Task<IActionResult> Record([FromBody] RecordPaymentRequest request)
    {
        var userId = long.Parse(User.FindFirst("sub")!.Value);
        var result = await _recordHandler.Handle(request, userId);
        return Ok(ApiResponse<PaymentDto>.Ok(result, "Payment recorded successfully."));
    }

    [HttpGet("summary")]
    [Authorize(Roles = "technical_admin,admin,accounting")]
    public async Task<IActionResult> GetSummary(
        [FromQuery] string school_year,
        [FromQuery] long? campus_id)
    {
        var result = await _summaryHandler.Handle(school_year, campus_id);
        return Ok(ApiResponse<PaymentSummaryDto>.Ok(result));
    }
}