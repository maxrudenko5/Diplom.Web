using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Diplom.Web.Utilities
{
    public class ResponseBootgrid<T> where T : class
    {
        public int current = 1;
        public int rowCount = 10;
        public List<T> rows;
        public ResponseBootgrid(List<T> list)
        {
            rows = list;
        }
        public int total { get { return rows.Count; } }
    }
}