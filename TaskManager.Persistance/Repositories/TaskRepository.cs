using Microsoft.EntityFrameworkCore;
using TaskManager.Application.Contracts.Persistance;

namespace TaskManager.Persistance.Repositories
{
    public class TaskRepository : BaseRepository<Domain.Entities.Task>, ITaskRepository
    {
        public TaskRepository(TaskManagerDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<List<Domain.Entities.Task>> GetTasksByUserIdAsync(Guid userId)
        {
            return await _dbContext.Tasks.Where(r => r.UserId == userId).ToListAsync();
        }
    }
}