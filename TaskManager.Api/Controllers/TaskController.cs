using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskManager.Application.Contracts;
using TaskManager.Application.DTOs;
using TaskManager.Application.Features.Tasks.Commads.CreateTask;
using TaskManager.Application.Features.Tasks.Commads.DeleteTask;
using TaskManager.Application.Features.Tasks.Commads.UpdateTask;
using TaskManager.Application.Features.Tasks.Queries.GetTaskDetails;
using TaskManager.Application.Features.Tasks.Queries.GetUserTasksList;
using TaskManager.Domain.Enums;

namespace TaskManager.Api.Controllers
{
    [ApiController]
    [Route("tasks")]
    public class TaskController(IMediator mediator, ICurrentUserService currentUserService) : Controller
    {
        [Authorize]
        [HttpGet(Name = "GetUserTasks")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<TaskListVm>> GetUserTasks(
            [FromQuery] Status? status,
            [FromQuery] DateTime? dueDate,
            [FromQuery] Priority? priority,
            [FromQuery] TaskSortFieldDto sortBy = TaskSortFieldDto.DueDate,
            [FromQuery] bool ascending = true,
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 10)
        {
            var query = new GetUserTasksListQuery
            {
                UserId = Guid.Parse(currentUserService.UserId),
                Status = status,
                DueDate = dueDate,
                Priority = priority,
                SortBy = sortBy,
                Ascending = ascending,
                PageNumber = pageNumber,
                PageSize = pageSize
            };

            var result = await mediator.Send(query);
            return Ok(result);
        }
        [Authorize]
        [HttpPost(Name = "AddTask")]
        public async Task<ActionResult<Guid>> Create([FromBody] CreateTaskCommand createTaskCommand)
        {
            var id = await mediator.Send(createTaskCommand);
            return Ok(id);
        }

        [Authorize]
        [HttpPut(Name = "UpdateTask")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> Update([FromBody] UpdateTaskCommand updateTaskCommand)
        {
            await mediator.Send(updateTaskCommand);
            return NoContent();
        }

        [Authorize]
        [HttpDelete("{id}", Name = "DeleteTask")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> Delete(Guid id)
        {
            var deleteTaskCommand = new DeleteTaskCommand() { Id = id };
            await mediator.Send(deleteTaskCommand);
            return NoContent();
        }

        [Authorize]
        [HttpGet("{id}", Name = "GetTaskById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<TaskDetailVm>> GetTaskById(Guid id)
        {
            var getTaskDetailQuery = new GetTaskDetailQuery() { Id = id };
            return Ok(await mediator.Send(getTaskDetailQuery));
        }
    }
}
