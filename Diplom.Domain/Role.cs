using Diplom.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diplom.Domain
{
  public class Role : BaseEntity
  {
    public string Name { get; set; }
    public Permission Permission { get; set; }
    public bool isActive { get; set; }
  }
}
