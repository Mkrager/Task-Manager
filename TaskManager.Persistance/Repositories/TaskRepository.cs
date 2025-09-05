using Microsoft.EntityFrameworkCore;
using TaskManager.Application.Contracts.Persistance;
using TaskManager.Application.DTOs;
using TaskManager.Domain.Enums;

namespace TaskManager.Persistance.Repositories
{
    public class TaskRepository : BaseRepository<Domain.Entities.Task>, ITaskRepository
    {
        public TaskRepository(TaskManagerDbContext dbContext) : base(dbContext)
        {
        }

        /// <summary>
        /// Retrieves user tasks with optional filtering, sorting, and pagination.
        /// </summary>
        /// <param name="userId">The ID of the user whose tasks are being retrieved.</param>
        /// <param name="status">Optional filter by task status.</param>
        /// <param name="dueDate">Optional filter by task due date.</param>
        /// <param name="priority">Optional filter by task priority.</param>
        /// <param name="sortBy">The field to sort by (default is DueDate).</param>
        /// <param name="ascending">Specifies sort order: true for ascending, false for descending.</param>
        /// <param name="pageNumber">Page number for pagination (starting from 1).</param>
        /// <param name="pageSize">Number of items per page.</param>
        /// <returns>
        /// <see cref="GetTasksByUserIdResponse"/> containing a list of user tasks after applying filters,
        /// sorting, and pagination, as well as the total count of all tasks without pagination.
        /// </returns>
        /// <remarks>
        /// This method supports:
        /// - Filtering by status, due date, and priority.
        /// - Sorting by due date or priority.
        /// - Pagination using Skip and Take.
        /// </remarks>
        public async Task<GetTasksByUserIdResponse> GetTasksByUserIdAsync(
            Guid userId,
            Status? status = null,
            DateTime? dueDate = null,
            Priority? priority = null,
            TaskSortFieldDto sortBy = TaskSortFieldDto.DueDate,
            bool ascending = true,
            int pageNumber = 1,
            int pageSize = 10)
        {
            var query = _dbContext.Tasks.Where(t => t.UserId == userId);

            if (status.HasValue)
                query = query.Where(t => t.Status == status.Value);

            if (dueDate.HasValue)
                query = query.Where(t => t.DueDate.Value.Date == dueDate.Value.Date);

            if (priority.HasValue)
                query = query.Where(t => t.Priority == priority.Value);

            query = sortBy switch
            {
                TaskSortFieldDto.DueDate => ascending ? query.OrderBy(t => t.DueDate) : query.OrderByDescending(t => t.DueDate),
                TaskSortFieldDto.Priority => ascending ? query.OrderBy(t => t.Priority) : query.OrderByDescending(t => t.Priority),
                _ => query.OrderBy(t => t.CreatedAt)
            };

            var totalCount = await query.CountAsync();

            var tasks = await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return new GetTasksByUserIdResponse()
            {
                Tasks = tasks,
                TotalCount = totalCount
            };
        }
    }
}