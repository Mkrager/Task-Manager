using AutoMapper;
using Microsoft.Extensions.Logging;
using Moq;
using Shouldly;
using TaskManager.Application.Contracts.Persistance;
using TaskManager.Application.DTOs;
using TaskManager.Application.Features.Tasks.Queries.GetUserTasksList;
using TaskManager.Application.Profiles;
using TaskManager.Application.UnitTests.Mocks;
using TaskManager.Domain.Enums;

namespace TaskManager.Application.UnitTests.Tasks.Queries
{
    public class GetUserTasksListQueryHandlerTests
    {
        private readonly IMapper _mapper;
        private readonly Mock<ITaskRepository> _mockTaskRepository;
        private readonly Mock<ILogger<GetUserTasksListQueryHandler>> _mockLoggerService;

        private readonly Guid _userId = Guid.Parse("6b6da9a2-1f8b-4676-84b9-baf714600217");

        public GetUserTasksListQueryHandlerTests()
        {
            _mockTaskRepository = TaskRepositoryMock.GetTaskRepository();
            _mockLoggerService = LoggerServiceMock.GetLoggerService<GetUserTasksListQueryHandler>();
            var configurationProvider = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });
            _mapper = configurationProvider.CreateMapper();
        }

        [Fact]
        public async Task GetUserTasksList_ReturnsListOfUserTasks()
        {
            var handler = new GetUserTasksListQueryHandler(_mapper, _mockTaskRepository.Object, _mockLoggerService.Object);

            var result = await handler.Handle(new GetUserTasksListQuery() { UserId = _userId }, CancellationToken.None);

            result.ShouldBeOfType<TaskListVm>();
            result.Tasks.Count.ShouldBe(4);
        }

        [Fact]
        public async Task GetUserTasksList_FiltersByStatus()
        {
            var handler = new GetUserTasksListQueryHandler(_mapper, _mockTaskRepository.Object, _mockLoggerService.Object);

            var result = await handler.Handle(new GetUserTasksListQuery()
            {
                UserId = _userId,
                Status = Status.Pending
            }, CancellationToken.None);

            result.Tasks.ShouldAllBe(t => t.Status == Status.Pending);
            result.Tasks.Count.ShouldBe(2);
        }

        [Fact]
        public async Task GetUserTasksList_FiltersByPriority()
        {
            var handler = new GetUserTasksListQueryHandler(_mapper, _mockTaskRepository.Object, _mockLoggerService.Object);

            var result = await handler.Handle(new GetUserTasksListQuery()
            {
                UserId = _userId,
                Priority = Priority.Low
            }, CancellationToken.None);

            result.Tasks.ShouldAllBe(t => t.Priority == Priority.Low);
            result.Tasks.Count.ShouldBe(2);
        }

        [Fact]
        public async Task GetUserTasksList_FiltersByDueDate()
        {
            var dueDate = DateTime.UtcNow.AddDays(5).Date;
            var handler = new GetUserTasksListQueryHandler(_mapper, _mockTaskRepository.Object, _mockLoggerService.Object);

            var result = await handler.Handle(new GetUserTasksListQuery()
            {
                UserId = _userId,
                DueDate = dueDate
            }, CancellationToken.None);

            result.Tasks.ShouldAllBe(t => t.DueDate.Value.Date == dueDate);
            result.Tasks.Count.ShouldBe(1);
        }

        [Fact]
        public async Task GetUserTasksList_SortsByPriorityDescending()
        {
            var handler = new GetUserTasksListQueryHandler(_mapper, _mockTaskRepository.Object, _mockLoggerService.Object);

            var result = await handler.Handle(new GetUserTasksListQuery()
            {
                UserId = _userId,
                SortBy = TaskSortFieldDto.Priority,
                Ascending = false
            }, CancellationToken.None);

            result.Tasks[0].Priority.ShouldBe(Priority.High);
            result.Tasks[1].Priority.ShouldBe(Priority.Medium);
            result.Tasks[2].Priority.ShouldBe(Priority.Low);
            result.Tasks[3].Priority.ShouldBe(Priority.Low);
        }

        [Fact]
        public async Task GetUserTasksList_Pagination_WorksCorrectly()
        {
            var handler = new GetUserTasksListQueryHandler(_mapper, _mockTaskRepository.Object, _mockLoggerService.Object);

            var result = await handler.Handle(new GetUserTasksListQuery()
            {
                UserId = _userId,
                PageNumber = 2,
                PageSize = 2
            }, CancellationToken.None);

            result.Tasks.Count.ShouldBe(2);
        }
    }
}
