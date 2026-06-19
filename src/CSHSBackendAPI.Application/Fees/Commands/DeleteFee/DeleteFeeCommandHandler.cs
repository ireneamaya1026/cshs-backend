using CSHSBackendAPI.Application.Common.Exceptions;
using CSHSBackendAPI.Application.Common.Interfaces.Repositories;

namespace CSHSBackendAPI.Application.Fees.Commands.DeleteFee;

public class DeleteFeeCommandHandler
{
    private readonly IFeeRepository _repo;

    public DeleteFeeCommandHandler(IFeeRepository repo) =>
        _repo = repo;

    public async Task Handle(long id)
    {
        var fee = await _repo.GetByIdAsync(id)
            ?? throw new NotFoundException("FeeStructure", id);

        fee.IsActive = false;
        await _repo.UpdateAsync(fee);
    }
}