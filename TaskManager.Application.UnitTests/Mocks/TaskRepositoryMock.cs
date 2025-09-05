using Moq;
using TaskManager.Application.Contracts.Persistance;
using TaskManager.Domain.Enums;

namespace TaskManager.Application.UnitTests.Mocks
{
    public class TaskRepositoryMock
    {
        public static Mock<ITaskRepository> GetTaskRepository()
        {
            var tasks = new List<Domain.Entities.Task>
            {
                new Domain.Entities.Task
                {
                    Id = Guid.Parse("b8c3f27a-7b28-4ae6-94c2-91fdc33b77e8"),
                    Title = "Test Task 1",
                    Description = "Description for task 1",
                    DueDate = DateTime.UtcNow.AddDays(3),
                    Priority = Priority.Medium,
                    Status = Status.Pending,
                    UserId = Guid.NewGuid()
                },
                new Domain.Entities.Task
                {
                    Id = Guid.Parse("b8c3f27a-7b28-4ae6-94c2-91fdc33b77e2"),
                    Title = "Test Task 2",
                    Description = "Description for task 2",
                    DueDate = DateTime.UtcNow.AddDays(5),
                    Priority = Priority.High,
                    Status = Status.InProgress,
                    UserId = Guid.NewGuid()
                },
                new Domain.Entities.Task
                {
                    Id = Guid.Parse("b8c3f27a-7b28-4ae6-94c2-91fdc33b21e8"),
                    Title = "Test Task 3",
                    Description = "Description for task 3",
                    DueDate = DateTime.UtcNow.AddDays(1),
                    Priority = Priority.Low,
                    Status = Status.Completed,
                    UserId = Guid.NewGuid()
                },
                new Domain.Entities.Task
                {
                    Id = Guid.Parse("b8c3f27a-7b28-5ae6-94c2-91fdc33b77e2"),
                    Title = "Test Task 4",
                    Description = "Description for task 4",
                    DueDate = DateTime.UtcNow.AddDays(7),
                    Priority = Priority.Low,
                    Status = Status.Pending,
                    UserId = Guid.NewGuid()
                }
            };

            var mockRepository = new Mock<ITaskRepository>();

            mockRepository.Setup(r => r.ListAllAsync())
                .ReturnsAsync(tasks);

            mockRepository.Setup(r => r.GetByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync((Guid id) => tasks.FirstOrDefault(x => x.Id == id));

            mockRepository.Setup(r => r.AddAsync(It.IsAny<Domain.Entities.Task>()))
                .ReturnsAsync((Domain.Entities.Task task) =>
                {
                    tasks.Add(task);
                    return task;
                });

            mockRepository.Setup(r => r.UpdateAsync(It.IsAny<Domain.Entities.Task>()))
                .Callback((Domain.Entities.Task task) =>
                {
                    var oldTask = tasks.FirstOrDefault(x => x.Id == task.Id);
                    if (oldTask != null)
                    {
                        oldTask.Title = task.Title;
                        oldTask.Description = task.Description;
                        oldTask.Status = task.Status;
                        oldTask.Priority = task.Priority;
                        oldTask.DueDate = task.DueDate;
                    }
                });

            mockRepository.Setup(r => r.DeleteAsync(It.IsAny<Domain.Entities.Task>()))
                .Callback((Domain.Entities.Task task) => tasks.Remove(task));

            return mockRepository;
        }
    }
}
