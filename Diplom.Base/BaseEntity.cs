using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diplom.Base
{
  public abstract class BaseEntity
  {
    private String id;
    [Key]
    [Required]
    public string Id
    {
      get
      {
        return id ?? (id = Guid.NewGuid().ToString());
      }
      set
      {
        id = value;
      }
    }

    [Required]
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
