using Diplom.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diplom.Domain
{
    public class UserInChat : BaseEntity
    {
        public User User { get; set; }
        public Chat Chat { get; set; }
        public bool Leaving { get; set; }
    }
}
