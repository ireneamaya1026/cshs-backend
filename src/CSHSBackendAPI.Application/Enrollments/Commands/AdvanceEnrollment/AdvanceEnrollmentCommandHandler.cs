using CSHSBackendAPI.Application.Common.Exceptions;
using CSHSBackendAPI.Application.Common.Interfaces.Repositories;
using CSHSBackendAPI.Application.Enrollments.DTOs;
using CSHSBackendAPI.Domain.Entities;

namespace CSHSBackendAPI.Application.Enrollments.Commands.AdvanceEnrollment;

public class AdvanceEnrollmentCommandHandler
{
    private readonly IEnrollmentRepository _enrollmentRepo;
    private readonly IWorkflowRepository _workflowRepo;

    public AdvanceEnrollmentCommandHandler(
        IEnrollmentRepository enrollmentRepo,
        IWorkflowRepository workflowRepo)
    {
        _enrollmentRepo = enrollmentRepo;
        _workflowRepo = workflowRepo;
    }

    public async Task<EnrollmentDto> Handle(
        long id, AdvanceEnrollmentRequest request,
        string byName, string byRole)
    {
        var enrollment = await _enrollmentRepo.GetByIdAsync(id)
            ?? throw new NotFoundException("Enrollment", id);

        // Get workflow definition
        var workflow = await _workflowRepo.GetByWorkflowIdAsync(enrollment.WorkflowId)
            ?? throw new NotFoundException("Workflow", enrollment.WorkflowId);

        // Find next step based on action_id
        var nextStep = GetNextStep(workflow.StepsJson, enrollment.CurrentStep, request.ActionId);

        if (nextStep == null)
            throw new ValidationException(new List<string>
            {
                $"Action '{request.ActionId}' is not valid for step '{enrollment.CurrentStep}'."
            });

        var previousStep = enrollment.CurrentStep;
        enrollment.PreviousStep = previousStep;
        enrollment.CurrentStep = nextStep;

        await _enrollmentRepo.UpdateAsync(enrollment);

        // Append to stage history (immutable)
        await _enrollmentRepo.AddStageHistoryAsync(new EnrollmentStageHistory
        {
            EnrollmentId = enrollment.Id,
            Step = nextStep,
            FromStep = previousStep,
            ActionId = request.ActionId,
            ByName = byName,
            ByRole = byRole,
            Note = request.Note,
            RecordedAt = DateTime.UtcNow
        });

        return new EnrollmentDto
        {
            Id = enrollment.Id,
            ReferenceNo = enrollment.ReferenceNo,
            CurrentStep = enrollment.CurrentStep,
            PreviousStep = enrollment.PreviousStep,
            WorkflowId = enrollment.WorkflowId
        };
    }

    private string? GetNextStep(string stepsJson, string currentStep, string actionId)
    {
        try
        {
            var steps = System.Text.Json.JsonSerializer.Deserialize<List<WorkflowStep>>(stepsJson);
            var current = steps?.FirstOrDefault(s => s.Id == currentStep);
            var action = current?.Actions?.FirstOrDefault(a => a.Id == actionId);
            return action?.NextStep;
        }
        catch { return null; }
    }
}

public class WorkflowStep
{
    public string Id { get; set; } = string.Empty;
    public string Label { get; set; } = string.Empty;
    public List<WorkflowAction>? Actions { get; set; }
}

public class WorkflowAction
{
    public string Id { get; set; } = string.Empty;
    public string Label { get; set; } = string.Empty;
    public string NextStep { get; set; } = string.Empty;
}