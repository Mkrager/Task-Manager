using AutoMapper;
using MediatR;
using TaskManager.Application.Contracts.Persistance;
using TaskManager.Application.Exceptions;

namespace TaskManager.Application.Features.Tasks.Queries.GetTaskDetails
{
    public class GetTaskDetailQueryHandler : IRequestHandler<GetTaskDetailQuery, TaskDetailVm>
    {
        private readonly IMapper _mapper;
        private readonly IAsyncRepository<Domain.Entities.Task> _taskRepository;

        public GetTaskDetailQueryHandler(IMapper mapper, ITaskRepository taskRepository)
        {
            _mapper = mapper;
            _taskRepository = taskRepository;
        }

        public async Task<TaskDetailVm> Handle(GetTaskDetailQuery request, CancellationToken cancellationToken)
        {
            var task = await _taskRepository.GetByIdAsync(request.Id);

            if (task == null)
                throw new NotFoundException(nameof(Domain.Entities.Task), request.Id);

            return _mapper.Map<TaskDetailVm>(task);
        }
    }
}
