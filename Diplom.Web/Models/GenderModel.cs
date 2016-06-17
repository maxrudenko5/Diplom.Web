using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Diplom.Web.Models
{
  public class GenderModel
  {
    static public int GetSex(string str)
    {
      switch (str)
      {
        case "мужской":return 1;
        case "женский": return 0;
        case "male": return 1;
        case "female": return 0;
        default:return 1;
      }
    }
  }
}