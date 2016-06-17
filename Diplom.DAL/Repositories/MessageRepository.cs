using Diplom.DAL.EF;
using Diplom.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diplom.DAL.Repositories
{
  public class MessageRepository 
  {
    private ApplicationContext context;
    public MessageRepository(ApplicationContext context)
    {
      this.context = context;
    }
    public IEnumerable<Message> GetAll()
    {
      return context.Messages;
    }
    public Message Get(int id)
    {
      return context.Messages.Find(id);
    }
    public int Count()
    {
      return context.Chats.Count();
    }
    public void Create(Message item)
    {
      context.Messages.Add(item);
      Save();
    }
    public void Update(Message item)
    {
      context.Entry(item).State = EntityState.Modified;
      Save();
    }
    public void Delete(int id)
    {
      var msg = context.Messages.Find(id);
      if (msg != null)
        context.Messages.Remove(msg);
    }
    public void Save()
    {
      context.SaveChanges();
    }
  }
}
