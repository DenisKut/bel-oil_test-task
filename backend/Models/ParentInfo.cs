namespace backend.Models
{
  public class ParentInfo
  {
    public long Id { get; set; }
    public string Mother { get; set; }
    public string Father { get; set; }
    public string FatherProfession { get; set; }
    public string MotherProfession { get; set; }
    public int FatherAge { get; set; }
    public int MotherAge { get; set; }

    public IEnumerable<Child> Children { get; set; }
  }
}