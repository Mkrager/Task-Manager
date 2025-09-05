using TaskManager.Application.DTOs;
using TaskManager.Domain.Enums;

namespace TaskManager.Application.Contracts.Persistance
{
    public interface ITaskRepository : IAsyncRepository<Domain.Entities.Task>
    {
        Task<GetTasksByUserIdResponse> GetTasksByUserIdAsync(
            Guid userId,
            Status? status = null,
            DateTime? dueDate = null,
            Priority? priority = null,
            TaskSortFieldDto sortBy = TaskSortFieldDto.DueDate,
            bool ascending = true,
            int pageNumber = 1,
            int pageSize = 10);
    }
}
