using Diplom.BLL.DTO;
using Diplom.BLL.Infrastructure;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Diplom.BLL.Interfaces
{
  public interface IUserService :IDisposable
  {
    UserDTO SearchUser(string userName);
    OperationDetails ConfirmedUser(UserDTO model);
    UserDTO GetUnconfirmedUser();
    void SetInitialData(UserDTO adminDto);
    OperationDetails Create(UserDTO userDto);
    HttpCookie Authenticate(UserDTO userDto);
    UserDTO GetUser(string login);
    OperationDetails Edit(UserDTO userDto);
    List<UserDTO> GetUsers(int lastId=-1,int count=10);
  }
}
