
namespace backend.Models
{
  public class Child : BaseEntity
  {
    public string Name { get; set; }
    public int Age { get; set; }
    public string Passport { get; set; }
    public string StateOfHealth { get; set; }
    public long GroupId { get; set; }
    public double Weight { get; set; }
    public double Growth { get; set; }
    public long ParentInfoId { get; set; }
    public long ContractId { get; set; }
    public string Region { get; set; }
    public bool DirtyZone { get; set; }

    public Group Group { get; set; }
    public ParentInfo ParentInfo { get; set; }
    public Contract Contract { get; set; }
    public List<Payment> Payments { get; set; }
  }
}