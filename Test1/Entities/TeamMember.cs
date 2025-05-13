using System.ComponentModel.DataAnnotations;

namespace Test1.Entities;

public class TeamMember
{
    [Key]
    public int IdTeamMember { get; set; }
    
    [MaxLength(100)]
    public string FirstName { get; set; }
    
    [MaxLength(100)]
    public string LastName { get; set; }
    
    [MaxLength(100)]
    public string Email { get; set; }
    
}