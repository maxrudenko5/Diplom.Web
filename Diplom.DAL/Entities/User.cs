using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Diplom.DAL.Entities
{
  public class User
  {
    public int Id { get; set; }
    public string UserName { get; set; }
    public string PasswordHash { get; set; }
    public string Email { get; set; }
    public int Confirmed { get; set; }
    public Profile Profile { get; set; }
    public Role Role { get; set; }
  }
}