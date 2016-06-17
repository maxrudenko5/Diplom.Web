using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Diplom.DAL.Entities
{
  public class Profile
  {
    [Key]
    [ForeignKey("User")]
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string Patronymic { get; set; }
    public string LastName { get; set; }
    public DateTime BirthDate { get; set; }
    public int Sex { get; set; }
    public string Photo_max { get; set; }
    public string Photo_200 { get; set; }
    public string Skype { get; set; }
    public string Phone { get; set; }
    public User User { get; set; }
  }
}