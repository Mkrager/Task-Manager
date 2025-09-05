using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using TaskManager.Application.Contracts.Persistance;
using TaskManager.Application.Exceptions;

namespace TaskManager.Application.Features.Tasks.Commads.DeleteTask
{
    public class DeleteTaskCommandHandler : IRequestHandler<DeleteTaskCommand>
    {
        private readonly IAsyncRepository<Domain.Entities.Task> _taskRepository;
        private readonly ILogger<DeleteTaskCommandHandler> _logger;

        public DeleteTaskCommandHandler(
            IAsyncRepository<Domain.Entities.Task> taskRepository,
            ILogger<DeleteTaskCommandHandler> logger)
        {
            _taskRepository = taskRepository;
            _logger = logger;
        }

        public async Task<Unit> Handle(DeleteTaskCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Received DeleteTaskCommand for TaskId: {TaskId}", request.Id);

            var taskToDelete = await _taskRepository.GetByIdAsync(request.Id);

            if (taskToDelete == null)
            {
                _logger.LogInformation("Task with Id: {TaskId} not found", request.Id);
                throw new NotFoundException(nameof(Domain.Entities.Task), request.Id);
            }

            await _taskRepository.DeleteAsync(taskToDelete);

            _logger.LogInformation("Task with Id: {TaskId} deleted successfully", request.Id);

            return Unit.Value;
        }
    }
}