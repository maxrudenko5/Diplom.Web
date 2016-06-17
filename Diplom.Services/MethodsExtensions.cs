using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace Diplom.Services
{
  static public class MethodsExtensions
  {
    public static string GetHash(this string str)
    {
      MD5 md5 = new MD5CryptoServiceProvider();
      string temp = str;
      byte[] checkSum = md5.ComputeHash(Encoding.UTF8.GetBytes(temp));
      string result = BitConverter.ToString(checkSum).Replace("-", String.Empty);
      return result;
    }
    public static string NormalizeResponse(this string str)
    {
      str = str.Remove(0, 13);
      str = str.Remove(str.Length - 2, 2);
      return str;
    }
    
  }
}