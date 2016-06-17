using AutoMapper;
using Diplom.DataAccess.Repositories;
using Diplom.Domain;
using Diplom.Interfaces;
using Diplom.ViewModels.SystemAdministration.Role;
using Diplom.ViewModels.SystemAdministration.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diplom.Services
{
    public class RoleService : IRoleService
    {
        private RoleRepository RoleRepository;
        private PermissionRepository PermissionRepository;
        public RoleService(IApplicationContext context)
        {
            RoleRepository = new RoleRepository(context);
            PermissionRepository = new PermissionRepository(context);
        }
        public List<Role> GetAllRoles()
        {
            return RoleRepository.Get().ToList();
        }
        public bool Add(AddRoleViewModel view)
        {
            var existRole = RoleRepository.Get().Any(u => u.Name.ToLower() == view.Name.ToLower());
            if (existRole)
            {
                return false;
            }
            var role = new Role();
            PermissionRepository.Insert(view.Permission);
            role.Permission = view.Permission;
            role.Name = view.Name;
            role.isActive = true;
            RoleRepository.Insert(role);
            RoleRepository.Save();
            return true;
        }
        public void FillEdit(EditRoleViewModel view)
        {
            var role = RoleRepository.Get(r=>r.Id==view.Id,includeProperties:"Permission").FirstOrDefault();
            if (role == null)
            {
                return;
            }
            view.Permission = role.Permission;
        }
        public void Edit(EditRoleViewModel view)
        {
            var role = RoleRepository.Get(r=>r.Id==view.Id,includeProperties:"Permission").FirstOrDefault();
            role.Permission.ModeratorAccess = view.Permission.ModeratorAccess;
            role.Permission.StudentAccess = view.Permission.StudentAccess;
            role.Permission.SystemAdministrationAccess = view.Permission.SystemAdministrationAccess;
            role.Permission.TeacherAccess = view.Permission.TeacherAccess;
            PermissionRepository.Update(role.Permission);
            PermissionRepository.Save();
        }
        public bool ChangeActive(string id)
        {
            if (String.IsNullOrEmpty(id))
            {
                return false;
            }
            var role = RoleRepository.GetByID(id);
            role.isActive = !role.isActive;
            RoleRepository.Update(role);
            RoleRepository.Save();
            return role.isActive;
        }
    }
}
