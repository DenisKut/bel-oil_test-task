namespace backend.Models
{
  public class Educator : BaseEntity
  {
    public string Name { get; set; }
    public int Age { get; set; }
    public string Email { get; set; }

    public IEnumerable<Group> Groups { get; set; }
  }
}