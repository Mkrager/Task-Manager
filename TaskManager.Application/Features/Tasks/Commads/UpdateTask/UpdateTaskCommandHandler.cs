using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using TaskManager.Application.Contracts.Persistance;
using TaskManager.Application.Exceptions;

namespace TaskManager.Application.Features.Tasks.Commads.UpdateTask
{
    public class UpdateTaskCommandHandler : IRequestHandler<UpdateTaskCommand>
    {
        private readonly IAsyncRepository<Domain.Entities.Task> _taskRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<UpdateTaskCommandHandler> _logger;

        public UpdateTaskCommandHandler(
            IAsyncRepository<Domain.Entities.Task> taskRepository,
            IMapper mapper,
            ILogger<UpdateTaskCommandHandler> logger)
        {
            _taskRepository = taskRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<Unit> Handle(UpdateTaskCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Received UpdateTaskCommand for TaskId: {TaskId}", request.Id);

            var taskToUpdate = await _taskRepository.GetByIdAsync(request.Id);

            if (taskToUpdate == null)
            {
                _logger.LogInformation("Task with Id: {TaskId} not found", request.Id);
                throw new NotFoundException(nameof(Domain.Entities.Task), request.Id);
            }

            _mapper.Map(request, taskToUpdate, typeof(UpdateTaskCommand), typeof(Domain.Entities.Task));
            _logger.LogDebug("Task with Id: {TaskId} mapped with new values", request.Id);

            await _taskRepository.UpdateAsync(taskToUpdate);
            _logger.LogInformation("Task with Id: {TaskId} updated successfully", request.Id);

            return Unit.Value;
        }
    }
}