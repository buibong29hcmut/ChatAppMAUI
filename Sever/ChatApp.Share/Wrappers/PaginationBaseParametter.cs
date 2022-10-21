using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp.Share.Wrappers
{
    public abstract class PaginationBaseParametter
    {
        public int PageSize { get; set; }
        public int PageNumber { get; set; }
    }
}
