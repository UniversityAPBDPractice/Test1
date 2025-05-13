using Test1.Entities;

namespace Test1.Services.Abstractions;

public interface ITeamMemberService
{
    Task<TeamMember> GetMemberInfoAsync(int idTeamMember, CancellationToken token = default);
}