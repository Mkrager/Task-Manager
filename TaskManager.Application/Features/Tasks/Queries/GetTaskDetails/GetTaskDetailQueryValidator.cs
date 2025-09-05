using FluentValidation;
using TaskManager.Application.Contracts.Application;
using TaskManager.Application.Contracts.Persistance;

namespace TaskManager.Application.Features.Tasks.Queries.GetTaskDetails
{
    public class GetTaskDetailQueryValidator : AbstractValidator<GetTaskDetailQuery>
    {
        private readonly IAsyncRepository<Domain.Entities.Task> _taskRepository;
        private readonly IPermissionService _permissionService;
        public GetTaskDetailQueryValidator(IAsyncRepository<Domain.Entities.Task> taskRepository, IPermissionService permissionService)
        {
            _taskRepository = taskRepository;
            _permissionService = permissionService;

            RuleFor(e => e)
                .MustAsync(CheckUserPermissionAsync)
                .WithMessage("You don't have permission");
        }

        private async Task<bool> CheckUserPermissionAsync(GetTaskDetailQuery e,
            CancellationToken cancellationToken)
        {
            var entity = await _taskRepository.GetByIdAsync(e.Id);

            return _permissionService.HasUserPermission(entity, e.UserId);
        }

    }
}
