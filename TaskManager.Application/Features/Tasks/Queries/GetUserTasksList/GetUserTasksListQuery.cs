using MediatR;

namespace TaskManager.Application.Features.Tasks.Queries.GetUserTasksList
{
    public class GetUserTasksListQuery : IRequest<List<TaskListVm>>
    {
        public Guid UserId { get; set; }
    }
}
