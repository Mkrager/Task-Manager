using AutoMapper;
using MediatR;
using TaskManager.Application.Contracts.Persistance;
using TaskManager.Application.Exceptions;

namespace TaskManager.Application.Features.Tasks.Commads.UpdateTask
{
    public class UpdateTaskCommandHandler : IRequestHandler<UpdateTaskCommand>
    {
        private readonly IAsyncRepository<Domain.Entities.Task> _taskRepository;

        private readonly IMapper _mapper;
        public UpdateTaskCommandHandler(IAsyncRepository<Domain.Entities.Task> taskRepository, IMapper mapper)
        {
            _taskRepository = taskRepository;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(UpdateTaskCommand request, CancellationToken cancellationToken)
        {
            var taskToUpdate = await _taskRepository.GetByIdAsync(request.Id);

            if (taskToUpdate == null)
                throw new NotFoundException(nameof(Domain.Entities.Task), request.Id);

            _mapper.Map(request, taskToUpdate, typeof(UpdateTaskCommand), typeof(Domain.Entities.Task));

            await _taskRepository.UpdateAsync(taskToUpdate);

            return Unit.Value;
        }
    }
}
