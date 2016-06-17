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
  public class RequestRepository 
  {
    private ApplicationContext context;
    public RequestRepository(ApplicationContext context)
    {
      this.context = context;
    }
    public IEnumerable<FriendRequest> GetAll()
    {
      return context.Requests;
    }
    public FriendRequest Get(int id)
    {
      return context.Requests.Find(id);
    }
    public IEnumerable<FriendRequest> GetAllFrom(int id)
    {
      return context.Requests.Where(req => req.From == id);
    }
    public IEnumerable<FriendRequest> GetAllTo(int id)
    {
      return context.Requests.Where(req => req.To == id);
    }
    public FriendRequest GetFrom(int id)
    {
      return context.Requests.FirstOrDefault(req => req.From == id);
    }
    public FriendRequest GetTo(int id)
    {
      return context.Requests.FirstOrDefault(req => req.To == id);
    }
    public int Count()
    {
      return context.Requests.Count();
    }
    public void Create(FriendRequest item)
    {
      context.Requests.Add(item);
      Save();
    }
    public void Update(FriendRequest item)
    {
      context.Entry(item).State = EntityState.Modified;
      Save();
    }
    public void Delete(int id)
    {
      var request = context.Requests.Find(id);
      if (request != null)
        context.Requests.Remove(request);
    }
    public void Save()
    {
      context.SaveChanges();
    }
  }
}
