using Diplom.Domain;
using Diplom.ViewModels.SystemAdministration.Role;
using Diplom.ViewModels.SystemAdministration.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diplom.Interfaces
{
    public interface IRoleService
    {
        List<Role> GetAllRoles();
        bool Add(AddRoleViewModel view);
        void FillEdit(EditRoleViewModel view);
        void Edit(EditRoleViewModel view);
        bool ChangeActive(string id);
    }
}
