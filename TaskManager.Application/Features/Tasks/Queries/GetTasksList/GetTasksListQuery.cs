using MediatR;

namespace TaskManager.Application.Features.Tasks.Queries.GetTasksList
{
    public class GetTasksListQuery : IRequest<List<TaskListVm>>
    {
        public Guid UserId { get; set; }
    }
}
