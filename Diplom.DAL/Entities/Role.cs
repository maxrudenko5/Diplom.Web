using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diplom.DAL.Entities
{
  public class Role
  {
    [Key]
    [ForeignKey("User")]
    public int Id { get; set; }
    public int RoleId { get; set; }
    public User User { get; set; }
  }
}
