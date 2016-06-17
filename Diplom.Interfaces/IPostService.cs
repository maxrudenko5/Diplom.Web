using Diplom.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diplom.Interfaces
{
    public interface IPostService
    {
        List<Post> GetPosts();
        bool CreatePost(Post post);
        bool DeletePost(string id);
    }
}
