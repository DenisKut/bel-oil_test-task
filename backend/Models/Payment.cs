namespace backend.Models
{
  public class Payment : BaseEntity
  {
    public DateTime DateOfPayment { get; set; }
    public string TypeOfPayment { get; set; }
    public double PaymentAmount { get; set; }
    public long ChildId { get; set; }

    public Child Child { get; set; }
  }
}