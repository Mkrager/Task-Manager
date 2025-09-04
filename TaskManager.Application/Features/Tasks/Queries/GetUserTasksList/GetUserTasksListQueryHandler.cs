using AutoMapper;
using MediatR;
using TaskManager.Application.Contracts.Persistance;

namespace TaskManager.Application.Features.Tasks.Queries.GetUserTasksList
{
    public class GetUserTasksListQueryHandler : IRequestHandler<GetUserTasksListQuery, List<TaskListVm>>
    {
        private readonly IMapper _mapper;
        private readonly ITaskRepository _taskRepository;

        public GetUserTasksListQueryHandler(IMapper mapper, ITaskRepository taskRepository)
        {
            _mapper = mapper;
            _taskRepository = taskRepository;
        }

        public async Task<List<TaskListVm>> Handle(GetUserTasksListQuery request, CancellationToken cancellationToken)
        {
            var userTasks = await _taskRepository.GetTasksByUserIdAsync(request.UserId);
            return _mapper.Map<List<TaskListVm>>(userTasks);
        }
    }
}
