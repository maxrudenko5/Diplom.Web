using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using Diplom.DAL.EF;
using Diplom.DAL.Entities;

namespace Diplom.DAL.Repositories
{
  public class ChatRepository
  {
    private ApplicationContext context;
    public ChatRepository(ApplicationContext context)
    {
      this.context = context;
    }
    public IEnumerable<Chat> GetAll()
    {
      return context.Chats;
    }
    
    public Chat Get(int id)
    {
      return context.Chats.Find(id);
    }
    public int Count()
    {
      return context.Chats.Count();
    }
    public void Create(Chat item)
    {
      context.Chats.Add(item);
      Save();
    }
    public void Update(Chat item)
    {
      context.Entry(item).State = EntityState.Modified;
      Save();
    }
    public void Delete(int id)
    {
      var chat = context.Chats.Find(id);
      if (chat != null)
        context.Chats.Remove(chat);
    }
    public void Save()
    {
      context.SaveChanges();
    }
  }
}
