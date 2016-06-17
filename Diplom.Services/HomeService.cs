using Diplom.DataAccess.Repositories;
using Diplom.Domain;
using Diplom.Interfaces;
using Diplom.ViewModels.Main.HomeViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diplom.Services
{
    public class HomeService : IHomeService
    {
        private PostRepository PostRepository;
        private TeacherRepository TeacherRepository;
        private UserRepository UserRepository;
        public HomeService(IApplicationContext context)
        {
            UserRepository = new UserRepository(context);
            TeacherRepository = new TeacherRepository(context);
            PostRepository = new PostRepository(context);
        }
        public void FillIndex(HomeIndexViewModel view)
        {
            view.Teachers = UserRepository.Get(x=>x.Teacher.IsActive,includeProperties:"Teacher,Profile").ToList();
        }
        public List<Post> GetPosts()
        {
            return PostRepository.Get().OrderByDescending(p => p.CreationDate).ToList();
        }
        public Post GetPost(int? id)
        {
            if (id==null)
            {
                return null;
            }
            return PostRepository.Get().FirstOrDefault(p => p.Id == (int)id);
        }
        public bool DeletePost(int? id)
        {
            if (id == null)
            {
                return false;
            }
            var post = PostRepository.Get().FirstOrDefault(p => p.Id == (int)id);
            if (post == null)
            {
                return false;
            }
            PostRepository.Delete(post);
            PostRepository.Save();
            return true;
        }
    }
}
