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
                .NotEmpty().WithMessage("Status required");

            RuleFor(r => r.Priority)
                .NotEmpty().WithMessage("Due date required");

            RuleFor(r => r.DueDate)
                .NotEmpty().WithMessage("Due date required");
        }
    }
}
