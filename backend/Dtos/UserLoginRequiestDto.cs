using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace backend.Dtos
{
    public class UserLoginRequiestDto
    {
      [Required]
      public string Email { get; set; }

      [Required]
      public string Password { get; set; }
    }
}