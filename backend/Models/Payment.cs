namespace backend.Models
{
  public class Payment
  {
    public long Id { get; set; }
    public DateTime DateOfPayment { get; set; }
    public string TypeOfPayment { get; set; }
    public double PaymentAmount { get; set; }
    public int ChildId { get; set; }

    public Child Child { get; set; }
  }
}