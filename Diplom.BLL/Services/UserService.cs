using AutoMapper;
using Diplom.BLL.DTO;
using Diplom.BLL.Infrastructure;
using Diplom.BLL.Interfaces;
using Diplom.DAL.EF;
using Diplom.DAL.Entities;
using Diplom.DAL.Interfaces;
using Diplom.DAL.Repositories;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Security;

namespace Diplom.BLL.Services
{
  public class UserService : IUserService
  {
    IUnitOfWork Database { get; set; }

    public UserService(IUnitOfWork uow)
    {
      Database = uow;
    }
    public static IUserService Create(IdentityFactoryOptions<IUserService> options,
                                            IOwinContext context)
    {
      var database = context.Get<IUnitOfWork>();
      var service = new UserService(database);
      return service;
    }
    public OperationDetails ConfirmedUser(UserDTO model)
    {
      var user=Database.UserManager.Get(model.Id);
      if(user!=null)
      {
        user.Confirmed = 1;
        user.Role.RoleId = model.Role.RoleId;
        Database.RoleManager.Update(user.Role);
        Database.UserManager.Update(user);
        Database.Save();
        return new OperationDetails(true, "Пользователь подтвержден", "");
      }
      return new OperationDetails(false, "Пользователь с таким id не найден", "id");
    }
    public UserDTO GetUnconfirmedUser()
    {
      var user=Database.UserManager.GetFirstUnconfirmed();
      return Mapper.Map<User,UserDTO>(user);
    }
    public List<UserDTO> GetUsers(int lastId=-1, int count = 10)
    {
      var userList = new List<UserDTO>();
      var users = Database.UserManager.GetAll(lastId, count);
      foreach (var item in users)
      {
        userList.Add(Mapper.Map<User, UserDTO>(item));
      }
      return userList;
    }
    public UserDTO SearchUser(string userName)
    {
      var user = Database.UserManager.GetByUserName(userName);
      var userDto = Mapper.Map<User, UserDTO>(user);
      return userDto;
    }
    public UserDTO GetUser(string userName)
    {
      var user = Database.UserManager.GetByUserName(userName);
      var userDto = Mapper.Map<User, UserDTO>(user);
      return userDto;
    }
    private HttpCookie GetAuthTicket(string userName, string role, bool rememberMe=true)
    {
      var authTicket = new FormsAuthenticationTicket(
        10, // Version
        userName, // User Login
        DateTime.Now, // Issue-Date
        DateTime.Now.AddMinutes(FormsAuthentication.Timeout.TotalMinutes), // Expiration
        rememberMe, //TODO: Remember me
        role, // User Role
        FormsAuthentication.FormsCookiePath // Cookie Path
        );
      var authCookie = new HttpCookie(FormsAuthentication.FormsCookieName, FormsAuthentication.Encrypt(authTicket));
      if (authTicket.IsPersistent)
        authCookie.Expires = authTicket.Expiration;

      return authCookie;
    }

    public OperationDetails Create(UserDTO userDto)
    {
      var email = Database.UserManager.GetByEmail(userDto.Email);
      var userName = Database.UserManager.GetByUserName(userDto.UserName);
      if (userName == null && email == null)
      {
        var user = Mapper.Map<UserDTO, User>(userDto);
        user.PasswordHash = userDto.Password.GetHash();
        var profile = new DAL.Entities.Profile
        {
          Id = user.Id,
          FirstName = userDto.Profile.FirstName,
          LastName = userDto.Profile.LastName,
          Patronymic = userDto.Profile.Patronomic,
          BirthDate = DateTime.Parse(userDto.Profile.BirthDate),
          Sex = userDto.Profile.Sex
        };
        user.Profile = profile;
        Database.UserManager.Create(user);
        return new OperationDetails(true, "Регистрация успешно пройдена", "");
      }
      return new OperationDetails(false, "Пользователь с таким логином уже существует", "Email");
    }
    public OperationDetails Edit(UserDTO userDto)
    {
      var user = Database.UserManager.GetByUserName(userDto.UserName);
      if (user == null)
      {
        return new OperationDetails(false, "Пользватель с таким UserName не найден", "UserName");
      }
      user.Confirmed = userDto.Confirmed;
      user.Role.RoleId = userDto.Role.RoleId;
      Database.RoleManager.Update(user.Role);
      Database.UserManager.Update(user);
      Database.Save();
      return new OperationDetails(true, "Изменения внесены успешно!", "");
    }

    public HttpCookie Authenticate(UserDTO userDto)
    {
      HttpCookie cookie = null;
      // находим пользователя
      var user = Database.UserManager.Find(userDto.UserName, userDto.Password.GetHash());
      // авторизуем его и возвращаем объект HttpCookie
      if (user != null)
      {
        return GetAuthTicket(user.UserName, user.Role.RoleId.ToString());
      }
      return cookie;
    }
    // начальная инициализация бд
    public void SetInitialData(UserDTO adminDto)
    {
      Create(adminDto);
    }

    public void Dispose()
    {
      //Database.Dispose();
    }
  }
}
