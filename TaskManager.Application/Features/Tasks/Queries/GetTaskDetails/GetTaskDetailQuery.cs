using MediatR;

namespace TaskManager.Application.Features.Tasks.Queries.GetTaskDetails
{
    public class GetTaskDetailQuery : IRequest<TaskDetailVm>
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
    }
}
