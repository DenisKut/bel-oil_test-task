using System.Linq;
using System.Threading.Tasks;

namespace backend.Models
{
    public class UserInfoRes
    {
      public string Token { get; set; }
      public bool Result { get; set; }
      public List<string> Errors { get; set; }
      public string Email { get;set; }
      public string UserId { get; set; }
      public string Name { get; set; }
    }
}