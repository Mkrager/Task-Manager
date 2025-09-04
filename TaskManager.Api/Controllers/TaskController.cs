using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskManager.Application.Contracts;
using TaskManager.Application.Features.Tasks.Commads.CreateTask;
using TaskManager.Application.Features.Tasks.Queries.GetUserTasksList;

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
        public async Task<ActionResult<List<TaskListVm>>> GetUserTasks()
        {
            var dtos = await mediator.Send(new GetUserTasksListQuery()
            {
                UserId = Guid.Parse(currentUserService.UserId)
            });
            return Ok(dtos);
        }

        [Authorize]
        [HttpPost(Name = "AddTask")]
        public async Task<ActionResult<Guid>> Create([FromBody] CreateTaskCommand createTaskCommand)
        {
            var id = await mediator.Send(createTaskCommand);
            return Ok(id);
        }

    }
}
