using Diplom.DAL.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diplom.DAL.Repositories
{
  /*public class TeacherRepository
  {
    private ApplicationContext context;
    public TeacherRepository(ApplicationContext context)
    {
      this.context = context;
    }


    public void Save()
    {
      context.SaveChanges();
    }
    public IEnumerable<Teacher> GetAll()
    {
      return context.Teachers;
    }
    public Teacher Find(int id)
    {
      return context.Teachers.FirstOrDefault(teach => teach.Id == id);
    }
    public Teacher Get(int id)
    {
      return context.Teachers.FirstOrDefault(teach => teach.UserId == id);
    }
    public int Count()
    {
      return context.Users.Count();
    }
    public void Create(Teacher teach)
    {
      context.Teachers.Add(teach);
      context.SaveChanges();
    }
    public void Update(Teacher teach)
    {
      context.Entry(teach).State = EntityState.Modified;
      Save();
    }
    public void Delete(int id)
    {
      var teach = context.Teachers.Find(id);
      if (teach != null)
        context.Teachers.Remove(teach);
    }
  }*/
}
