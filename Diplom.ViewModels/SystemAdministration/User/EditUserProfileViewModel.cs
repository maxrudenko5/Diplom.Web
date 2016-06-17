using Diplom.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Diplom.ViewModels.SystemAdministration.User
{
    public class EditUserProfileViewModel
    {
        [Required]
        public string UserId { get; set; }
        [Required]
        [RegularExpression(@"[A-Za-zА-Яа-я]{3,10}\s[A-Za-zА-Яа-я]{3,10}", ErrorMessage = "Некорректный адрес")]
        public string FullName { get; set; }
        public string Patronymic { get; set; }
        [Required]
        public string BirthDate { get; set; }
        [Required]
        public Sex Sex { get; set; }
        public string Photo_max { get; set; }
        public string Photo_200 { get; set; }
        public string Skype { get; set; }
        public string Phone { get; set; }
    }
}
