using Diplom.BusinessLogic;
using Diplom.DataAccess.Repositories;
using Diplom.Domain;
using Diplom.Interfaces;
using Diplom.ViewModels.Main.AccountViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Security;

namespace Diplom.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private TeacherRepository TeacherRepository;
        private UserRepository UserRepository;
        private ProfileRepository ProfileRepository;
        private RoleRepository RoleRepository;
        private PermissionRepository PermissionRepository;
        public AuthenticationService(IApplicationContext context)
        {
            UserRepository = new UserRepository(context);
            ProfileRepository = new ProfileRepository(context);
            RoleRepository = new RoleRepository(context);
            PermissionRepository = new PermissionRepository(context);
            TeacherRepository = new TeacherRepository(context);
        }
        public void SignOut()
        {
            FormsAuthentication.SignOut();
        }
        public HttpCookie SignIn(LoginViewModel view)
        {
            view.Password = view.Password.GetHash();
            var user = UserRepository.Get(u => u.Email.ToLower() == view.Email.ToLower() && u.PasswordHash == view.Password, includeProperties: "Role").FirstOrDefault();
            if (user != null && user.isActive)
            {
                var authCookie = GetAuthTicket(user.UserName, user.Role.Name, view.RememberMe);
                return authCookie;
            }
            return null;
        }
        public HttpCookie SignUp(RegisterView view)
        {
            if (UserRepository.Get().Any(u => u.UserName.ToLower() == view.UserName.ToLower() || u.Email.ToLower() == view.Email.ToLower()))
            {
                return null;
            }
            var teacher = new Teacher();
            var profile = new Domain.Profile()
            {
                FullName = view.FirstName +" "+ view.LastName,
                Sex = view.Gender,
                BirthDate = DateTimeProvider.ParseDate(view.BirthDate)
            };
           
            //ProfileRepository.Insert(profile);
            //TeacherRepository.Insert(teacher);
            var user = new Domain.User()
            {
                Email = view.Email,
                isActive = true,
                PasswordHash = view.Password.GetHash(),
                UserName = view.UserName,
                Profile = profile,
                Role = GetDefaultRole(),
                Teacher = teacher
            };
            ProfileRepository.Insert(profile);
            TeacherRepository.Insert(teacher);
            UserRepository.Insert(user);
            UserRepository.Save();
            return GetAuthTicket(user.UserName, user.Role.Name, true);
        }
        public HttpCookie SignUpIndirect(User user,Profile profile,LoginType type)
        {
            if (UserRepository.Get().Any(u => u.UserName.ToLower() == user.UserName.ToLower() || u.Email.ToLower() == user.Email.ToLower()))
            {
                var userTemp = UserRepository.Get(includeProperties: "Role").FirstOrDefault(u=>u.UserName==user.UserName && u.isActive==user.isActive);
                if (userTemp == null) return null;
                return GetAuthTicket(userTemp.UserName, userTemp.Role.Name, true);
            }
            user.LoginType = type;
            user.Role = GetDefaultRole();
            var teacher = new Teacher();
            TeacherRepository.Insert(teacher);
            user.Teacher = teacher;
            ProfileRepository.Insert(profile);
            UserRepository.Insert(user);
            UserRepository.Save();
            return GetAuthTicket(user.UserName, user.Role.Name, true);
        }
        private Role GetDefaultRole()
        {
            var defaultRole = RoleRepository.Get().FirstOrDefault(r => r.Name == "Anonymous");
            if (defaultRole != null)
            {
                return defaultRole;
            }
            return CreateDefaultRole();
        }
        private Role CreateDefaultRole()
        {
            var permission = new Permission();
            PermissionRepository.Insert(permission);
            var role = new Role()
            {
                Name = "Anonymous",
                Permission = permission
            };
            RoleRepository.Insert(role);
            return role;
        }
        private HttpCookie GetAuthTicket(string email, string role, bool rememberMe)
        {
            var authTicket = new FormsAuthenticationTicket(
              10, // Version
              email, // User Login
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

    }
}
