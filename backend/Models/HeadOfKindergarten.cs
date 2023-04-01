namespace backend.Models
{
  public class HeadOfKindergarten
  {
    public long Id { get; set; }
    public string Name { get; set; }
    public int Age { get; set; }
    public string Email { get; set; }

    public List<Contract> Contracts { get; set; }
  }
}