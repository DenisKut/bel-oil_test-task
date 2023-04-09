using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace backend.Dtos
{
  public class CreateGroupDto
  {
    public string Name { get; set; }
    public long EducatorId { get; set; }
  }
}