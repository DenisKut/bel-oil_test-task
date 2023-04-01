namespace backend.Models
{
  public class Group
  {
    public long Id { get; set; }
    public string Name { get; set; }
    public long EducatorId { get; set; }

    public Educator Educator { get; set; }
    public IEnumerable<Child> Children { get; set; }
  }
}