using Diplom.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Diplom.Domain
{
    public class User : BaseEntity
    {
        public string UserName { get; set; }
        public string PasswordHash { get; set; }
        public string Email { get; set; }
        public bool isActive { get; set; }
        public LoginType LoginType { get; set; }
        public Profile Profile { get; set; }
        public Role Role { get; set; }
        public Teacher Teacher { get; set; }
    }

}