using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Diplom.Web.Utilities.ApiVK
{
    public class UserInfo
    {
        public string id { get; set; }
        public string sex { get; set; }
        public string bdate { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string nickname { get; set; }
        public string photo_200_orig { get; set; }
        public string photo_400_orig { get; set; }
        public string photo_max { get; set; }
        public string photo_id { get; set; }
        public string has_photo { get; set; }
        public string home_phone { get; set; }
        public string status { get; set; }
        public string home_town { get; set; }
        public string relation { get; set; }
        public string deactivated { get; set; }
    }

}