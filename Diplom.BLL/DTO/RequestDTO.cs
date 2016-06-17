using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diplom.BLL.DTO
{
  public class RequestDTO
  {
    public int Id { get; set; }
    public int From { get; set; }
    public int To { get; set; }
    public string Description { get; set; }
  }
}
