using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace backend.Dtos
{
    public class CreateChildDto
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
    }
}