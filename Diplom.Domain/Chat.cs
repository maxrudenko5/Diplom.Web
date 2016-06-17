using Diplom.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diplom.Domain
{
  public class Chat : BaseEntity
  {
    public string Photo { get; set; }
    public string Name { get; set; }
    public int isActive { get; set; }
  }
}
