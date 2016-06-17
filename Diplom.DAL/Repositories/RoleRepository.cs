using Diplom.DAL.EF;
using Diplom.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diplom.DAL.Repositories
{
  public class RoleRepository
  {
    private ApplicationContext context;
    public RoleRepository(ApplicationContext context)
    {
      this.context = context;
    }
    public Role Get(int id)
    {
      return context.Roles.FirstOrDefault(u => u.Id == id);
    }
    public void AddToRole(Role role)
    {
      context.Roles.Add(role);
      context.SaveChanges();
    }
    public void Update(Role role)
    {
      context.Entry(role).State = EntityState.Modified;
    }
  }
}
