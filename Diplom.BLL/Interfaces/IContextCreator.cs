using Diplom.DAL.Interfaces;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diplom.BLL.Interfaces
{
  public interface IContextCreator
  {
    IUnitOfWork CreateContext(string connection);
  }
}
