using MediatR;
using TaskManager.Domain.Enums;

namespace TaskManager.Application.Features.Tasks.Commads.UpdateTask
{
    public class UpdateTaskCommand : IRequest
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime? DueDate { get; set; }
        public Status Status { get; set; }
        public Priority Priority { get; set; }
        public Guid UserId { get; set; }
    }
}
