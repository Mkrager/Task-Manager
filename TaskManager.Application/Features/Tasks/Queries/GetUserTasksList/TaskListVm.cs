namespace TaskManager.Application.Features.Tasks.Queries.GetUserTasksList
{
    public class TaskListVm
    {
        public List<TaskDto> Tasks { get; set; } = default!;
        public int TotalCount { get; set; }
    }
}
