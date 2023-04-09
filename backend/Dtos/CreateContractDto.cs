using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace backend.Dtos
{
  public class CreateContractDto
  {
    public long HeadId { get; set; }
    public DateTime Date { get; set; }
    public string Description { get; set; }
  }
}