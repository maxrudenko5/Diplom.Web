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
  public class ProfileRepository
  {
    private ApplicationContext context;
    public ProfileRepository(ApplicationContext db)
    {
      context = db;
    }
    public int Count()
    {
      return context.Profiles.Count();
    }
    public IEnumerable<Profile> GetAll()
    {
      return context.Profiles;
    }
    public void Create(Profile profile)
    {
      context.Profiles.Add(profile);
      context.SaveChanges();
    }
    public void Delete(int id)
    {
      var prof = context.Profiles.Find(id);
      if (prof != null)
        context.Profiles.Remove(prof);
    }
    public void Update(Profile profile)
    {
      context.Entry(profile).State = EntityState.Modified;
      context.SaveChanges();
    }
    public IEnumerable<Profile> Get()
    {
      return context.Profiles;
    }
    public Profile Get(int id)
    {
      return context.Profiles.FirstOrDefault(profile => profile.Id==id);
    }
    private bool disposed = false;
    public virtual void Dispose(bool disposing)
    {
      if (!this.disposed)
      {
        if (disposing)
        {
          context.Dispose();
        }
      }
      this.disposed = true;
    }

    public void Dispose()
    {
      Dispose(true);
      GC.SuppressFinalize(this);
    }
  }
}
