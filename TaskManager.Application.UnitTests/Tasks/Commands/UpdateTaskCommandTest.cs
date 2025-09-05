using AutoMapper;
using Microsoft.Extensions.Logging;
using Moq;
using Shouldly;
using TaskManager.Application.Contracts.Application;
using TaskManager.Application.Contracts.Persistance;
using TaskManager.Application.Features.Tasks.Commads.UpdateTask;
using TaskManager.Application.Profiles;
using TaskManager.Application.UnitTests.Mocks;
using TaskManager.Domain.Enums;

namespace TaskManager.Application.UnitTests.Tasks.Commands
{
    public class UpdateTaskCommandTest
    {
        private readonly IMapper _mapper;
        private readonly Mock<ITaskRepository> _mockTaskRepository;
        private readonly Mock<ILogger<UpdateTaskCommandHandler>> _mockLoggerService;
        private readonly Mock<IPermissionService> _mockPermissionService;
        public UpdateTaskCommandTest()
        {
            _mockTaskRepository = TaskRepositoryMock.GetTaskRepository();
            _mockPermissionService = PermissionServiceMock.GetPermissionService();
            _mockLoggerService = LoggerServiceMock.GetLoggerService<UpdateTaskCommandHandler>();
            var configurationProvider = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });
            _mapper = configurationProvider.CreateMapper();
        }

        [Fact]
        public async Task Should_Create_Task_Successfully()
        {
            var handler = new UpdateTaskCommandHandler(_mockTaskRepository.Object, _mapper, _mockLoggerService.Object);

            var command = new UpdateTaskCommand
            {
                Id = Guid.Parse("b8c3f27a-7b28-4ae6-94c2-91fdc33b77e8"),
                Description = "UpdDesc",
                Title = "UpdTitile",
                Priority = Priority.Low,
                Status = Status.Completed,
                DueDate = DateTime.UtcNow.AddDays(2),
                UserId = Guid.Parse("6b6da9a2-1f8b-4676-84b9-baf714600217")
            };

            await handler.Handle(command, CancellationToken.None);

            var allTasks = await _mockTaskRepository.Object.ListAllAsync();

            var createdTask = allTasks.FirstOrDefault(a => a.Title == command.Title && a.Description == command.Description);
            createdTask.ShouldNotBeNull();
            createdTask.Title.ShouldBe(command.Title);
            createdTask.Description.ShouldBe(command.Description);
            createdTask.Priority.ShouldBe(command.Priority);
            createdTask.Status.ShouldBe(command.Status);
            createdTask.DueDate.ShouldBe(command.DueDate);
        }

        [Fact]
        public async void Validator_ShouldHaveError_WhenTitleEmpty()
        {
            var validator = new UpdateTaskCommandValidator(_mockTaskRepository.Object, _mockPermissionService.Object);
            var query = new UpdateTaskCommand
            {
                Id = Guid.Parse("b8c3f27a-7b28-4ae6-94c2-91fdc33b77e8"),
                Description = "UpdDesc",
                Title = "",
                Priority = Priority.Low,
                Status = Status.Completed,
                DueDate = DateTime.UtcNow.AddDays(2),
                UserId = Guid.Parse("6b6da9a2-1f8b-4676-84b9-baf714600217")
            };

            var result = await validator.ValidateAsync(query);

            Assert.False(result.IsValid);
            Assert.Contains(result.Errors, f => f.PropertyName == "Title");
        }        
        
        [Fact]
        public async void Validator_ShouldHaveError_WhenUserDontHavePermission()
        {
            var validator = new UpdateTaskCommandValidator(_mockTaskRepository.Object, _mockPermissionService.Object);
            var query = new UpdateTaskCommand
            {
                Id = Guid.Parse("b8c3f27a-7b28-4ae6-94c2-91fdc33b77e8"),
                Description = "UpdDesc",
                Title = "123",
                Priority = Priority.Low,
                Status = Status.Completed,
                DueDate = DateTime.UtcNow.AddDays(2),
                UserId = Guid.Parse("6b6da9a2-1f8b-4676-84b9-baf714601231")
            };

            var result = await validator.ValidateAsync(query);

            Assert.False(result.IsValid);
            Assert.Contains("You don't have permission", result.Errors.Select(e => e.ErrorMessage));
        }
    }
}
