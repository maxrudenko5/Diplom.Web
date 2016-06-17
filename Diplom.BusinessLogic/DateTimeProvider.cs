using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diplom.BusinessLogic
{
  public static class DateTimeProvider
  {
    public static string ParseDateToString(DateTime? date)
    {
      if (!date.HasValue)
      {
        return string.Empty;
      }
      return date.Value.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
    }
    public static DateTime ParseDate(string date)
    {
      if (String.IsNullOrEmpty(date))
      {
        return DateTime.Now;
      }
      return DateTime.ParseExact(date, "dd/MM/yyyy", CultureInfo.InvariantCulture);
    }
  }
}
