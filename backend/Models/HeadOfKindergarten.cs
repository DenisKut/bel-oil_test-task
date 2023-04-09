namespace backend.Models
{
  public class HeadOfKindergarten : BaseEntity
  {
    public string Name { get; set; }
    public int Age { get; set; }
    public string Email { get; set; }

    public List<Contract> Contracts { get; set; }
  }
}