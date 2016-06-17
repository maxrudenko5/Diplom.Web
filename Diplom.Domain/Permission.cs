using Diplom.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diplom.Domain
{
    public class Permission : BaseEntity
    {
        public bool SystemAdministrationAccess { get; set; }
        public bool ModeratorAccess { get; set; }
        public bool StudentAccess { get; set; }
        public bool TeacherAccess { get; set; }
    }
}
