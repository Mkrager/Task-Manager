using AutoMapper;
using Microsoft.Extensions.Logging;
using Moq;
using Shouldly;
using TaskManager.Application.Contracts.Persistance;
using TaskManager.Application.Features.Tasks.Commads.CreateTask;
using TaskManager.Application.Profiles;
using TaskManager.Application.UnitTests.Mocks;
using TaskManager.Domain.Enums;

namespace TaskManager.Application.UnitTests.Tasks.Commands
{
    public class CreateTaskCommandTest
    {
        private readonly IMapper _mapper;
        private readonly Mock<ITaskRepository> _mockTaskRepository;
        private readonly Mock<ILogger<CreateTaskCommandHandler>> _mockLoggerService;
        public CreateTaskCommandTest()
        {
            _mockTaskRepository = TaskRepositoryMock.GetTaskRepository();
            _mockLoggerService = LoggerServiceMock.GetLoggerService<CreateTaskCommandHandler>();
            var configurationProvider = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });
            _mapper = configurationProvider.CreateMapper();
        }

        [Fact]
        public async Task Should_Update_Task_Successfully()
        {
            var handler = new CreateTaskCommandHandler(_mapper, _mockTaskRepository.Object, _mockLoggerService.Object);

            var command = new CreateTaskCommand
            {
                Description = "CreatedTitleCreatedTitleCreatedTitle",
                Title = "CreatedTitle",
                Priority = Priority.Medium,
                Status = Status.Pending,
                DueDate = DateTime.UtcNow.AddDays(1)
            };

            await handler.Handle(command, CancellationToken.None);

            var allTasks = await _mockTaskRepository.Object.ListAllAsync();
            allTasks.Count.ShouldBe(5);

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
            var validator = new CreateTaskCommandValidator();
            var query = new CreateTaskCommand
            {
                Description = "CreatedTitleCreatedTitleCreatedTitle",
                Title = "",
                Priority = Priority.Medium,
                Status = Status.Pending,
                DueDate = DateTime.UtcNow.AddDays(1)
            };

            var result = await validator.ValidateAsync(query);

            Assert.False(result.IsValid);
            Assert.Contains(result.Errors, f => f.PropertyName == "Title");
        }
    }
}
