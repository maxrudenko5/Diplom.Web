using Diplom.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Diplom.BLL.DTO
{
  public class UserDTO
  {
    public int Id { get; set; }
    public string UserName { get; set; }
    public string Password { get; set; }
    public RoleDTO Role { get; set; }
    public int Confirmed { get; set; }
    public string Email { get; set; }
    public ProfileDTO Profile { get; set; }
  }
}