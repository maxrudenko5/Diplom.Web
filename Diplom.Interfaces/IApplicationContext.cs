using Diplom.Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diplom.Interfaces
{
    public interface IApplicationContext : IDisposable
    {
        DbSet<Profile> Profiles { get; set; }
        DbSet<Chat> Chats { get; set; }
        DbSet<Message> Messages { get; set; }
        DbSet<FriendRequest> FriendRequests { get; set; }
        DbSet<Teacher> Teachers { get; set; }
        DbSet<User> Users { get; set; }
        DbSet<Role> Roles { get; set; }
        DbSet<Post> Posts { get; set; }
        DbSet<Permission> Permissions { get; set; }
        DbSet<UserInChat> UserInChats { get; set; }
        DbSet<ReadUserMessage> ReadUserMessages { get; set; }
        int SaveChanges();
    }
}
