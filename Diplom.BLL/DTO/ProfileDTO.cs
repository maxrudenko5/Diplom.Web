﻿using Diplom.DAL.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Diplom.BLL.DTO
{
  public class ProfileDTO
  {
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Patronomic { get; set; }
    public string BirthDate { get; set; }
    public int Sex { get; set; }
    public string Photo_max { get; set; }
    public string Photo_200 { get; set; }
    public string Skype { get; set; }
    public string Phone { get; set; }
  }
}