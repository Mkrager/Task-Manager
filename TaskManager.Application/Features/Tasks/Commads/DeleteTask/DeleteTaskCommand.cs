using MediatR;

namespace TaskManager.Application.Features.Tasks.Commads.DeleteTask
{
    public class DeleteTaskCommand : IRequest
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
    }
}
