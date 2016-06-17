using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diplom.Domain
{
    public class ReadUserMessage : Base.BaseEntity
    {
        public User User { get; set; }
        public Message Message { get; set; }
        public bool Read { get; set; }
    }
}
