namespace backend.Models
{
  public class Contract
  {
    public long Id { get; set; }
    public long HeadId { get; set; }
    public DateTime Date { get; set; }
    public string Description { get; set; }

    public HeadOfKindergarten HeadOfKindergarten { get; set; }
    public IEnumerable<Child> Children { get; set; }
  }
}