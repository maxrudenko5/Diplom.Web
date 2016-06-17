using Diplom.Domain;
using Diplom.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diplom.DataAccess
{
    public class ApplicationContext : DbContext, IApplicationContext
    {
        public ApplicationContext() : base("ApplicationConnection")
        {
        }
        static public ApplicationContext Create()
        {
            return new ApplicationContext();
        }
        public DbSet<Profile> Profiles { get; set; }
        public DbSet<Chat> Chats { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<FriendRequest> FriendRequests { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Permission> Permissions { get; set; }
        public DbSet<UserInChat> UserInChats { get; set; }
        public DbSet<ReadUserMessage> ReadUserMessages { get; set; }
        public DbSet<Post> Posts { get; set; }
        
    }
}
