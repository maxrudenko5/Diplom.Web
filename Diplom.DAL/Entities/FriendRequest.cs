using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diplom.DAL.Entities
{
  public class FriendRequest
  {
    public int Id { get; set; }
    public int From { get; set; }
    public int To { get; set; }
    public int Confirmed { get; set; }
  }
}
