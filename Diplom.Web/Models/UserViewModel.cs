using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Diplom.Web.Models
{
  public class UserViewModel
  {
    public int Id { get; set; }
    public string UserName { get; set; }
    public string Password { get; set; }
    public RoleViewModel Role { get; set; }
    public int Confirmed { get; set; }
    public string Email { get; set; }
    public ProfileViewModel Profile { get; set; }
  }
}