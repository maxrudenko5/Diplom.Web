using Diplom.Domain;
using Diplom.ViewModels.SystemAdministration.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diplom.Interfaces
{
    public interface IUserService
    {
        List<User> GetAllUsers();
        List<Role> GetAllRoles();
        List<User> GetAllLockUsers();
        bool Add(AddUserViewModel view);
        void FillShow(ShowUserViewModel view);
        void FillEdit(EditUserViewModel view);
        void FillProfileEdit(EditUserProfileViewModel view);
        void EditUserProfile(EditUserProfileViewModel view);
        void Edit(EditUserViewModel view);
        bool ChangeActive(string id);
    }
}
