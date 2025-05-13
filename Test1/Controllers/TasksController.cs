using Microsoft.AspNetCore.Mvc;
using Test1.Entities;
using Test1.Services.Abstractions;

namespace Test1.Controllers;

[ApiController]
[Route("/api/")]
public class TasksController : ControllerBase
{
    private ITasksService _tasksService;
    private ITeamMemberService _teamMemberService;
    public TasksController(
        ITasksService tasksService,
        ITeamMemberService teamMemberService
        )
    {
        _tasksService = tasksService;
        _teamMemberService = teamMemberService;
    }
    [HttpGet]
    [Route("tasks/{idMember:int}")]
    public async Task<IActionResult> GetTeamMemberAndTasksAsync([FromRoute] int idMember, CancellationToken token)
    {
        try
        {
            var tasks = await _tasksService.GetTeamMemberTasksAsync(idMember, token);
            var member = await _teamMemberService.GetMemberInfoAsync(idMember, token);
            var memberTasks = new MemberDTO()
            {
                Tasks = tasks,
                TeamMember = member,
            };

            return Ok(memberTasks);
        }
        catch (Exception)
        {
            return StatusCode(500);
        }
    }

    [HttpDelete]
    [Route("api/projects/{idProject:int}")]
    public async Task<IActionResult> DeleteProjectAsync([FromRoute] int idProject, CancellationToken token)
    {
        bool success = await _tasksService.DeleteProjectAsync(idProject, token);
        if (success) {return Ok(); }
        return StatusCode(500);
    }
}