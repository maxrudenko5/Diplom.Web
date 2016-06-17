using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Diplom.ViewModels.Main.AccountViewModels
{

    public class LoginViewModel
    {
        [Required]
        [Display(Name = "Адрес электронной почты")]
        [StringLength(30, ErrorMessage = "Значение {0} должно содержать не менее {2} символов, и не более 30", MinimumLength = 3)]
        [RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}", ErrorMessage = "Некорректный адрес")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль")]
        public string Password { get; set; }

        [Display(Name = "Запомнить меня")]
        public bool RememberMe { get; set; }
    }
    public class RegisterView
    {
        [Required]
        [Display(Name = "Имя")]
        [StringLength(16, ErrorMessage = "Значение {0} должно содержать не менее {2} символов.", MinimumLength = 3)]
        public string FirstName { get; set; }
        [Required]
        [Display(Name = "Фамилия")]
        [StringLength(16, ErrorMessage = "Значение {0} должно содержать не менее {2} символов.", MinimumLength = 3)]
        public string LastName { get; set; }
        /*  [Required]
          [Display(Name = "Отчество")]
          [StringLength(16, ErrorMessage = "Значение {0} должно содержать не менее {2} символов.", MinimumLength = 3)]
          public string Patronomic { get; set; }*/
        [Required]
        [Display(Name = "Адрес электронной почты")]
        [StringLength(30, ErrorMessage = "Значение {0} должно содержать не менее {2} символов, и не более 30", MinimumLength = 3)]
        [RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}", ErrorMessage = "Некорректный адрес")]
        public string Email { get; set; }
        [Required]
        [Display(Name = "Username")]
        [StringLength(20, ErrorMessage = "Значение {0} должно содержать не менее {2} символов, и не более 20", MinimumLength = 3)]
        public string UserName { get; set; }


        [Required]
        [DataType(DataType.Password)]
        [StringLength(16, ErrorMessage = "Значение {0} должно содержать не менее {2} символов.", MinimumLength = 6)]
        [Display(Name = "Пароль")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Подтверждение пароля")]
        [Compare("Password", ErrorMessage = "Пароль и его подтверждение не совпадают.")]
        public string ConfirmPassword { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd'/'MM'/'yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Дата рождения")]
        public string BirthDate { get; set; }
        [Required]
        [Display(Name = "Пол")]
        public Domain.Sex Gender { get; set; }
    }
    public class ForgotPasswordView
    {

    }
}