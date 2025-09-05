using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using TaskManager.Application.Contracts.Persistance;
using TaskManager.Application.Exceptions;

namespace TaskManager.Application.Features.Tasks.Queries.GetTaskDetails
{
    public class GetTaskDetailQueryHandler : IRequestHandler<GetTaskDetailQuery, TaskDetailVm>
    {
        private readonly IMapper _mapper;
        private readonly IAsyncRepository<Domain.Entities.Task> _taskRepository;
        private readonly ILogger<GetTaskDetailQueryHandler> _logger;

        public GetTaskDetailQueryHandler(
            IMapper mapper,
            ITaskRepository taskRepository,
            ILogger<GetTaskDetailQueryHandler> logger)
        {
            _mapper = mapper;
            _taskRepository = taskRepository;
            _logger = logger;
        }

        public async Task<TaskDetailVm> Handle(GetTaskDetailQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Received GetTaskDetailQuery for TaskId: {TaskId}", request.Id);

            var task = await _taskRepository.GetByIdAsync(request.Id);

            if (task == null)
            {
                _logger.LogInformation("Task with Id: {TaskId} not found", request.Id);
                throw new NotFoundException(nameof(Domain.Entities.Task), request.Id);
            }

            _logger.LogDebug("Mapping Task entity with Id: {TaskId} to TaskDetailVm", request.Id);
            var taskDetail = _mapper.Map<TaskDetailVm>(task);

            _logger.LogInformation("Returning details for TaskId: {TaskId}", request.Id);
            return taskDetail;
        }
    }
}