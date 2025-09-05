using AutoMapper;
using Microsoft.Extensions.Logging;
using Moq;
using Shouldly;
using TaskManager.Application.Contracts.Persistance;
using TaskManager.Application.Features.Tasks.Queries.GetTaskDetails;
using TaskManager.Application.Profiles;
using TaskManager.Application.UnitTests.Mocks;
using TaskManager.Domain.Enums;

namespace TaskManager.Application.UnitTests.Tasks.Queries
{
    public class GetTaskDetailQueryHandlerTest
    {
        private readonly IMapper _mapper;
        private readonly Mock<ITaskRepository> _mockTaskRepository;
        private readonly Mock<ILogger<GetTaskDetailQueryHandler>> _mockLoggerService;
        public GetTaskDetailQueryHandlerTest()
        {
            _mockTaskRepository = TaskRepositoryMock.GetTaskRepository();
            _mockLoggerService = LoggerServiceMock.GetLoggerService<GetTaskDetailQueryHandler>();
            var configurationProvider = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });
            _mapper = configurationProvider.CreateMapper();
        }

        [Fact]
        public async Task GetTaskDetails_ReturnsCorrectTaskDetails()
        {
            var handler = new GetTaskDetailQueryHandler(_mapper, _mockTaskRepository.Object, _mockLoggerService.Object);

            var result = await handler.Handle(new GetTaskDetailQuery() { Id = Guid.Parse("b8c3f27a-7b28-4ae6-94c2-91fdc33b77e8") }, CancellationToken.None);

            result.ShouldBeOfType<TaskDetailVm>();

            result.Id.ShouldBe(Guid.Parse("b8c3f27a-7b28-4ae6-94c2-91fdc33b77e8"));
            result.Title.ShouldBe("Test Task 1");
            result.Description.ShouldBe("Description for task 1");
            result.DueDate.Value.ShouldBe(DateTime.UtcNow.AddDays(3), TimeSpan.FromSeconds(1));
            result.Status.ShouldBe(Status.Pending);
            result.Priority.ShouldBe(Priority.Medium);
        }
    }
}
