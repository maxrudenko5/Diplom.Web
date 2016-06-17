using Diplom.DAL.EF;
using Diplom.DAL.Entities;
using Diplom.DAL.Interfaces;
using Diplom.DAL.Repositories;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diplom.DAL.Repositories
{
  public class UnitOfWork : IUnitOfWork
  {
    private ApplicationContext db;
    private RoleRepository roleRepository;
    private UsersRepository userRepository;
    private ProfileRepository profileRepository;
    public UnitOfWork(ApplicationContext context)
    {
      db = context;
    }
    public UnitOfWork(string connectionString)
    {
      db = new ApplicationContext(connectionString);
    }
    public RoleRepository RoleManager {
      get
      {
        if (roleRepository == null)
        {
          roleRepository = new RoleRepository(db);
        }
        return roleRepository;
      }
    }
    public UsersRepository UserManager {
      get
      {
        if (userRepository == null)
        {
          userRepository = new UsersRepository(db);
        }
        return userRepository;
      }
    }

    public ProfileRepository ProfileManager {
      get
      {
        if (profileRepository == null)
        {
          profileRepository = new ProfileRepository(db);
        }
        return profileRepository;
      }
    }


    public async Task SaveAsync()
    {
      await db.SaveChangesAsync();
    }
    public void Save()
    {
      db.SaveChanges();
    }
    public void Dispose()
    {
     // Dispose(true);
     // GC.SuppressFinalize(this);
    }
    private bool disposed = false;

    public virtual void Dispose(bool disposing)
    {
      if (!this.disposed)
      {
        if (disposing)
        {
          //db?.Dispose();
        }
        //this.disposed = true;
      }
    }
  }
}
