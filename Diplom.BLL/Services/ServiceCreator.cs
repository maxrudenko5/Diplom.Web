using Diplom.BLL.Interfaces;
using Diplom.DAL.Interfaces;
using Diplom.DAL.Repositories;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diplom.BLL.Services
{
  public class ContextCreator : IContextCreator
  {

    public IUnitOfWork CreateContext(string connection)
    {
      return new UnitOfWork(connection);
    }
  }
}
