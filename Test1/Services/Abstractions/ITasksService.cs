using Test1.Entities;

namespace Test1.Services.Abstractions;

public interface ITasksService
{
    Task<List<MemberTask>> GetTeamMemberTasksAsync(int idMember, CancellationToken cancellationToken);
}