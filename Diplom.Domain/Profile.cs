using Diplom.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Diplom.Domain
{
    public class Profile : BaseEntity
    {
        public string FullName { get; set; }
        public string Patronymic { get; set; }
        public DateTime BirthDate { get; set; }
        [Range(0,2, ErrorMessage = "Select a correct license")]
        public Sex Sex { get; set; }
        public string Photo_max { get; set; }
        public string Photo_200 { get; set; }
        public string Skype { get; set; }
        public string Phone { get; set; }
    }
    public enum Sex
    {
        [Display(Name = "Нет")]
        none = 0,
        [Display(Name = "Женский")]
        female = 1,
        [Display(Name = "Мужской")]
        male = 2

    }
}