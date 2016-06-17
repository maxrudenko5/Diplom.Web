using Diplom.DataAccess.Repositories;
using Diplom.Domain;
using Diplom.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diplom.Services
{
    public class PostService : IPostService
    {
        private PostRepository PostRepository;
        public PostService(IApplicationContext context)
        {
            PostRepository = new PostRepository(context);
        }
        public List<Post> GetPosts()
        {
            return PostRepository.Get().ToList();
        }
        public bool CreatePost(Post post)
        {
            if (post == null)
            {
                return false;
            }
            PostRepository.Insert(post);
            PostRepository.Save();
            return true;
        }
        public bool DeletePost(string id)
        {
            if (String.IsNullOrEmpty(id))
            {
                return false;
            }
            var post=PostRepository.GetByID(id);
            if(post==null)
            {
                return false;
            }
            PostRepository.Delete(post);
            PostRepository.Save();
            return true;
        }
    }
}
