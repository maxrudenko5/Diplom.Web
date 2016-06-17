using Diplom.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diplom.BLL.DTO
{
  public class MessageDTO
  {
    public int Id { get; set; }
    public int? IdChat { get; set; }
    public string From { get; set; }
    public string Trash { get; set; }
    public string Text { get; set; }
    public DateTime Time { get; set; }
    public int Read { get; set; }
    public string Type { get; set; }
    public Chat Chat { get; set; }
  }
}
