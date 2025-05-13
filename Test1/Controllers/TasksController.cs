using Microsoft.AspNetCore.Mvc;
using Test1.Entities;
using Test1.Services.Abstractions;

namespace Test1.Controllers;

[ApiController]
[Route("/api/tasks/")]
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
    [Route("/{idMember:int}")]
    public async Task<IActionResult> GetTeamMemberAndTasksAsync([FromRoute] int idMember, CancellationToken token)
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
}