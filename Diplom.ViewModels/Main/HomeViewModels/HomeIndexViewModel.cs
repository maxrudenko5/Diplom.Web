using Diplom.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diplom.ViewModels.Main.HomeViewModels
{
    public class HomeIndexViewModel
    {
        public List<User> Teachers { get; set;}
        public HomeIndexViewModel()
        {
            Teachers = new List<User>();
        }
    }
}
