using System.Data;
using Microsoft.Data.SqlClient;
using Test1.Entities;
using Test1.Services.Abstractions;

namespace Test1.Services;

public class TasksService : ITasksService
{
    private string? _connectionString;
    public TasksService(IConfiguration cfg)
    {
        _connectionString = cfg.GetConnectionString("Default");
    }
    public async Task<List<MemberTask>> GetTeamMemberTasksAsync(int idMember, CancellationToken token = default)
    {
        const string query = """
                            SELECT IdTask,
                                   Name,
                                   Description,
                                   Deadline,
                                   IdProject,
                                   IdTaskType,
                                   IdAssignedTo,
                                   IdCreator
                            FROM Task WHERE IdAssignedTo = @idMember
                            """; //IdCreator = @idMember ORDER BY Deadline DESC
        List<MemberTask> memberTasks = new List<MemberTask>();
        using (SqlConnection con = new SqlConnection(_connectionString))
        using (SqlCommand cmd = new SqlCommand(query, con))
        {
            cmd.Parameters.AddWithValue("@idMember", idMember);
            await con.OpenAsync(token);
            
            using (SqlDataReader reader = await cmd.ExecuteReaderAsync(token))
            {
                // Read results asynchronously
                while (await reader.ReadAsync(token))
                {
                    MemberTask newMemberTask = new MemberTask
                    {
                        IdTask = reader.GetInt32(0),
                        Name = reader.GetString(1),
                        Description = reader.GetString(2),
                        Deadline = reader.GetDateTime(3),
                        idProject = reader.GetInt32(4),
                        idTaskType = reader.GetInt32(5),
                        idAssignedTo = reader.GetInt32(6),
                        idCreator = reader.GetInt32(7),
                    };
                    // // Add project names
                    // const string queryName = "SELECT Name FROM Project WHERE IdProject = @idProject";
                    // using (SqlCommand cmd2 = new SqlCommand(query, con))
                    // {
                    //     cmd2.Parameters.AddWithValue("@idProject", newMemberTask.idProject);
                    //     string ProjectName = (await cmd2.ExecuteScalarAsync(token))?.ToString();
                    //     if (ProjectName == null) continue;
                    //     newMemberTask.Name = ProjectName;
                    // }
                    
                    memberTasks.Add(newMemberTask);
                    
                }
            }
        }
        return memberTasks;
    }
}