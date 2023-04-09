using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace backend.Dtos
{
  public class CreateParentsDto
  {
    public string Mother { get; set; }
    public string Father { get; set; }
    public string FatherProfession { get; set; }
    public string MotherProfession { get; set; }
    public int FatherAge { get; set; }
    public int MotherAge { get; set; }
  }
}