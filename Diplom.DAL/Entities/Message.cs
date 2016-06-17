using Diplom.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diplom.DAL.Entities
{
  public class Message
  {
    public int Id { get; set; }
    public int IdChat { get; set; }
    public string From { get; set; }
    public string Text { get; set; }
    public DateTime Time { get; set; }
    public int Read { get; set; }
    public int Deleted { get; set; }
  }
}
