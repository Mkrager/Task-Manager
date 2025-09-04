namespace TaskManager.Application.Contracts.Persistance
{
    public interface ITaskRepository : IAsyncRepository<Domain.Entities.Task>
    {
        Task<List<Domain.Entities.Task>> GetTasksByUserIdAsync(Guid userId);
    }
}
