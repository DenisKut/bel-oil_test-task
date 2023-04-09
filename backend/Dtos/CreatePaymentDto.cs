using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace backend.Dtos
{
    public class CreatePaymentDto
    {
      public DateTime DateOfPayment { get; set; }
      public string TypeOfPayment { get; set; }
      public long ChildId { get; set; }
    }
}