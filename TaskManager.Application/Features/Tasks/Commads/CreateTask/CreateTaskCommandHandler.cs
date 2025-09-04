using AutoMapper;
using MediatR;
using TaskManager.Application.Contracts.Persistance;

namespace TaskManager.Application.Features.Tasks.Commads.CreateTask
{
    public class CreateTaskCommandHandler : IRequestHandler<CreateTaskCommand, Guid>
    {
        private readonly IMapper _mapper;
        private readonly IAsyncRepository<Domain.Entities.Task> _taskRepository;

        public CreateTaskCommandHandler(IMapper mapper, IAsyncRepository<Domain.Entities.Task> taskRepository)
        {
            _mapper = mapper;
            _taskRepository = taskRepository;
        }
        public async Task<Guid> Handle(CreateTaskCommand request, CancellationToken cancellationToken)
        {
            var task = _mapper.Map<Domain.Entities.Task>(request);

            task = await _taskRepository.AddAsync(task);

            return task.Id;
        }
    }
}