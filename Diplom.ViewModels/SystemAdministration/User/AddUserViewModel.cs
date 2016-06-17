using Diplom.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diplom.ViewModels.SystemAdministration.User
{
  public class AddUserViewModel
  {
    [Required]
    [Display(Name = "Имя")]
    [StringLength(16, ErrorMessage = "Значение {0} должно содержать не менее {2} символов.", MinimumLength = 3)]
    public string FirstName { get; set; }
    [Required]
    [Display(Name = "Фамилия")]
    [StringLength(16, ErrorMessage = "Значение {0} должно содержать не менее {2} символов.", MinimumLength = 3)]
    public string LastName { get; set; }
    [Required]
    [Display(Name = "Адрес электронной почты")]
    [StringLength(30, ErrorMessage = "Значение {0} должно содержать не менее {2} символов, и не более 30", MinimumLength = 3)]
    [RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}", ErrorMessage = "Некорректный адрес")]
    public string Email { get; set; }
    [Required]
    [Display(Name = "UserName")]
    [RegularExpression(@"^[a-zA-Z][a-zA-Z0-9-_\.]{1,20}$", ErrorMessage = "Некорректный nickname")]
    [StringLength(20, ErrorMessage = "Значение {0} должно содержать не менее {2} символов, и не более 20", MinimumLength = 3)]
    public string UserName { get; set; }


    [Required]
    [StringLength(16, ErrorMessage = "Значение {0} должно содержать не менее {2} символов.", MinimumLength = 6)]
    [DataType(DataType.Password)]
    [Display(Name = "Пароль")]
    public string Password { get; set; }

    [Required]
    [DataType(DataType.Date)]
    [DisplayFormat(DataFormatString = "{0:dd'/'MM'/'yyyy}", ApplyFormatInEditMode = true)]
    [Display(Name = "Дата рождения")]
    public string BirthDate { get; set; }
    [Required]
    [Display(Name = "Пол")]
    public Domain.Sex Sex { get; set; }
    [Required]
    public string SelectedRoleId { get; set; }
    public List<Domain.Role> AllExitstRoles { get; set; }
  }
}
