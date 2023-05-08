using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace backend.Models
{
    public class UserUpdateModel
    {
      public string Email { get;set; }
      public string Name { get; set; }
      public string UserId { get; set; }
    }
}