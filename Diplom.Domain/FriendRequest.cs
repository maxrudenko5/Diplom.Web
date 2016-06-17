using Diplom.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diplom.Domain
{
    public class FriendRequest : BaseEntity
    {
        public User From { get; set; }
        public User To { get; set; }
        public FriendRequestResult FromResult { get; set; }
        public FriendRequestResult ToResult { get; set; }
    }
}
