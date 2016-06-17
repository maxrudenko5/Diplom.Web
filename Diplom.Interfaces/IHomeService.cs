using Diplom.Domain;
using Diplom.ViewModels.Main.HomeViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diplom.Interfaces
{
    public interface IHomeService
    {
        List<Post> GetPosts();
        Post GetPost(int? id);
        bool DeletePost(int? id);
        void FillIndex(HomeIndexViewModel view);
    }
}
