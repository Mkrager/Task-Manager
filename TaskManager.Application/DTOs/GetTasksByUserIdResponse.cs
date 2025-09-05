namespace TaskManager.Application.DTOs
{
    public class GetTasksByUserIdResponse
    {
        public List<Domain.Entities.Task> Tasks { get; set; } = default!;
        public int TotalCount { get; set; }
    }
}
