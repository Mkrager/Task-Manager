using AutoMapper;
using MediatR;
using TaskManager.Application.Contracts.Persistance;
using TaskManager.Application.Exceptions;

namespace TaskManager.Application.Features.Tasks.Commads.DeleteTask
{
    public class DeleteTaskCommandHandler : IRequestHandler<DeleteTaskCommand>
    {
        private readonly IAsyncRepository<Domain.Entities.Task> _taskRepository;

        public DeleteTaskCommandHandler(IMapper mapper, IAsyncRepository<Domain.Entities.Task> taskRepository)
        {
            _taskRepository = taskRepository;
        }

        public async Task<Unit> Handle(DeleteTaskCommand request, CancellationToken cancellationToken)
        {
            var taskToDelete = await _taskRepository.GetByIdAsync(request.Id);

            if (taskToDelete == null)
                throw new NotFoundException(nameof(Domain.Entities.Task), request.Id);

            await _taskRepository.DeleteAsync(taskToDelete);

            return Unit.Value;
        }
    }
}
