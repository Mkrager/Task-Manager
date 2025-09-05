using MediatR;
using TaskManager.Application.DTOs;
using TaskManager.Domain.Enums;

namespace TaskManager.Application.Features.Tasks.Queries.GetUserTasksList
{
    public class GetUserTasksListQuery : IRequest<TaskListVm>
    {
        public Guid UserId { get; set; }
        public Status? Status { get; set; }
        public Priority? Priority { get; set; }
        public TaskSortFieldDto SortBy { get; set; }
        public bool Ascending { get; set; }
        public DateTime? DueDate { get; set; }

        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}
