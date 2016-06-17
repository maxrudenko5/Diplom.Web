using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diplom.DAL.Entities
{
  public class Chat
  {
    public int Id { get; set; }
    public string Users { get; set; }
    public string Photo { get; set; }
    public string Name { get; set; }
    public int Deleted { get; set; }
  }
}
