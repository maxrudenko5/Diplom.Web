using Diplom.DAL.EF;
using Diplom.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diplom.DAL.Interfaces
{
  public interface IUnitOfWork : IDisposable
  {
    UsersRepository UserManager { get; }
    ProfileRepository ProfileManager { get; }
    RoleRepository RoleManager { get; }
    Task SaveAsync();
    void Save();
  }
}
