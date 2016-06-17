using Diplom.Base;
using Diplom.Domain;
using Diplom.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diplom.DataAccess.Repositories
{
  public class ProfileRepository : BaseRepository<Profile>
  {
    public ProfileRepository(IApplicationContext context)
            : base(context as DbContext)
    {

    }
  }
}
