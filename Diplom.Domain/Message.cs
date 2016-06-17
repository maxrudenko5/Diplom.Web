using Diplom.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diplom.Domain
{
    public class Message : BaseEntity
    {
        public Chat Chat { get; set; }
        public User Author { get; set; }
        public string Text { get; set; }
        public int Read { get; set; }
        public int Deleted { get; set; }
    }
}
