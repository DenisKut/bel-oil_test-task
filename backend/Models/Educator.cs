namespace backend.Models
{
  public class Educator
  {
    public long Id { get; set; }
    public string Name { get; set; }
    public int Age { get; set; }
    public string Email { get; set; }

    public IEnumerable<Group> Groups { get; set; }
  }
}