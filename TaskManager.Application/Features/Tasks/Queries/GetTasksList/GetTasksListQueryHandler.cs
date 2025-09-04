using AutoMapper;
using MediatR;
using TaskManager.Application.Contracts.Persistance;

namespace TaskManager.Application.Features.Tasks.Queries.GetTasksList
{
    public class GetTasksListQueryHandler : IRequestHandler<GetTasksListQuery, List<TaskListVm>>
    {
        private readonly IMapper _mapper;
        private readonly ITaskRepository _taskRepository;

        public GetTasksListQueryHandler(IMapper mapper, ITaskRepository taskRepository)
        {
            _mapper = mapper;
            _taskRepository = taskRepository;
        }

        public async Task<List<TaskListVm>> Handle(GetTasksListQuery request, CancellationToken cancellationToken)
        {
            var userTasks = await _taskRepository.GetTasksByUserIdAsync(request.UserId);
            return _mapper.Map<List<TaskListVm>>(userTasks);
        }
    }
}
