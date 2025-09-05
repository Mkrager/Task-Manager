using Microsoft.Extensions.Logging;
using Moq;
using Shouldly;
using TaskManager.Application.Contracts.Application;
using TaskManager.Application.Contracts.Persistance;
using TaskManager.Application.Features.Tasks.Commads.DeleteTask;
using TaskManager.Application.UnitTests.Mocks;

namespace TaskManager.Application.UnitTests.Tasks.Commands
{
    public class DeleteTaskCommandTest
    {
        private readonly Mock<ITaskRepository> _mockTaskRepository;
        private readonly Mock<ILogger<DeleteTaskCommandHandler>> _mockLoggerService;
        private readonly Mock<IPermissionService> _mockPermissionService;

        public DeleteTaskCommandTest()
        {
            _mockTaskRepository = TaskRepositoryMock.GetTaskRepository();
            _mockPermissionService = PermissionServiceMock.GetPermissionService();
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

        [Fact]
        public async void Validator_ShouldHaveError_WhenUserDontHavePermission()
        {
            var validator = new DeleteTaskCommandValidator(_mockTaskRepository.Object, _mockPermissionService.Object);
            var query = new DeleteTaskCommand
            {
                Id = Guid.Parse("b8c3f27a-7b28-4ae6-94c2-91fdc33b77e8"),
                UserId = Guid.Parse("6b6da9a2-1f8b-4676-84b9-baf714601231")
            };

            var result = await validator.ValidateAsync(query);

            Assert.False(result.IsValid);
            Assert.Contains("You don't have permission", result.Errors.Select(e => e.ErrorMessage));
        }
    }
}
