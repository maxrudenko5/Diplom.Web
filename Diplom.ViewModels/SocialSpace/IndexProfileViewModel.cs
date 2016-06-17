using Diplom.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diplom.ViewModels.SocialSpace
{
    public class IndexProfileViewModel
    {
        public User User { get; set; }
        public List<User> Friends { get; set; }
        public List<User> Subscribers { get; set; }
        public List<User> Supscriptions { get; set; }
        //Basic information
        public string BirthDate { get; set; }
        [Range(0, 2, ErrorMessage = "Select a correct gender")]
        public Sex Sex { get; set; }
        [StringLength(16, MinimumLength = 13, ErrorMessage = "Длина строки должна быть от 0 до 11 символов")]
        public string Phone { get; set; }
        [RegularExpression(@"[A-Za-z0-9._%+-]{0,31}", ErrorMessage = "Некорректный адрес")]
        public string Skype { get; set; }
        public PageType Type { get; set; }
    }
    public enum PageType
    {
        My=0,
        Friend,
        Signed,
        Other
    }
}
