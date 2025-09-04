using FluentValidation;

namespace TaskManager.Application.Features.Tasks.Commads.UpdateTask
{
    public class UpdateTaskCommandValidator : AbstractValidator<UpdateTaskCommand>
    {
        public UpdateTaskCommandValidator()
        {
            RuleFor(r => r.Title)
                .NotEmpty().WithMessage("Title required");

            RuleFor(r => r.Status)
                .IsInEnum().WithMessage("Status is invalid");

            RuleFor(r => r.Priority)
                .IsInEnum().WithMessage("Priority is invalid");

            RuleFor(r => r.DueDate)
                .NotEmpty().WithMessage("Due date required");
        }
    }
}
