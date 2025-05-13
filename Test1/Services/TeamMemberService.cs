using Microsoft.Data.SqlClient;
using Test1.Entities;
using Test1.Services.Abstractions;

namespace Test1.Services;

public class TeamMemberService : ITeamMemberService
{
    private string? _connectionString;
    public TeamMemberService(IConfiguration cfg)
    {
        _connectionString = cfg.GetConnectionString("Default");
    }
    public async Task<TeamMember> GetMemberInfoAsync(int idTeamMember, CancellationToken token)
    {
        const string query = "SELECT IdTeamMember, FirstName, LastName, Email FROM TeamMember WHERE IdTeamMember = @idMember";
        TeamMember teamMember = null;
        using (SqlConnection connection = new SqlConnection(_connectionString))
        using (SqlCommand command = new SqlCommand(query, connection))
        {
            await connection.OpenAsync(token);
            command.Parameters.AddWithValue("@idMember", idTeamMember);
 
            using (SqlDataReader reader = await command.ExecuteReaderAsync(token))
            {
                while (await reader.ReadAsync(token))
                {
                    // I forgot how to parse only first result
                    teamMember = new TeamMember
                    {
                        IdTeamMember = reader.GetInt32(0),
                        FirstName = reader.GetString(1),
                        LastName = reader.GetString(2),
                        Email = reader.GetString(3),
                    };
                    
                }
            }
        }

        return teamMember;
    }
}