using Test1.Services.Abstractions;

namespace Test1.Services;

public static class DefaultServices
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<ITasksService, TasksService>();
        services.AddScoped<ITeamMemberService, TeamMemberService>();
        return services;
    }
}