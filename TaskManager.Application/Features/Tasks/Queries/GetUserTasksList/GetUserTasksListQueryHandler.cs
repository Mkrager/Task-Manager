using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using TaskManager.Application.Contracts.Persistance;

namespace TaskManager.Application.Features.Tasks.Queries.GetUserTasksList
{
    public class GetUserTasksListQueryHandler : IRequestHandler<GetUserTasksListQuery, TaskListVm>
    {
        private readonly IMapper _mapper;
        private readonly ITaskRepository _taskRepository;
        private readonly ILogger<GetUserTasksListQueryHandler> _logger;

        public GetUserTasksListQueryHandler(
            IMapper mapper,
            ITaskRepository taskRepository,
            ILogger<GetUserTasksListQueryHandler> logger)
        {
            _mapper = mapper;
            _taskRepository = taskRepository;
            _logger = logger;
        }

        public async Task<TaskListVm> Handle(GetUserTasksListQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation(
                "Received GetUserTasksListQuery for UserId: {UserId}, Page: {PageNumber}, PageSize: {PageSize}",
                request.UserId, request.PageNumber, request.PageSize);

            _logger.LogDebug(
                "Filters applied: Status={Status}, DueDate={DueDate}, Priority={Priority}, SortBy={SortBy}, Ascending={Ascending}",
                request.Status, request.DueDate, request.Priority, request.SortBy, request.Ascending);

            var userTasks = await _taskRepository.GetTasksByUserIdAsync(
                request.UserId,
                request.Status,
                request.DueDate,
                request.Priority,
                request.SortBy,
                request.Ascending,
                request.PageNumber,
                request.PageSize);

            _logger.LogInformation(
                "Returning {Count} tasks for UserId: {UserId}", userTasks.Tasks.Count, request.UserId);

            return _mapper.Map<TaskListVm>(userTasks);
        }
    }
}
