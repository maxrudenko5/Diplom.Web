using Diplom.DAL.EF;
using Diplom.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Diplom.DAL.Repositories
{
  public class UsersRepository
  {
    private ApplicationContext context;
    public UsersRepository(ApplicationContext context)
    {
      this.context = context;
    }
    public User GetFirstUnconfirmed()
    {
      return context.Users.Include(u => u.Profile).Include(u => u.Role).FirstOrDefault(u => u.Confirmed==0);
    }
    public User Get(int id)
    {
      return context.Users.Include(u=>u.Profile).Include(u=>u.Role).FirstOrDefault(u => u.Id == id);
    }
    public User GetByEmail(string email)
    {
      return context.Users.Include(u => u.Profile).Include(u => u.Role).FirstOrDefault(u => u.Email.ToLower() == email.ToLower());
    }
    public User GetByUserName(string userName)
    {
      return context.Users.Include(u=>u.Profile).Include(u=>u.Role).FirstOrDefault(u => u.UserName.ToLower() == userName.ToLower());
    }
    public User Find(string userName, string hpassword)
    {
      return context.Users.Include(p=>p.Role).FirstOrDefault(u => u.UserName.ToLower() == userName.ToLower() && u.PasswordHash== hpassword);
    }
    public List<User> GetAll(int lastId,int count)
    {
      return context.Users.Include(u=>u.Profile).Include(u=>u.Role).OrderByDescending(u => u.Id).Take(count).Where(u => u.Id > lastId).ToList();
    }
    public List<User> GetAll()
    {
      return context.Users.ToList();
    }
    public async Task<List<User>> GetAllAsync()
    {
      return await context.Users.ToListAsync();
    }
    public int Count()
    {
      return context.Users.Count();
    }
    public void Create(User user)
    {
      context.Users.Add(user);
      context.SaveChanges();
    }
    public void Update(User book)
    {
      context.Entry(book).State = EntityState.Modified;
    }
    public void Delete(int id)
    {
      var user = context.Users.Find(id);
      if (user != null)
        context.Users.Remove(user);
    }
    
  }
}
