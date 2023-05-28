namespace Entities.Models;
public class Agency : EntityBase
{
    public string Name { get; set; }
    public DateTime CreatedDate { get; set; }

    public User User { get; set; }
    public string UserId { get; set; }

    public IEnumerable<ImaginaryAgent> ImaginaryAgents { get; set; }
}
