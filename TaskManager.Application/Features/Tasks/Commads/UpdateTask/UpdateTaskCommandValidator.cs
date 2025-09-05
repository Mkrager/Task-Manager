using FluentValidation;
using TaskManager.Application.Contracts.Application;
using TaskManager.Application.Contracts.Persistance;
using TaskManager.Application.Features.Tasks.Commads.DeleteTask;
using TaskManager.Application.Services;

namespace TaskManager.Application.Features.Tasks.Commads.UpdateTask
{
    public class UpdateTaskCommandValidator : AbstractValidator<UpdateTaskCommand>
    {
        private readonly IAsyncRepository<Domain.Entities.Task> _taskRepository;
        private readonly IPermissionService _permissionService;
        public UpdateTaskCommandValidator(IAsyncRepository<Domain.Entities.Task> taskRepository, IPermissionService permissionService)
        {
            _taskRepository = taskRepository;
            _permissionService = permissionService;

            RuleFor(r => r.Title)
                .NotEmpty().WithMessage("Title required");

            RuleFor(r => r.Status)
                .IsInEnum().WithMessage("Status is invalid");

            RuleFor(r => r.Priority)
                .IsInEnum().WithMessage("Priority is invalid");

            RuleFor(r => r.DueDate)
                .NotEmpty().WithMessage("Due date required");

            RuleFor(e => e)
                .MustAsync(CheckUserPermissionAsync)
                .WithMessage("You don't have permission");
        }

        private async Task<bool> CheckUserPermissionAsync(UpdateTaskCommand e,
            CancellationToken cancellationToken)
        {
            var entity = await _taskRepository.GetByIdAsync(e.Id);

            return _permissionService.HasUserPermission(entity, e.UserId);
        }

    }
}
