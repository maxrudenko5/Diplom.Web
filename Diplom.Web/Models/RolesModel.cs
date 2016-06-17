using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Diplom.Web.Models
{
  public class DefaultRoles
  {
    public const string Admin="1";
    public const string Teacher = "2";
    public const string Student = "3";
    public const string Anonymous = "4";
    public static string GetRoles(int id)
    {
      switch (id)
      {
        case 1:
          return nameof(Admin);
        case 2:
          return nameof(Teacher);
        case 3:
          return nameof(Student);
        default:
          return nameof(Anonymous);
      }
    }
    public static List<ItemRole> GetList()
    {
      var list = new List<ItemRole>();
      list.Add(new ItemRole() {RoleId=1,Name= "admin"});
      list.Add(new ItemRole() { RoleId = 2, Name = "teacher" });
      list.Add(new ItemRole() { RoleId = 3, Name = "student" });
      list.Add(new ItemRole() { RoleId = 4, Name = "anonymous" });
      return list;
    }
  }
  public class ItemRole
  {
    public int RoleId { get; set; }
    public string Name { get; set; }
  }
}