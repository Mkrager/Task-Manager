using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using TaskManager.Application.Contracts.Persistance;

namespace TaskManager.Application.Features.Tasks.Commads.CreateTask
{
    public class CreateTaskCommandHandler : IRequestHandler<CreateTaskCommand, Guid>
    {
        private readonly IMapper _mapper;
        private readonly IAsyncRepository<Domain.Entities.Task> _taskRepository;
        private readonly ILogger<CreateTaskCommandHandler> _logger;

        public CreateTaskCommandHandler(
            IMapper mapper, 
            IAsyncRepository<Domain.Entities.Task> taskRepository,
            ILogger<CreateTaskCommandHandler> logger)
        {
            _mapper = mapper;
            _taskRepository = taskRepository;
            _logger = logger;
        }
        public async Task<Guid> Handle(CreateTaskCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Creating task with Title: {Title}, Status: {Status}, Priority: {Priority}",
                request.Title, request.Status, request.Priority);

            var task = _mapper.Map<Domain.Entities.Task>(request);
            _logger.LogDebug("Task entity mapped from CreateTaskCommand for Title: {Title}", request.Title);

            task = await _taskRepository.AddAsync(task);
            _logger.LogInformation(
                "Task created successfully with Id: {TaskId} for UserId: {UserId}",
                task.Id, task.UserId);

            return task.Id;
        }
    }
}