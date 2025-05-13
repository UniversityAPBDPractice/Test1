using System.ComponentModel.DataAnnotations;

namespace Test1.Entities;

public class MemberTask
{
    [Key]
    public int IdTask { get; set; }
    
    [MaxLength(100)]
    public string Name { get; set; }
    
    [MaxLength(100)]
    public string Description { get; set; }
    
    public DateTime Deadline{ get; set; }
    
    [Key]
    public int idProject { get; set; }
    
    [Key]
    public int idTaskType { get; set; }
    
    [Key]
    public int idAssignedTo { get; set; }
    
    [Key]
    public int idCreator { get; set; }
}