using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diplom.ViewModels.SocialSpace
{
    public class SearchProfileViewModel
    {
        public List<SearchProfileItemViewMOdel> Users { get; set; }
        public SearchProfileViewModel()
        {
            Users = new List<SearchProfileItemViewMOdel>();
        }
    }
    public class SearchProfileItemViewMOdel
    {
        public string Username { get; set; }
        public string FullName { get; set; }
        public string Photo_200 { get; set; }
        public string Photo_Max { get; set; }
    }
}
