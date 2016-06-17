using Diplom.BusinessLogic;
using Diplom.DataAccess.Repositories;
using Diplom.Domain;
using Diplom.Interfaces;
using Diplom.ViewModels.SystemAdministration.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diplom.Services
{
    public class UserService : IUserService
    {
        private UserRepository UserRepository;
        private ProfileRepository ProfileRepository;
        private RoleRepository RoleRepository;
        private TeacherRepository TeacherRepository;
        public UserService(IApplicationContext context)
        {
            TeacherRepository = new TeacherRepository(context);
            UserRepository = new UserRepository(context);
            ProfileRepository = new ProfileRepository(context);
            RoleRepository = new RoleRepository(context);
        }
        public List<User> GetAllUsers()
        {
            return UserRepository.Get(includeProperties: "Role").ToList();
        }
        public List<User> GetAllLockUsers()
        {
            return UserRepository.Get(u => u.isActive == false, includeProperties: "Role").ToList();
        }
        public List<Role> GetAllRoles()
        {
            return RoleRepository.Get().ToList();
        }
        public bool Add(AddUserViewModel view)
        {
            var existUserName = UserRepository.Get().Any(u => u.UserName == view.UserName);
            if (existUserName)
            {
                return false;
            }
            var user = new User();
            var role = RoleRepository.Get(u => u.Id == view.SelectedRoleId).FirstOrDefault();
            user.Role = role;
            user.isActive = true;
            user.Email = view.Email;
            user.UserName = view.UserName;
            user.PasswordHash = view.Password.GetHash();
            var profile = new Profile()
            {
                Sex = view.Sex,
                FullName = view.FirstName + " " + view.LastName,
                BirthDate = DateTimeProvider.ParseDate(view.BirthDate)
            };
            ProfileRepository.Insert(profile);
            user.Profile = profile;
            UserRepository.Insert(user);
            UserRepository.Save();
            return true;
        }

        public void FillEdit(EditUserViewModel view)
        {
            var user = UserRepository.Get(u => u.Id == view.Id, includeProperties: "Profile").FirstOrDefault();
            if (user == null)
            {
                return;
            }
            var roles = RoleRepository.Get().ToList();
            view.AllExitstRoles = roles;
            //view.Email = user.Email;
            view.UserName = user.UserName;
            view.SelectedRoleId = user.Role.Id;
            view.Id = user.Id;
        }
        public void FillProfileEdit(EditUserProfileViewModel view)
        {
            if (String.IsNullOrEmpty(view.UserId))
            {
                return;
            }
            var user = UserRepository.Get(u => u.Id == view.UserId, includeProperties: "Profile").FirstOrDefault();
            if (user != null)
            {
                view.Patronymic = user.Profile.Patronymic;
                view.FullName = user.Profile.FullName;
                view.Photo_200 = user.Profile.Photo_200;
                view.Photo_max = user.Profile.Photo_max;
                view.Phone = user.Profile.Phone;
                view.Sex = user.Profile.Sex;
                view.Skype = user.Profile.Skype;
                view.BirthDate = DateTimeProvider.ParseDateToString(user.Profile.BirthDate);
                view.UserId = user.Id;
            }
        }
        public void FillShow(ShowUserViewModel view)
        {
            var user = UserRepository.Get(u => u.Id == view.Id, includeProperties: "Profile").FirstOrDefault();
            if (user == null)
            {
                return;
            }
            view.Email = user.Email;
            view.UserName = user.UserName;
            view.Id = user.Id;
            view.Profile = user.Profile;
        }
        public void Edit(EditUserViewModel view)
        {
            var user = UserRepository.Get(x=>x.Id==view.Id,includeProperties:"Teacher").FirstOrDefault();
            var role = RoleRepository.Get(x=>x.Id==view.SelectedRoleId,includeProperties: "Permission").FirstOrDefault();
            user.Role = role;
            user.Teacher.IsActive = false;
            if (user.Role.Permission.TeacherAccess)
            {
                user.Teacher.IsActive = true;
            }
            TeacherRepository.Update(user.Teacher);
            UserRepository.Update(user);
            UserRepository.Save();
        }
        public void EditUserProfile(EditUserProfileViewModel view)
        {
            if (String.IsNullOrEmpty(view.UserId))
            {
                return;
            }
            var profile = UserRepository.Get(u => u.Id == view.UserId, includeProperties: "Profile").Select(s => s.Profile).FirstOrDefault();
            if (profile == null)
            {
                return;
            }
            profile.FullName = view.FullName;
            profile.BirthDate = DateTimeProvider.ParseDate(view.BirthDate);
            profile.Patronymic = view.Patronymic;
            profile.Photo_200 = view.Photo_200;
            profile.Photo_max = view.Photo_max;
            profile.Sex = view.Sex;
            profile.Skype = view.Skype;
            profile.Phone = view.Phone;
            ProfileRepository.Update(profile);
            ProfileRepository.Save();
        }
        public bool ChangeActive(string id)
        {
            if (String.IsNullOrEmpty(id))
            {
                return false;
            }
            var user = UserRepository.GetByID(id);
            user.isActive = !user.isActive;
            UserRepository.Update(user);
            UserRepository.Save();
            return user.isActive;
        }
    }
}
