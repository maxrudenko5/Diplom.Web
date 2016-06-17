
using Diplom.DataAccess;
using Diplom.Interfaces;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace Diplom.Web.Providers
{
    public class CustomRoleProvider : RoleProvider
    {
        public override string[] GetRolesForUser(string login)
        {
            string[] role = new string[] { };
            using (IApplicationContext context = new ApplicationContext())
            {
                var user = context.Users.Include("Role").FirstOrDefault(u => u.Role.isActive &&u.UserName==login);
                if (user == null)
                {
                    role = new string[] { };
                    return role;
                }
                var userrole = context.Roles.Include("Permission").FirstOrDefault(r => r.Id == user.Role.Id);
                if (userrole != null)
                {
                    role = new string[] {
                        userrole.Permission.ModeratorAccess==true ? nameof(userrole.Permission.ModeratorAccess) : "",
                        userrole.Permission.StudentAccess==true ? nameof(userrole.Permission.StudentAccess) : "",
                        userrole.Permission.SystemAdministrationAccess==true ? nameof(userrole.Permission.SystemAdministrationAccess) : "",
                        userrole.Permission.TeacherAccess==true ? nameof(userrole.Permission.TeacherAccess) : ""
                    };
                    return role;
                }
            }
            role = new string[] { };
            return role;
        }
        public override void CreateRole(string roleName)
        {
            throw new NotImplementedException();
        }
        public override bool IsUserInRole(string username, string roleName)
        {
            bool outputResult = false;
            // Находим пользователя

            using (IApplicationContext context = new ApplicationContext())
            {
                var user = context.Users.Include("Role").FirstOrDefault(u => u.Role.isActive && u.UserName == username);
                if (user == null)
                {
                    return outputResult;
                }
                var userrole = context.Roles.Include("Permission").FirstOrDefault(r => r.Id == user.Role.Id);
                if (userrole != null)
                {
                    var permissions = new List<string>();
                    permissions.Add(userrole.Permission.ModeratorAccess == true ? nameof(userrole.Permission.ModeratorAccess) : "");
                    permissions.Add(userrole.Permission.StudentAccess == true ? nameof(userrole.Permission.StudentAccess) : "");
                    permissions.Add(userrole.Permission.SystemAdministrationAccess == true ? nameof(userrole.Permission.SystemAdministrationAccess) : "");
                    permissions.Add(userrole.Permission.TeacherAccess == true ? nameof(userrole.Permission.TeacherAccess) : "");
                    outputResult = permissions.Any(p => p == roleName);
                }
            }
            return outputResult;
        }
        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override string ApplicationName
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }

        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            throw new NotImplementedException();
        }

        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            throw new NotImplementedException();
        }

        public override string[] GetAllRoles()
        {
            throw new NotImplementedException();
        }

        public override string[] GetUsersInRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override bool RoleExists(string roleName)
        {
            throw new NotImplementedException();
        }
    }
}