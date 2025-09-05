using AutoMapper;
using Microsoft.Extensions.Logging;
using Moq;
using Shouldly;
using TaskManager.Application.Contracts.Persistance;
using TaskManager.Application.Features.Tasks.Commads.CreateTask;
using TaskManager.Application.Features.Tasks.Commads.DeleteTask;
using TaskManager.Application.Profiles;
using TaskManager.Application.UnitTests.Mocks;

namespace TaskManager.Application.UnitTests.Tasks.Commands
{
    public class DeleteTaskCommandTest
    {
        private readonly Mock<ITaskRepository> _mockTaskRepository;
        private readonly Mock<ILogger<DeleteTaskCommandHandler>> _mockLoggerService;
        public DeleteTaskCommandTest()
        {
            _mockTaskRepository = TaskRepositoryMock.GetTaskRepository();
            _mockLoggerService = LoggerServiceMock.GetLoggerService<DeleteTaskCommandHandler>();
        }

        [Fact]
        public async Task Delete_Course_RemovesCourseFromRepo()
        {
            var handler = new DeleteTaskCommandHandler(_mockTaskRepository.Object, _mockLoggerService.Object);
            await handler.Handle(new DeleteTaskCommand() { Id = Guid.Parse("b8c3f27a-7b28-4ae6-94c2-91fdc33b77e8") }, CancellationToken.None);

            var allCourses = await _mockTaskRepository.Object.ListAllAsync();
            allCourses.Count.ShouldBe(3);
        }

    }
}
