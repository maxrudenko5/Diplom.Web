using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diplom.DAL.Entities
{
  public abstract class BaseEntity
  {
    [Key]
    public int Id { get; set; }

    public DateTime CreationDate
    {
      get;
      protected set;
    }

    protected BaseEntity()
    {
      DateTime now = DateTime.Now;
      CreationDate = new DateTime(now.Ticks / 100000 * 100000, now.Kind);
    }
  }
}
