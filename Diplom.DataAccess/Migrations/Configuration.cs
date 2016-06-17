namespace Diplom.DataAccess.Migrations
{
    using Domain;
    using Repositories;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Globalization;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Diplom.DataAccess.ApplicationContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(Diplom.DataAccess.ApplicationContext context)
        {
            AddRoles(context);
            AddAdministrator(context);
        }
        private void AddRoles(ApplicationContext context)
        {
            var roleRepository = new RoleRepository(context);
            var permissionRepository = new PermissionRepository(context);
            if (roleRepository.Get().Any())
            {
                return;
            }
            var permission = new Permission()
            {
                ModeratorAccess = true,
                StudentAccess = true,
                SystemAdministrationAccess = true,
                TeacherAccess = true
            };
            var role = new Role();
            role.isActive = true;
            role.Name = "Admin";
            role.Permission = permission;
            permissionRepository.Insert(permission);
            roleRepository.Insert(role);
            context.SaveChanges();
        }
        private void AddAdministrator(ApplicationContext context)
        {
            var roleRepository = new RoleRepository(context);
            var userRepository = new UserRepository(context);
            var profileRepository = new ProfileRepository(context);

            if (userRepository.Get().Any(u=>u.UserName=="MainAdmin"))
            {
                return;
            }

            var roleAdmin = roleRepository.Get(x => x.Name == "Admin").FirstOrDefault();
            if (roleAdmin == null)
            {
                return;
            }
            var profile = new Profile()
            {
                FullName="Max Rudenko",
                Sex = Sex.male,
                BirthDate = DateTime.Now,
                Photo_200 = "http://www.directmedia.ru/images/user-photo.png",
                Photo_max = "http://www.directmedia.ru/images/user-photo.png"
            };
            var teacher = new Teacher();
            context.Teachers.Add(teacher);
            context.Profiles.Add(profile);
            var user = new User()
            {
                UserName = "MainAdmin",
                PasswordHash = "200820e3227815ed1756a6b531e7e0d2",
                Email = "brut_75@mail.ru",
                isActive = true,
                LoginType = LoginType.Direct,
                Profile = profile,
                Role = roleAdmin,
                Teacher = teacher
            };
            userRepository.Insert(user);
            context.SaveChanges();
        }
    }
}
