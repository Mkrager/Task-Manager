using Moq;
using TaskManager.Application.Contracts.Persistance;
using TaskManager.Application.DTOs;
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
                    UserId = Guid.Parse("6b6da9a2-1f8b-4676-84b9-baf714600217")
                },
                new Domain.Entities.Task
                {
                    Id = Guid.Parse("b8c3f27a-7b28-4ae6-94c2-91fdc33b77e2"),
                    Title = "Test Task 2",
                    Description = "Description for task 2",
                    DueDate = DateTime.UtcNow.AddDays(5),
                    Priority = Priority.High,
                    Status = Status.InProgress,
                    UserId = Guid.Parse("6b6da9a2-1f8b-4676-84b9-baf714600217")
                },
                new Domain.Entities.Task
                {
                    Id = Guid.Parse("b8c3f27a-7b28-4ae6-94c2-91fdc33b21e8"),
                    Title = "Test Task 3",
                    Description = "Description for task 3",
                    DueDate = DateTime.UtcNow.AddDays(1),
                    Priority = Priority.Low,
                    Status = Status.Completed,
                    UserId = Guid.Parse("6b6da9a2-1f8b-4676-84b9-baf714600217")
                },
                new Domain.Entities.Task
                {
                    Id = Guid.Parse("b8c3f27a-7b28-5ae6-94c2-91fdc33b77e2"),
                    Title = "Test Task 4",
                    Description = "Description for task 4",
                    DueDate = DateTime.UtcNow.AddDays(7),
                    Priority = Priority.Low,
                    Status = Status.Pending,
                    UserId = Guid.Parse("6b6da9a2-1f8b-4676-84b9-baf714600217")
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


            mockRepository.Setup(r => r.GetTasksByUserIdAsync(
                It.IsAny<Guid>(),
                It.IsAny<Status?>(),
                It.IsAny<DateTime?>(),
                It.IsAny<Priority?>(),
                It.IsAny<TaskSortFieldDto>(),
                It.IsAny<bool>(),
                It.IsAny<int>(),
                It.IsAny<int>()))
            .ReturnsAsync((Guid userId, Status? status, DateTime? dueDate, Priority? priority,
                           TaskSortFieldDto sortBy, bool ascending, int pageNumber, int pageSize) =>
            {
                var query = tasks.AsQueryable().Where(t => t.UserId == userId);

                if (status.HasValue)
                    query = query.Where(t => t.Status == status.Value);

                if (dueDate.HasValue)
                    query = query.Where(t => t.DueDate.Value.Date == dueDate.Value.Date);

                if (priority.HasValue)
                    query = query.Where(t => t.Priority == priority.Value);

                query = sortBy switch
                {
                    TaskSortFieldDto.DueDate => ascending ? query.OrderBy(t => t.DueDate) : query.OrderByDescending(t => t.DueDate),
                    TaskSortFieldDto.Priority => ascending ? query.OrderBy(t => t.Priority) : query.OrderByDescending(t => t.Priority),
                    _ => query.OrderBy(t => t.CreatedAt)
                };

                var totalCount = query.Count();

                var tasksResult = query
                    .Skip((pageNumber - 1) * pageSize)
                    .Take(pageSize)
                    .ToList();

                return new GetTasksByUserIdResponse
                {
                    Tasks = tasksResult,
                    TotalCount = totalCount
                };
            });

            return mockRepository;
        }
    }
}
